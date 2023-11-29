using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W���쐬
public class CreateStage : MonoBehaviour
{
    [SerializeField] StageData stageData;             // Inspector��StageData���w��
    [SerializeField] StageController stageController; // Inspector��StageController���w��
    [SerializeField] GameObject sphere;               // �X�t�B�A

    private GameObject[] star;   // �P����GameObject
    private GameObject[] planet; // �f����GameObject

    // �X�e�[�W���쐬/�폜
    public void Create(bool draw)
    {
        // �`�悷��Ȃ�
        if (draw)
        {
            // �z���������
            star = new GameObject[stageData.stageList[stageController.stageNum].fixedStar.Length];
            planet = new GameObject[stageData.stageList[stageController.stageNum].planet.Length];

            // �P���̃C���X�^���X�𐶐��E���O����(clone)���폜�E�e��ݒ�
            for (int i = 0; i < stageData.stageList[stageController.stageNum].fixedStar.Length; i++)
            {
                star[i] = Instantiate(stageData.stageList[stageController.stageNum].fixedStar[i]);
                star[i].name = stageData.stageList[stageController.stageNum].fixedStar[i].name;
                star[i].transform.SetParent(this.transform, false);
            }

            // �f���̃C���X�^���X�𐶐��E���O����(clone)���폜�E�e��ݒ�
            for (int i = 0; i < stageData.stageList[stageController.stageNum].planet.Length; i++)
            {
                planet[i] = Instantiate(stageData.stageList[stageController.stageNum].planet[i]);
                planet[i].name = stageData.stageList[stageController.stageNum].planet[i].name;
                planet[i].transform.SetParent(this.transform, false);
            }
        }
        // �`�悵�Ȃ����C���X�^���X�������ς݂Ȃ�
        else if((!draw) && (star != null))
        {
            // �P���̃C���X�^���X���폜
            for (int i = 0; i < stageData.stageList[stageController.stageNum].fixedStar.Length; i++)
                Destroy(star[i]);

            // �f���̃C���X�^���X���폜
            for (int i = 0; i < stageData.stageList[stageController.stageNum].planet.Length; i++)
                Destroy(planet[i]);
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
