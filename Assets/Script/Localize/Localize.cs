using Illogic.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム内の翻訳に関する処理
public class Localize : Singleton<Localize>
{
    [SerializeField] private Font font;

    // 翻訳テキストをまとめたODSファイル
    private ODSReader reader;

    public enum LanguageType
    {
        English,
        Japanese,
    }

    private LanguageType language;
    public LanguageType Language
    {
        get { return language; }
        set
        {
            language = value;
            switchLanguageDele?.Invoke();
        }
    }

    public event System.Action switchLanguageDele;

    private void Start()
    {
        // 翻訳テキストをまとめたODSファイルを読み込む
        reader = new ODSReader("localize_string_data.ods");

        // 初期言語は日本語
        Language = LanguageType.Japanese;
    }

    // 文字列を取得
    public string GetString(string seet, string name)
    {
        if (seet == "") return NotFindText();
        if (name == "") return NotFindText();

        // シート名を指定
        var seetData = new DataTable(reader, seet);

        // 取り出したいデータ名とid(言語番号)を指定
        var s = seetData[(int)Language][name];

        return s;
    }

    // フォントを取得
    public Font GetFont()
    {
        return font;
    }

    // 指定のテキストが見つからなかったとき
    public string NotFindText()
    {
        return "Could not find the text";
    }
}
