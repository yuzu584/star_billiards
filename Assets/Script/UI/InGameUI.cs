using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム中のUIを管理
public class InGameUI : MonoBehaviour
{
    public KeyGuide.KeyGuideIconAndTextType[] keyGuideTypes;

    private KeyGuideUI keyGuideUI;

    private void Start()
    {
        keyGuideUI = KeyGuideUI.instance;

        // キー操作ガイドUIを描画
        keyGuideUI.DrawGuide(keyGuideTypes);
    }
}
