using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Q�[������UI���Ǘ�
public class InGameUI : MonoBehaviour
{
    public KeyGuide.KeyGuideIconAndTextType[] keyGuideTypes;

    private KeyGuideUI keyGuideUI;

    private void Start()
    {
        keyGuideUI = KeyGuideUI.instance;

        // �L�[����K�C�hUI��`��
        keyGuideUI.DrawGuide(keyGuideTypes);
    }
}
