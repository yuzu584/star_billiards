using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W���쐬
public class CreateStage : Singleton<CreateStage>
{
    [SerializeField] private StageData stageData;   // Inspector��StageData���w��

    private StageController stageCon;
    private GameObject stagePrefab;                 // �X�e�[�W�̃v���n�u

    private void Start()
    {
        stageCon = StageController.instance;
    }

    // �X�e�[�W���쐬
    public void Create()
    {
        // �X�e�[�W�̃C���X�^���X�𐶐��E���O����(clone)���폜�E�e��ݒ�
        stagePrefab = Instantiate(stageData.stageList[stageCon.stageNum].stagePrefab);
        stagePrefab.name = stageData.stageList[stageCon.stageNum].stagePrefab.name;
        stagePrefab.transform.SetParent(transform, false);
    }

    // �X�e�[�W�폜
    public void Destroy()
    {
        if(stagePrefab != null)
        {
            Destroy(stagePrefab);
        }
    }

    // �X�e�[�W��\��/��\��
    public void Render(bool draw)
    {
        // �X�e�[�W��\��/��\��
        if(stagePrefab != null)
        {
            stagePrefab.SetActive(draw);
        }
    }

    // ���݃X�e�[�W���`�悳��Ă��邩��Ԃ�
    public bool NowRenderState()
    {
        if(stagePrefab != null)
            return stagePrefab.activeSelf;
        else return false;
    }
}
