/******************************************************************************
 * Copyright 2021-2023 Illogic Games - All Rights Reserved
 * 
 * You may not use, distribute or modify this code without the express 
 * authorization of Illogic Games.
 *
 * Partial rights may be granted to third parties with the sole purpose of 
 * creating ports for various platforms, as well as debugging and Q.A.
 * 
 * Any other use is strictly prohibited.
 ******************************************************************************/
 
using UnityEngine;

using System;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Globalization;

namespace Illogic.Data
{
    // --- Class --------------------------------------------------------------
    public class InvalidHeaderException : System.Exception{}

    // --- Class --------------------------------------------------------------
    public class DataRow : Dictionary<string, DataItem> {}

    // --- Class --------------------------------------------------------------
    public class DataItem
    {
        // --- Constants ------------------------------------------------------
        /// <summary>
        /// Matches hex color #RRGGBBAA or #RRGGBB
        /// </summary>
        static readonly Regex ColorRegex = new Regex(@"^#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})?$");

        object data;
        Color? color; // prefetched color

        public bool isEmpty => data == null;
        public DataItem(object data) { this.data = data; }
        public static implicit operator int(DataItem d)     { return Convert.ToInt32(d.data, CultureInfo.InvariantCulture); }
        public static implicit operator long(DataItem d)    { return Convert.ToInt64(d.data, CultureInfo.InvariantCulture); }
        public static implicit operator float(DataItem d)   { return Convert.ToSingle(d.data, CultureInfo.InvariantCulture); }
        public static implicit operator string(DataItem d)  { return Convert.ToString(d.data, CultureInfo.InvariantCulture); }
        public static implicit operator bool(DataItem d)    { return Convert.ToBoolean(d.data, CultureInfo.InvariantCulture); }
        public static implicit operator Color(DataItem d)   { return d.ToColor(); }

        // --------------------------------------------------------------------
        /// <summary>
        /// Convert string to equivalent enum
        /// </summary>
        public TEnum ToEnum<TEnum>() where TEnum : struct
        {
            TEnum result;
            if (Enum.TryParse<TEnum>(Convert.ToString(data), out result))
                return result;
            throw new ArgumentException();
        }

        // --------------------------------------------------------------------
        /// <summary>
        /// if the internal data is in the format #RRGGBB[AA], it will
        /// return the correct color. Otherwise it always returns magenta
        /// </summary>
        /// <returns></returns>
        public Color ToColor()
        {
            if (color != null)
                return color.Value;

            var str = (string)data;
            Match match = ColorRegex.Match(str);                    
            if (!match.Success)
            {
                color = Color.magenta;
                return color.Value;
            }

            // shorthand
            const System.Globalization.NumberStyles hex = 
                System.Globalization.NumberStyles.HexNumber;
            
            // get RGB
            byte r = byte.Parse(match.Groups[1].Value, hex);
            byte g = byte.Parse(match.Groups[2].Value, hex);
            byte b = byte.Parse(match.Groups[3].Value, hex);

            // check if there's A
            byte a = 255;
            if (match.Groups[4].Success)
                a = byte.Parse(match.Groups[4].Value, hex);

            color = new Color32(r, g, b, a);
            return color.Value;
        }
    }

    // --- Class --------------------------------------------------------------
    /// <summary>
    /// An extension of the spreadsheet reader that organizes the data
    /// in a table form to make it easier to access
    /// </summary>
    public class DataTable : IEnumerable<DataRow>
    {
        /// <summary> Shorthand to obtain a row by id </summary>
        public DataRow this[long id] { get => data[id]; }

        // --- Properties -----------------------------------------------------
        /// <summary> Number of rows </summary>
        public int nRows => data.Count;

        /// <summary> Where the data is stored. Key is row id </summary>
        Dictionary<long, DataRow> data;

        // --- Methods --------------------------------------------------------
        // --------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataSource">A loaded ODS spreadsheet with data to read</param>
        /// <param name="tableName">Which table to read from the spreadsheet </param>
        public DataTable(ODSReader dataSource, string tableName) 
        {
            dataSource.SelectTable(tableName);
            LoadData(dataSource.rows);
        }
        
        // --------------------------------------------------------------------
        /// <summary>
        /// Loads the spreadsheet data in an accesible way
        /// </summary>
        private void LoadData(XmlNode [] rows)
        {
            data = new Dictionary<long, DataRow>();
            
            // prepare arrays for each column
            var headers = GetHeaders(rows[0]);
            int nHeaders = headers.Count;

            // start filling each column            
            for (int i = 1; i < rows.Length; i++)
            {
                var row = GetDataRow(rows[i], headers);
                if (row != null && row["id"] != null)
                    data[row["id"]] = row;
            }
        }

        // --------------------------------------------------------------------
        /// <summary>
        /// Gets a list of headers from the spreadsheet. This is always the 
        /// first row
        /// </summary>
        List<string> GetHeaders(XmlNode headerRow)
        {
            List<string> headers = new List<string>();

            // first row should be the headers
            var cells = headerRow.ChildNodes;
            for (int i = 0; i < cells.Count; i++)
            {
                // can only recognize string type for headers!
                var cell = cells[i];

                // wrong type of cell
                var attribute = cell.Attributes["office:value-type"];
                if (attribute == null)
                    continue;

                // not a string cell
                if (attribute.Value != "string")
                    throw new InvalidHeaderException();

                // add cell contents
                headers.Add(cell.FirstChild.InnerText);
            }

            return headers;
        }

        // --------------------------------------------------------------------
        /// <summary>
        /// Converts an XmlNode row from the spreadsheet into a DataRow
        /// </summary>
        /// <param name="row">Row to convert</param>
        /// <param name="headers"> List of headers </param>
        DataRow GetDataRow(XmlNode row, List<string> headers)
        {
            var dataRow = new DataRow();
            int nHeaders = headers.Count;

            for (int i = 0, j = 0; i < nHeaders;)
            {
                var cell = row.ChildNodes[j++];
                int copies = GetNumberOfCopies(cell);

                // add as many copies as necessary
                while (copies > 0 && i < nHeaders)
                {
                    copies--;
                    DataItem item = CreateItemFromCell(cell);
                    
                    // skip columns that begin with "#"
                    if (!headers[i].StartsWith("#"))
                        dataRow.Add(headers[i], item);
                    
                    i++;
                }
            }

            return dataRow;
        }
        
        // --------------------------------------------------------------------
        /// <summary>
        /// Some cells just repeat the previous value...
        /// </summary>
        int GetNumberOfCopies(XmlNode cell)
        {
            var repeat = cell.Attributes["table:number-columns-repeated"];
            if (repeat != null)
                return Convert.ToInt32(repeat.Value);
            return 1;
        }

        // --------------------------------------------------------------------
        /// <summary>
        /// Takes a spreadsheet cell and makes a data item for supported types
        /// </summary>
        DataItem CreateItemFromCell(XmlNode cell)
        {
            // if not a valid cell type, then skip it
            var attribute = cell.Attributes["office:value-type"];
            if (attribute == null)
                return null;

            // check if it's a multi-line text
            if (attribute.Value == "string" && cell.ChildNodes.Count > 1) {
                return MultilineString(cell);
            }

            // return correct type
            string value = cell.FirstChild.InnerText;
            switch(attribute.Value)
            {
                case "string"   : return new DataItem(value);
                case "float"    : return new DataItem(float.Parse(value, CultureInfo.InvariantCulture));
                case "boolean"  : return new DataItem(bool.Parse(value));

                // unable to decode this...
                default: 
                    string msg = attribute.Value.ToString() + "->" + value.ToString();
                    throw new System.NotImplementedException(msg);
            }
        }

        // --------------------------------------------------------------------
        DataItem MultilineString(XmlNode cell)
        {
            var sb = new StringBuilder();
            foreach(XmlNode child in cell.ChildNodes)
                sb.AppendLine(child.InnerText);
            
            if (sb.Length >= 2)
                sb.Length -= 2; // remove last /n/r

            return new DataItem(sb.ToString());
        }

        // --------------------------------------------------------------------
        #region Interfaces
        // --------------------------------------------------------------------
        // --- IEnumerable ----------------------------------------------------
        // --------------------------------------------------------------------
        IEnumerator<DataRow> IEnumerable<DataRow>.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        // --------------------------------------------------------------------
        public IEnumerator GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        // --------------------------------------------------------------------
        #endregion
        // --------------------------------------------------------------------
    }
}