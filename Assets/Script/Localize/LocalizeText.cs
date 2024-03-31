using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// テキストの文字列を言語に応じて変える
public class LocalizeText : MonoBehaviour
{
    [SerializeField] private StringGroup group;
    [SerializeField] private StringEnumStruct type;
    [SerializeField] private Text text;

    private Localize localize;

    private void Start()
    {
        localize = Localize.instance;

        SetText();
    }

    void SetText()
    {
        // テキストを設定
        text.text = localize.GetString(group, type);

        // フォントを設定
        Font f = localize.GetFont();
        if(f != null)
            text.font = f;
    }
}
