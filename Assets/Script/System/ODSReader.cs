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
 
using System.Xml;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Unity.SharpZipLib.Zip;

namespace Illogic.Data
{
    // --- Class --------------------------------------------------------------
    public class TableNotFoundException : System.Exception 
    {
        public TableNotFoundException(string tableName) : 
            base("The table named " + tableName + " could not be found") {}
    }

    // --- Class --------------------------------------------------------------
    /// <summary>
    /// Class to read Open Document Spreadsheets
    /// NOTE: Requires SharpZipLib to uncompress the files
    /// </summary>
    public class ODSReader : XmlDocument
    {
        // --- Properties -----------------------------------------------------
        /// <summary> The rows of the table </summary>
        public XmlNode [] rows { get; private set; }
        
        /// <summary> Needed for searching </summary>
        XmlNamespaceManager nsmgr;

        // --- Methods --------------------------------------------------------
        // --------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resource">Unity resource with an ods file</param>
        /// <param name="selectTable">Which table to read. If null, it reads the first table</param>
        public ODSReader(string resource, string selectTable = null)
        {
            // load the resource, if it exists
            var odsFile = Resources.Load<TextAsset>(resource);
            if (odsFile == null)
                throw new System.IO.FileNotFoundException();

            LoadDocument(odsFile);
            SelectTable(selectTable);
        }

        // --------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="odsFile">ods file</param>
        /// <param name="selectTable">Which table to read. If null, it reads the first table</param>
        public ODSReader(TextAsset odsFile, string selectTable = null)
        {
            LoadDocument(odsFile);
            SelectTable(selectTable);
        }

        // --------------------------------------------------------------------
        /// <summary>
        /// Retrieve a list of table names (sheet names)
        /// </summary>
        public string [] GetTableNames()
        {
            var result = new List<string>();

            // fetch all tables
            var tables = DocumentElement.GetElementsByTagName("table:table");
            for (int i = 0; i < tables.Count; i++)
            {
                var name = tables[i].Attributes["table:name"].Value;
                result.Add(name);
            }
            return result.ToArray();
        }

        // --------------------------------------------------------------------
        /// <summary>
        /// Switches which rows to read and reloads the rows property
        /// </summary>
        public void SelectTable(string tableName)
        {
            var tableRoot = GetTableRoot(tableName);
            LoadRows(tableRoot);
        }

        // --------------------------------------------------------------------
        /// <summary>
        /// Loads the main document
        /// </summary>
        void LoadDocument(TextAsset odsFile)
        {
            // unzip it
            var odsStream = new MemoryStream(odsFile.bytes);
            var odsZipStream = new ZipInputStream(odsStream);

            // search for the content entry
            ZipEntry contentEntry = null;
            while ((contentEntry = odsZipStream.GetNextEntry()) != null)
            {
                if (!contentEntry.IsFile)
                    continue;
                if (contentEntry.Name.ToLower() == "content.xml")
                    break;
            }

            // load the document
            this.Load(odsZipStream);

            // set the namespace
            nsmgr = new XmlNamespaceManager(this.NameTable);
            nsmgr.AddNamespace("xsd",   "http://www.w3.org/2001/XMLSchema");  
            nsmgr.AddNamespace("table", "urn:oasis:names:tc:opendocument:xmlns:table:1.0");  
        }

        // --------------------------------------------------------------------
        /// <summary>
        /// Fetches the XmlNode root for the table with the given name
        /// If tableName is null, returns the first table
        /// It tableName is not found, it throws an exception
        /// </summary>
        XmlNode GetTableRoot(string tableName)
        {
            // fetch all tables
            var tables = DocumentElement.GetElementsByTagName("table:table");

            // if no name was given, return the first one
            if (tableName == null)
                return tables[0];
            
            // otherwise, try to find it
            for (int i = 0; i < tables.Count; i++)
            {
                var name = tables[i].Attributes["table:name"].Value;
                if (name == tableName)
                    return tables[i];
            }

            throw new TableNotFoundException(tableName);
        }

        // --------------------------------------------------------------------
        /// <summary>
        /// Loads all rows in the document
        /// </summary>
        void LoadRows(XmlNode tableRoot)
        {
            var rows = tableRoot.SelectNodes("./table:table-row", nsmgr);
            this.rows = new XmlNode[rows.Count];
            for (int i = 0; i < rows.Count; i++)
                this.rows[i] = rows[i];
        }
    }
}