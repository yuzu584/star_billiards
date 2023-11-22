using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W���쐬
public class CreateStage : MonoBehaviour
{
    [SerializeField] StageData stageData;             // Inspector��StageData���w��
    [SerializeField] StageController stageController; // Inspector��StageController���w��
    [SerializeField] GameObject sphere;               // �X�t�B�A

    private GameObject[] star;   // ����GameObject
    private GameObject[] planet; // �f����GameObject

    // �X�e�[�W���쐬
    public void Create()
    {
        // �z���������
        star = new GameObject[stageData.stageList[stageController.stageNum].fixedStar.Length];
        planet = new GameObject[stageData.stageList[stageController.stageNum].planet.Length];

        // �P���̃C���X�^���X�𐶐��E���O����(clone)���폜�E�e��ݒ�
        for(int i = 0; i < stageData.stageList[stageController.stageNum].fixedStar.Length; i++)
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

        // �X�t�B�A��\��
        sphere.SetActive(true);
    }

    // �X�e�[�W���폜
    public void Delete()
    {
        // �P���̃C���X�^���X���폜
        for (int i = 0; i < stageData.stageList[stageController.stageNum].fixedStar.Length; i++)
        {
            Destroy(star[i]);
            star[i] = null;
        }

        // �f���̃C���X�^���X���폜
        for (int i = 0; i < stageData.stageList[stageController.stageNum].planet.Length; i++)
        {
            Destroy(planet[i]);
            planet[i] = null;
        }

        // �X�t�B�A���\��
        sphere.SetActive(false);
    }

    void Start()
    {
        // �X�t�B�A���\��
        sphere.SetActive(false);
    }
}
