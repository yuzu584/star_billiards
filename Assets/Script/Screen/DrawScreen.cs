using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�N���[����`�悷��
public class DrawScreen : MonoBehaviour
{
    [SerializeField] private ScreenData scrData;
    [SerializeField] private GameObject parentObj;

    private ScreenController scrCon;
    private GameObject Ins;                // �X�N���[���̃C���X�^���X

    private void Start()
    {
        scrCon = ScreenController.instance;

        // ��ʑJ�ڎ��ɃX�N���[����`��
        scrCon.changeScreen += Draw;

        Draw();
    }

    // �X�N���[����`��
    void Draw()
    {
        // �O��̃X�N���[���̃C���X�^���X���폜
        if (Ins)
        {
            Destroy(Ins);
            Ins = null;
        }

        // �X�N���[���̃C���X�^���X�𐶐�
        Ins = Instantiate(scrData.screenList[(int)scrCon.Screen].screenObj);

        // �X�N���[���̐e�I�u�W�F�N�g��ݒ�
        Ins.transform.SetParent(parentObj.transform, false);

        // �e�I�u�W�F�N�g�̒��Ő擪�ɂ���
        Ins.transform.SetAsFirstSibling();
    }
}
