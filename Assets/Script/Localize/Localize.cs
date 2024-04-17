using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム内の翻訳に関する処理
public class Localize : Singleton<Localize>
{
    // 言語ごとの情報が入った ScriptableObject
    [SerializeField] private LanguageData languageData;

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
        // 初期言語は日本語
        Language = LanguageType.Japanese;
    }

    // 文字列を取得
    public string GetString()
    {
        return "制作中";
    }

    // 言語ごとのフォントを取得
    public Font GetFont()
    {
        // 言語ごとのフォントの数繰り返す
        for(int i = 0; i < languageData.fonts.Length; i++)
        {
            // 現在の言語が見つかったら
            if (languageData.fonts[i].language == Language)
            {
                // 現在の言語のフォントを返す
                return languageData.fonts[i].font;
            }
        }

        return null;
    }
}
