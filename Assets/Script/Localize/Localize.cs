using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム内の翻訳に関する処理
public class Localize : Singleton<Localize>
{
    [SerializeField] private StringTextData stringTextData;     // 各言語のテキストが入った ScriptableObject

    [SerializeField] private LanguageType language;

    // 文字列を取得
    public string GetString(StringGroup group, StringType type)
    {
        // 文字列のグループの数繰り返す
        for (int i = 0; i < stringTextData.strings.Length; ++i)
        {
            // グループが見つかったら
            if (stringTextData.strings[i].group == group)
            {
                // グループごとのテキストの数繰り返す
                for (int j = 0; j < stringTextData.strings[i].stringData.Length; j++)
                {
                    // 指定の StringType と一致したら
                    if (stringTextData.strings[i].stringData[j].type == type)
                    {
                        // 指定の StringType と一致したテキストを返す
                        return stringTextData.strings[i].stringData[j].text[(int)language];
                    }
                }
            }
        }

        // 見つからなかったら null を文字列で返す
        return "null";
    }

    // 言語ごとのフォントを取得
    public Font GetFont()
    {
        // 言語ごとのフォントの数繰り返す
        for(int i = 0; i < stringTextData.fonts.Length; i++)
        {
            // 現在の言語が見つかったら
            if (stringTextData.fonts[i].language == language)
            {
                // 現在の言語のフォントを返す
                return stringTextData.fonts[i].font;
            }
        }

        return null;
    }
}
