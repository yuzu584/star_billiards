using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// テキストの文字列を言語に応じて変える
public class LocalizeText : MonoBehaviour
{
    public string seet;
    public string dataName;
    public Text text;

    private Localize localize;

    private void Start()
    {
        localize = Localize.instance;

        SetText();

        localize.switchLanguageDele += SetText;
    }

    private void OnDestroy()
    {
        if(localize != null)
            localize.switchLanguageDele -= SetText;
    }

    void SetText()
    {
        // テキストを設定
        text.text = localize.GetString(seet, dataName);

        // フォントを設定
        Font f = localize.GetFont();
        if(f != null)
            text.font = f;
    }
}
