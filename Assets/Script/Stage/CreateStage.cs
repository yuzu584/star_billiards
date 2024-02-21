using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W���쐬
public class CreateStage : MonoBehaviour
{
    [SerializeField] StageData stageData;             // Inspector��StageData���w��
    [SerializeField] StageController stageController; // Inspector��StageController���w��
    [SerializeField] GameObject sphere;               // �X�t�B�A

    private GameObject stagePrefab; // �X�e�[�W�̃v���n�u

    // �X�e�[�W���쐬
    public void Create()
    {
        // �X�e�[�W�̃C���X�^���X�𐶐��E���O����(clone)���폜�E�e��ݒ�
        stagePrefab = Instantiate(stageData.stageList[stageController.stageNum].stagePrefab);
        stagePrefab.name = stageData.stageList[stageController.stageNum].stagePrefab.name;
        stagePrefab.transform.SetParent(this.transform, false);
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
    public void Draw(bool draw)
    {
        // �X�e�[�W��\��/��\��
        if(stagePrefab != null)
        {
            stagePrefab.SetActive(draw);
        }

        // �X�t�B�A��\��/��\���؂�ւ�
        sphere.SetActive(draw);
    }

    void Start()
    {
        // �X�t�B�A���\��
        sphere.SetActive(false);
    }
}
