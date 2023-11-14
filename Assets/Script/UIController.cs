using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[������UI���Ǘ�
public class UIController : MonoBehaviour
{
    // Inspector��UI�̔z����w��
    public ChargeUI chargeUI;
    public EnergyUI energyUI;
    public MessageUI messageUI;
    public SkillUI skillUI;
    public PlanetInfoUI planetInfoUI;
    public MissionUI missionUI;
    public PauseUI pauseUI;
    public StageClearUI stageClearUI;
    public OtherUI otherUI;

    // �`���[�W��UI
    [System.Serializable]
    public class ChargeUI
    {
        public GameObject allChargeUI;       // �S�Ẵ`���[�W��UI
        public Image chargeCircle;           // �`���[�W�̉~
        public Text chargeValue;             // �`���[�W�̐��l
        public Text chargeName;              // �`���[�W�̕���
    }

    // �G�l���M�[��UI
    [System.Serializable]
    public class EnergyUI
    {
        public Image EnergyGauge;            // �G�l���M�[�Q�[�W
        public Image EnergyAfterImage;       // �G�l���M�[�Q�[�W�̌�����
        public Image EnergyGaugeOutline;     // �G�l���M�[�Q�[�W�̘g
        public Text EnergyValue;             // �G�l���M�[�̐��l
    }

    // ���b�Z�[�W��UI
    [System.Serializable]
    public class MessageUI
    {
        public GameObject Message;           // ���b�Z�[�W
        public Text NoEnergy;                // �G�l���M�[���Ȃ��|��`����e�L�X�g
    }

    // �X�L����UI
    [System.Serializable]
    public class SkillUI
    {
        public Text skillName;               // �X�L����
        public Image skillGauge;             // ���ʎ��ԂƃN�[���_�E���̃Q�[�W
    }

    // �f�����UI
    [System.Serializable]
    public class PlanetInfoUI
    {
        public GameObject allPlanetInfo;     // �S�Ă̘f�����UI
        public Image targetRing;             // �f�����UI�̉~
        public LineRenderer planetInfoLine;  // �f�����UI�̐�
        public Text planetName;              // �f���̖��O
    }

    // �~�b�V������UI
    [System.Serializable]
    public class MissionUI
    {
        public Text missionText;             // �~�b�V�����̃e�L�X�g
    }

    // �|�[�Y��ʂ�UI
    [System.Serializable]
    public class PauseUI
    {
        public GameObject allPauseUI;        // �|�[�Y��ʑS�̂�UI
    }

    // �X�e�[�W�N���A��ʂ�UI
    [System.Serializable]
    public class StageClearUI
    {
        public GameObject allStageClearUI;   // �X�e�[�W�N���A��ʑS�̂�UI
        public Text stageClearText;          // �X�e�[�W�N���A��ʂ̃e�L�X�g
        public GameObject[] button;          // �X�e�[�W�N���A��ʂ̃{�^��
        public Material stageClearButtonMat; // �{�^���̃}�e���A��
    }

    // ���̑�UI
    [System.Serializable]
    public class OtherUI
    {
        public Image reticle;                // ���e�B�N��
        public Text speedValue;              // �ړ����x��UI
    }

    [System.NonSerialized] public int popupAmount = 0;                // �|�b�v�A�b�v�̐�
    [System.NonSerialized] public bool[] drawingPopup = new bool[10]; // �|�b�v�A�b�v���`�悳��Ă��邩

    [SerializeField] private Shot shot;                                   // Inspector��Shot���w��
    [SerializeField] private EnergyController energyController;           // Inspector��EnergyController���w��
    [SerializeField] private ScreenController screenController;           // Inspector��ScreenController���w��
    [SerializeField] private PostProcessController postProcessController; // Inspector��PostProcessController���w��
    [SerializeField] private StageData stageData;                         // Inspector��StageData���w��
    [SerializeField] private StageController stageController;             // Inspector��StageController���w��
    [SerializeField] private DestroyPlanet destroyPlanet;                 // Inspector��DestroyPlanet���w��
    [SerializeField] private GameObject popUp;                            // �|�b�v�A�b�v�̃v���n�u
    [SerializeField] private Rigidbody rb;                                // �v���C���[��Rigidbody

    RectTransform PIR = null; // �f�����UI�̉~�̃X�N���[�����W
    Vector3 PIL1;             // �f�����UI�̐��̎n�_���W
    Vector3 PIL2;             // �f�����UI�̐��̒��ԍ��W
    Vector3 PIL3;             // �f�����UI�̐��̏I�_���W

    private bool drawedStageClearUI = false; // �X�e�[�W�N���A��ʂ��`�悳�ꂽ��

    // �G�l���M�[��UI��`��
    void DrawEnergyUI()
    {
        // �G�l���M�[�Q�[�W�̑�����`��
        energyUI.EnergyGauge.fillAmount = energyController.energy / energyController.maxEnergy;

        if (energyUI.EnergyAfterImage.fillAmount > energyController.energy / energyController.maxEnergy)
        {
            // �G�l���M�[�Q�[�W�̌����ʂ����������炷
            energyUI.EnergyAfterImage.fillAmount -=
                (energyUI.EnergyAfterImage.fillAmount - energyController.energy / energyController.maxEnergy) * Time.deltaTime;
        }

        // �G�l���M�[�̐��l��\��
        energyUI.EnergyValue.text = energyController.energy.ToString("0");

        // �G�l���M�[��0�ȉ�����\���Ȃ�
        if ((energyController.energy <= 0) && (messageUI.NoEnergy.enabled == false))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l��ԐF�ɂ���
            energyUI.EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);
            energyUI.EnergyValue.color = new Color32(155, 0, 0, 100);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g��\��
            messageUI.NoEnergy.enabled = true;
        }
        // �G�l���M�[��0���ォ�\������Ă���Ȃ�
        else if ((energyController.energy > 0) && (messageUI.NoEnergy.enabled == true))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l�𔒐F�ɂ���
            energyUI.EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);
            energyUI.EnergyValue.color = new Color32(255, 255, 255, 255);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
            messageUI.NoEnergy.enabled = false;
        }
    }

    // �`���[�W��UI��`��
    void DrawChargeUI()
    {
        // �`���[�W����Ă���Ȃ�
        if (shot.charge > 0)
        {
            // �`���[�W��UI������������Ă�����
            if (!(chargeUI.allChargeUI.activeSelf))
            {
                // UI��L����
                chargeUI.allChargeUI.SetActive(true);
            }

            // �`���[�W�̐��l���e�L�X�g�ŕ\��
            chargeUI.chargeValue.text = shot.charge.ToString("0") + "%";

            // �`���[�W�̉~��`��
            chargeUI.chargeCircle.fillAmount = shot.charge / 100;
        }
        // �`���[�W����Ă��Ȃ����\������Ă���Ȃ�
        else if ((shot.charge == 0) && (chargeUI.allChargeUI.activeSelf))
        {
            // UI�𖳌���
            chargeUI.allChargeUI.SetActive(false);
        }
    }

    // �X�L����UI��`��
    public void DrawSkillUI(string skillName, float coolDown, float effectTime, float nowCoolDown, float nowEffectTime)
    {
        // �e�L�X�g�����݂̃X�L�����ɕύX
        skillUI.skillName.text = skillName;

        // ���ʎ��Ԃ�`��
        if (nowEffectTime > 0)
            skillUI.skillGauge.fillAmount = nowEffectTime / effectTime;
        // ���ʎ��Ԃ��o�߂��Ă����Ȃ�N�[���_�E����`��
        else if (nowCoolDown > 0)
            skillUI.skillGauge.fillAmount = (coolDown - nowCoolDown) / coolDown;
    }

    // �f�����UI��`��
    public void DrawPlanetInfoUI(Vector3 position, string planetName)
    {
        // �Q�[����ʂ��Ώۂ�Sphere�ȊO�Ȃ�UI��\��
        if((screenController.screenNum == 0) && (!(planetName == "Sphere")))
        {
            // UI����\���Ȃ�\��
            if(!(planetInfoUI.allPlanetInfo.activeSelf))
            {
                planetInfoUI.allPlanetInfo.SetActive(true);
            }
            // �f�����UI�̉~�̃X�N���[�����W��ύX
            planetInfoUI.targetRing.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);

            // �f�����UI�̐��̃X�N���[�����W�����[���h���W�ɕϊ�
            PIL1 = Camera.main.ScreenToWorldPoint(planetInfoUI.targetRing.rectTransform.position + new Vector3(0, 0, 10));
            PIL2 = Camera.main.ScreenToWorldPoint(planetInfoUI.targetRing.rectTransform.position + new Vector3(50, 50, 10));
            PIL3 = Camera.main.ScreenToWorldPoint(planetInfoUI.targetRing.rectTransform.position + new Vector3(150, 50, 10));

            // ����`��
            planetInfoUI.planetInfoLine.SetPosition(0, PIL1);
            planetInfoUI.planetInfoLine.SetPosition(1, PIL2);
            planetInfoUI.planetInfoLine.SetPosition(2, PIL3);

            // �f���̖��O���e�L�X�g�ɐݒ�
            planetInfoUI.planetName.text = planetName;

            // �f���̖��OUI�̈ʒu��ݒ�
            planetInfoUI.planetName.rectTransform.position = planetInfoUI.targetRing.rectTransform.position + new Vector3(160, 80, 10);
        }
        // �Ώۂ�Sphere�Ȃ��\���ɂ���
        else if(planetName == "Sphere")
        {
            planetInfoUI.allPlanetInfo.SetActive(false);
        }
        // �|�[�Y��ʂ���UI���\������Ă���Ȃ��\���ɂ���
        else if((screenController.screenNum == 1) && (planetInfoUI.allPlanetInfo.activeSelf))
        {
            planetInfoUI.allPlanetInfo.SetActive(false);
        }
    }

    // �|�[�Y��ʂ�UI��\�����͔�\���ɂ���
    public void DrawPauseUI(bool draw)
    {
        // �|�[�Y��ʂ�\�����͔�\��
        pauseUI.allPauseUI.SetActive(draw);

        // ��ʊE�[�x��ONOFF�؂�ւ�
        postProcessController.DepthOfFieldOnOff(draw);

        // ���e�B�N����\�����͔�\��
        otherUI.reticle.enabled = !(draw);
    }

    // �|�b�v�A�b�v�𓮂���
    public IEnumerator MovePopup(float time, float fadeTime, GameObject popup, float moveDistance, Vector3 defaultPosition, int i)
    {
        // �w�肵�����Ԃ��o�߂���܂ŌJ��Ԃ�
        while (time < fadeTime)
        {
            // ���Ԃ��J�E���g
            time += Time.deltaTime;

            // �i�݋���v�Z
            float t = time / fadeTime;

            // �|�b�v�A�b�v���ړ�
            popup.transform.position = Vector3.Lerp(defaultPosition, defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), t);

            // 1�t���[���҂�
            yield return null;
        }

        if(moveDistance < 0)
        {
            // �`�悵�Ă��Ȃ���Ԃɂ���
            drawingPopup[i] = false;
        }
    }

    // �|�b�v�A�b�v��`��
    public IEnumerator DrawDestroyPlanetPopup(string text)
    {
        float destroyTime = 10.0f;   // �f����j�󂷂�܂ł̎���
        int i = 0;                   // ���𐔂���ϐ�
        float fadeTime = 0.2f;       // �t�F�[�h����
        float moveDistance = 300.0f; // �ړ�����
        Vector3 defaultPosition;     // �f�t�H���g�̈ʒu

        // false��������܂ŌJ��Ԃ�
        while ((drawingPopup[i]))
        {
            // �z��͈̔͊O�Ȃ�R���[�`���I��
            if (i > drawingPopup.Length)
                yield break;
            i++;
        }

        // �`��ς݂ɂ���
        drawingPopup[i] = true;

        // �|�b�v�A�b�v�̃C���X�^���X�𐶐�
        GameObject popup = Instantiate(popUp);

        // �e��ݒ�
        popup.transform.SetParent(messageUI.Message.transform, false);

        // �ʒu��ݒ�
        popup.transform.position += new Vector3(-moveDistance, i * -40.0f, 0.0f);

        // �v���n�u�̃e�L�X�g���擾
        Text popupText = popup.transform.GetChild(1).GetComponent<Text>();

        // �v���n�u�̃e�L�X�g��ݒ�
        popupText.text = text;

        // �o�ߎ��Ԃ��J�E���g
        float time = 0;

        // �f�t�H���g�ʒu��ݒ�
        defaultPosition = popup.transform.position;

        // �|�b�v�A�b�v�𓮂���
        StartCoroutine(MovePopup(time, fadeTime, popup, moveDistance, defaultPosition, i));

        // �|�b�v�A�b�v�����Ԃ��o�߂���܂ő҂�
        yield return new WaitForSeconds(destroyTime);

        // �|�b�v�A�b�v�𓮂���
        StartCoroutine(MovePopup(time, fadeTime, popup, -moveDistance, defaultPosition, i));

        yield return new WaitUntil(() => !(drawingPopup[i]));

        // �|�b�v�A�b�v���폜
        Destroy(popup.gameObject);

        // �|�b�v�A�b�v�J�E���g�����炷
        popupAmount--;
    }

    // �~�b�V������UI��`��
    public void DrawMissionUI()
    {
        // �~�b�V�����ԍ�����
        int missionNum = stageData.stageList[stageController.stageNum].missionNum;

        // �X�e�[�W�̏����f��������
        int planetAmount = stageData.stageList[stageController.stageNum].planetAmount;

        // �~�b�V�����ԍ��ɂ���ĕ���
        switch (missionNum)
        {
            case 0: // �S�Ă̘f����j��

                // �~�b�V�����̃e�L�X�g��ݒ�
                missionUI.missionText.text = "Destroy all planets " + destroyPlanet.planetDestroyAmount + " / " + planetAmount;
                break;
            case 1: // ���ԓ��ɃS�[���ɂ��ǂ蒅��

                // �~�b�V�����̃e�L�X�g��ݒ�
                missionUI.missionText.text = "Reach the goal";
                break;
            default:
                break;
        }
    }

    // �ړ����x�̐��l��`��
    void DrawSpeedValue()
    {
        // �|�[�Y��ʂ���UI���\������Ă���Ȃ��\���ɂ���
        if ((screenController.screenNum == 1) && (otherUI.speedValue.enabled == true))
        {
            otherUI.speedValue.enabled = false;
        }
        // �Q�[����ʂȂ�
        else if (screenController.screenNum == 0)
        {
            // ��\���Ȃ�\��
            if (otherUI.speedValue.enabled == false)
            {
                otherUI.speedValue.enabled = true;
            }

            // ���x�̃e�L�X�g���X�V
            otherUI.speedValue.text = rb.velocity.magnitude.ToString("0") + " km/s";
        }
    }

    // �X�e�[�W�N���A��ʂ�UI��`��
    void DrawStageClearUI(bool draw)
    {
        // �X�e�[�W�N���A��ʂ�\��
        stageClearUI.allStageClearUI.SetActive(draw);

        // �{�^�����\��
        for (int i = 0; i < stageClearUI.button.Length; i++)
            stageClearUI.button[i].SetActive(false);

        // �X�e�[�W�N���A��ʂ𓮂���
        StartCoroutine(MoveStageClearUI());
    }

    // �X�e�[�W�N���A��ʂ𓮂���
    IEnumerator MoveStageClearUI()
    {
        // �e�L�X�g�𓮂���
        StartCoroutine(MoveStageClearText());

        // ��u�҂�
        yield return new WaitForSeconds(1.0f);

        // �{�^���𓮂���
        StartCoroutine(MoveStageClearButton());
    }

    // �X�e�[�W�N���A��ʂ̃e�L�X�g�𓮂���
    IEnumerator MoveStageClearText()
    {
        float time = 0;        // �o�ߎ���
        float fadeTime = 0.5f; // �t�F�[�h����

        // �w�肵�����Ԃ��o�߂���܂ŌJ��Ԃ�
        while (time < fadeTime)
        {
            // ���Ԃ��J�E���g
            time += Time.unscaledDeltaTime;

            // �i�݋���v�Z
            float t = time / fadeTime;

            // �e�L�X�g�ړ��E�����x�ω�
            stageClearUI.stageClearText.transform.localPosition = Vector3.Lerp(new Vector3(300.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), t);
            stageClearUI.stageClearText.color = Color32.Lerp(new Color32(255, 255, 255, 0), new Color32(255, 255, 255, 255), t);

            // 1�t���[���҂�
            yield return null;
        }
    }

    // �X�e�[�W�N���A��ʂ̃{�^���𓮂���
    IEnumerator MoveStageClearButton()
    {
        float time = 0;        // �o�ߎ���
        float fadeTime = 0.5f; // �t�F�[�h����

        // �����ʒu��ۑ�
        Vector3[] defaultPosition = new Vector3[stageClearUI.button.Length];
        for (int i = 0; i < stageClearUI.button.Length; i++)
            defaultPosition[i] = stageClearUI.button[i].transform.position;

        // �{�^����\��
        for (int i = 0; i < stageClearUI.button.Length; i++)
            stageClearUI.button[i].SetActive(true);

        // �w�肵�����Ԃ��o�߂���܂ŌJ��Ԃ�
        while (time < fadeTime)
        {
            // ���Ԃ��J�E���g
            time += Time.unscaledDeltaTime;

            // �i�݋���v�Z
            float t = time / fadeTime;

            // �{�^���ړ��E�����x�ω�
            for (int i = 0; i < stageClearUI.button.Length; i++)
            {
                stageClearUI.button[i].transform.position = Vector3.Lerp(defaultPosition[i] - new Vector3(0.0f, 50.0f, 0.0f), defaultPosition[i], t);
                stageClearUI.stageClearButtonMat.color = Color32.Lerp(new Color32(255, 255, 255, 0), new Color32(255, 255, 255, 255), t);
            }

            // 1�t���[���҂�
            yield return null;
        }
    }

    void Start()
    {
        // �f�����UI�̉~��RectTransform���擾
        PIR = planetInfoUI.targetRing.GetComponent<RectTransform>();

        // �f�����UI�̐��̎n�_�ƏI�_�̑������w��
        planetInfoUI.planetInfoLine.startWidth = 0.01f;
        planetInfoUI.planetInfoLine.endWidth = 0.01f;

        // �f�����UI�̐��̐�
        planetInfoUI.planetInfoLine.positionCount = 3;

        // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
        messageUI.NoEnergy.enabled = false;

        // �|�[�Y��ʂ�UI���\��
        DrawPauseUI(false);

        // �X�e�[�W�N���A��ʂ�UI���\��
        DrawStageClearUI(false);

        // �|�b�v�A�b�v���`�悳��Ă��邩���Ǘ�����ϐ���������
        for (int i = 0; i > drawingPopup.Length; i++)
        {
            drawingPopup[i] = false;
        }
    }

    void Update()
    {
        // �G�l���M�[��UI��`��
        DrawEnergyUI();

        // �`���[�W��UI��`��
        DrawChargeUI();

        // �~�b�V������UI��`��
        DrawMissionUI();

        // �ړ����x�̐��l��`��
        DrawSpeedValue();

        // �X�e�[�W���N���A��������UI���`�悳��Ă��Ȃ��Ȃ�
        if ((stageController.stageCrear) && (!(drawedStageClearUI)))
        {
            // �X�e�[�W�N���A��ʂ�`��
            drawedStageClearUI = true;
            DrawStageClearUI(true);
        }
    }
}
