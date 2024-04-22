using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�N���[����`�悷��
public class DrawScreen : MonoBehaviour
{
    [SerializeField] private ScreenData scrData;
    [SerializeField] private GameObject parentObj;

    private ScreenController scrCon;
    private KeyGuideUI keyGuideUI;

    private GameObject scrIns;              // �X�N���[���̃C���X�^���X
    private GameObject backIns;             // �w�i�̃C���X�^���X

    private void Start()
    {
        scrCon = ScreenController.instance;
        keyGuideUI = KeyGuideUI.instance;

        // ��ʑJ�ڎ��ɃX�N���[����`��
        scrCon.changeScreen += Draw;

        Draw();
    }

    // �X�N���[����`��
    void Draw()
    {
        // �O��̃X�N���[���̃C���X�^���X���폜
        DestroyInstance(ref scrIns);

        // �C���X�^���X�𐶐�
        scrIns = Instantiate(scrData.screenList[(int)scrCon.Screen].screenObj);

        // �e�I�u�W�F�N�g��ݒ�
        scrIns.transform.SetParent(parentObj.transform, false);

        // �e�I�u�W�F�N�g�̒��Ő擪�ɂ���
        scrIns.transform.SetAsFirstSibling();

        // �w�i��`�悷���ʂȂ�`��
        if (scrData.screenList[(int)scrCon.Screen].drawBackGround)
        {
            DrawBackGround();
        }
        // �`�悵�Ȃ��Ȃ�C���X�^���X���폜
        else
        {
            DestroyInstance(ref backIns);
        }
    }

    // �w�i��`��
    void DrawBackGround()
    {
        // �O��̔w�i�̃C���X�^���X���폜
        DestroyInstance(ref backIns);

        // �C���X�^���X�𐶐�
        backIns = Instantiate(scrData.background);

        // �e�I�u�W�F�N�g��ݒ�
        backIns.transform.SetParent(parentObj.transform, false);

        // �e�I�u�W�F�N�g�̒��Ő擪�ɂ���
        backIns.transform.SetAsFirstSibling();
    }

    // �C���X�^���X���폜���� null ��������(GameObject �͎Q�Ƃ�n��)
    void DestroyInstance(ref GameObject obj)
    {
        if (obj)
        {
            Destroy(obj);
            obj = null;
        }
    }
}
