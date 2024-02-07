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

    // �X�e�[�W���쐬
    public void Create()
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

    // �X�e�[�W�폜
    public void Destroy()
    {
        if(star != null)
        {
            // �P���̃C���X�^���X���폜
            for (int i = 0; i < stageData.stageList[stageController.stageNum].fixedStar.Length; i++)
                Destroy(star[i]);
        }

        if(planet != null)
        {
            // �f���̃C���X�^���X���폜
            for (int i = 0; i < stageData.stageList[stageController.stageNum].planet.Length; i++)
                Destroy(planet[i]);
        }
    }

    // �X�e�[�W��\��/��\��
    public void Draw(bool draw)
    {
        // �P����\��/��\��
        for (int i = 0; i < stageData.stageList[stageController.stageNum].fixedStar.Length; i++)
        {
            if (star[i] != null)
                star[i].SetActive(draw);
        }

        // �f����\��/��\��
        for (int i = 0; i < stageData.stageList[stageController.stageNum].planet.Length; i++)
        {
            if (planet[i] != null)
                planet[i].SetActive(draw);
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
