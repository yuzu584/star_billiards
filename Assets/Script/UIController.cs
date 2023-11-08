using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[������UI���Ǘ�
public class UIController : MonoBehaviour
{
    // Inspector��UI�̔z����w��
    [SerializeField] InGameUI inGameUI;
    [SerializeField] PauseUI pauseUI;

    // UI�̔z��
    [System.Serializable]
    public class InGameUI
    {
        public GameObject chargeUI;          // �`���[�W��UI
        public Image chargeCircle;           // �`���[�W�̉~
        public Text chargeValue;             // �`���[�W�̐��l
        public Text chargeName;              // �`���[�W�̕���
        public Image EnergyGauge;            // �G�l���M�[�Q�[�W
        public Image EnergyAfterImage;       // �G�l���M�[�Q�[�W�̌�����
        public Image EnergyGaugeOutline;     // �G�l���M�[�Q�[�W�̘g
        public Text EnergyValue;             // �G�l���M�[�̐��l
        public GameObject Message;           // ���b�Z�[�W
        public Text NoEnergy;                // �G�l���M�[���Ȃ��|��`����e�L�X�g
        public Text skillName;               // �X�L����
        public Image skillGauge;             // ���ʎ��ԂƃN�[���_�E���̃Q�[�W
        public GameObject planetInfo;        // �f�����UI
        public Image planetInfoRing;         // �f�����UI�̉~
        public LineRenderer planetInfoLine;  // �f�����UI�̐�
        public Text planetName;              // �f���̖��O
        public Image reticle;                // ���e�B�N��
    }

    [System.Serializable]
    public class PauseUI
    {
        public GameObject pauseUI; // �|�[�Y��ʂ�UI
    }

    public int popupAmount = 0;                // �|�b�v�A�b�v�̐�
    public bool[] drawingPopup = new bool[10]; // �|�b�v�A�b�v���`�悳��Ă��邩

    [SerializeField] private Shot shot;                                   // Shot�^�̕ϐ�
    [SerializeField] private EnergyController energyController;           // EnergyController�^�̕ϐ�
    [SerializeField] private ScreenController screenController;           // ScreenController�^�̕ϐ�
    [SerializeField] private PostProcessController postProcessController; // PostProcessController�^�̕ϐ�
    [SerializeField] private GameObject popUp;                            // �|�b�v�A�b�v�̃v���n�u

    RectTransform PIR = null; // �f�����UI�̉~�̃X�N���[�����W
    Vector3 PIL1;             // �f�����UI�̐��̎n�_���W
    Vector3 PIL2;             // �f�����UI�̐��̒��ԍ��W
    Vector3 PIL3;             // �f�����UI�̐��̏I�_���W

    // �G�l���M�[��UI��`��
    void DrawEnergyUI()
    {
        // �G�l���M�[�Q�[�W�̑�����`��
        inGameUI.EnergyGauge.fillAmount = energyController.energy / energyController.maxEnergy;

        if (inGameUI.EnergyAfterImage.fillAmount > energyController.energy / energyController.maxEnergy)
        {
            // �G�l���M�[�Q�[�W�̌����ʂ����������炷
            inGameUI.EnergyAfterImage.fillAmount -=
                (inGameUI.EnergyAfterImage.fillAmount - energyController.energy / energyController.maxEnergy) * Time.deltaTime;
        }

        // �G�l���M�[�̐��l��\��
        inGameUI.EnergyValue.text = energyController.energy.ToString("0");

        // �G�l���M�[��0�ȉ�����\���Ȃ�
        if ((energyController.energy <= 0) && (inGameUI.NoEnergy.enabled == false))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l��ԐF�ɂ���
            inGameUI.EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);
            inGameUI.EnergyValue.color = new Color32(155, 0, 0, 100);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g��\��
            inGameUI.NoEnergy.enabled = true;
        }
        // �G�l���M�[��0���ォ�\������Ă���Ȃ�
        else if ((energyController.energy > 0) && (inGameUI.NoEnergy.enabled == true))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l�𔒐F�ɂ���
            inGameUI.EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);
            inGameUI.EnergyValue.color = new Color32(255, 255, 255, 255);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
            inGameUI.NoEnergy.enabled = false;
        }
    }

    // �`���[�W��UI��`��
    void DrawChargeUI()
    {
        // �`���[�W����Ă���Ȃ�
        if (shot.charge > 0)
        {
            // �`���[�W��UI������������Ă�����
            if (!(inGameUI.chargeUI.activeSelf))
            {
                // UI��L����
                inGameUI.chargeUI.SetActive(true);
            }

            // �`���[�W�̐��l���e�L�X�g�ŕ\��
            inGameUI.chargeValue.text = shot.charge.ToString("0") + "%";

            // �`���[�W�̉~��`��
            inGameUI.chargeCircle.fillAmount = shot.charge / 100;
        }
        // �`���[�W����Ă��Ȃ����\������Ă���Ȃ�
        else if ((shot.charge == 0) && (inGameUI.chargeUI.activeSelf))
        {
            // UI�𖳌���
            inGameUI.chargeUI.SetActive(false);
        }
    }

    // �X�L����UI��`��
    public void DrawSkillUI(string skillName, float coolDown, float effectTime, float nowCoolDown, float nowEffectTime)
    {
        // �e�L�X�g�����݂̃X�L�����ɕύX
        inGameUI.skillName.text = skillName;

        // ���ʎ��Ԃ�`��
        if (nowEffectTime > 0)
            inGameUI.skillGauge.fillAmount = nowEffectTime / effectTime;
        // ���ʎ��Ԃ��o�߂��Ă����Ȃ�N�[���_�E����`��
        else if (nowCoolDown > 0)
            inGameUI.skillGauge.fillAmount = (coolDown - nowCoolDown) / coolDown;
    }

    // �f�����UI��`��
    public void DrawPlanetInfoUI(Vector3 position, string planetName)
    {
        // �Q�[����ʂȂ�UI��\��
        if(screenController.screenNum == 0)
        {
            // UI����\���Ȃ�\��
            if(!(inGameUI.planetInfo.activeSelf))
            {
                inGameUI.planetInfo.SetActive(true);
            }
            // �f�����UI�̉~�̃X�N���[�����W��ύX
            inGameUI.planetInfoRing.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);

            // �f�����UI�̐��̃X�N���[�����W�����[���h���W�ɕϊ�
            PIL1 = Camera.main.ScreenToWorldPoint(inGameUI.planetInfoRing.rectTransform.position + new Vector3(0, 0, 10));
            PIL2 = Camera.main.ScreenToWorldPoint(inGameUI.planetInfoRing.rectTransform.position + new Vector3(50, 50, 10));
            PIL3 = Camera.main.ScreenToWorldPoint(inGameUI.planetInfoRing.rectTransform.position + new Vector3(150, 50, 10));

            // ����`��
            inGameUI.planetInfoLine.SetPosition(0, PIL1);
            inGameUI.planetInfoLine.SetPosition(1, PIL2);
            inGameUI.planetInfoLine.SetPosition(2, PIL3);

            // �f���̖��O���e�L�X�g�ɐݒ�
            inGameUI.planetName.text = planetName;

            // �f���̖��OUI�̈ʒu��ݒ�
            inGameUI.planetName.rectTransform.position = inGameUI.planetInfoRing.rectTransform.position + new Vector3(160, 80, 10);
        }
        // �|�[�Y��ʂ���UI���\������Ă���Ȃ��\���ɂ���
        else if((screenController.screenNum == 1) && (inGameUI.planetInfo.activeSelf))
        {
            inGameUI.planetInfo.SetActive(false);
        }
    }

    // �|�[�Y��ʂ�UI��\�����͔�\���ɂ���
    public void DrawPauseUI(bool draw)
    {
        // �|�[�Y��ʂ�\�����͔�\��
        pauseUI.pauseUI.SetActive(draw);

        // ��ʊE�[�x��ONOFF�؂�ւ�
        postProcessController.DepthOfFieldOnOff(draw);

        // ���e�B�N����\�����͔�\��
        inGameUI.reticle.enabled = !(draw);
    }

    // �f�����j�󂳂ꂽ�|��`����|�b�v�A�b�v��`��
    public IEnumerator DrawDestroyPlanetPopup(string name)
    {
        float destroyTime = 10.0f; // �f����j�󂷂�܂ł̎���
        int i = 0;                 // ���𐔂���ϐ�

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
        popup.transform.SetParent(inGameUI.Message.transform, false);

        // �ʒu��ݒ�
        popup.transform.position += new Vector3(0, i * -40, 0);

        // �v���n�u�̃e�L�X�g���擾
        Text popupText = popup.transform.GetChild(1).GetComponent<Text>();

        // �v���n�u�̃e�L�X�g��ݒ�
        popupText.text = name + " was destroyed";

        // �|�b�v�A�b�v���폜
        Destroy(popup.gameObject, destroyTime);

        // �|�b�v�A�b�v��������܂ő҂�
        yield return new WaitForSeconds(destroyTime);

        // �|�b�v�A�b�v�J�E���g�����炷
        popupAmount--;

        // �`�悵�Ă��Ȃ���Ԃɂ���
        drawingPopup[i] = false;
    }

    void Start()
    {
        // �f�����UI�̉~��RectTransform���擾
        PIR = inGameUI.planetInfoRing.GetComponent<RectTransform>();

        // �f�����UI�̐��̎n�_�ƏI�_�̑������w��
        inGameUI.planetInfoLine.startWidth = 0.01f;
        inGameUI.planetInfoLine.endWidth = 0.01f;

        // �f�����UI�̐��̐�
        inGameUI.planetInfoLine.positionCount = 3;

        // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
        inGameUI.NoEnergy.enabled = false;

        // �|�[�Y��ʂ�UI���\��
        DrawPauseUI(false);

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
    }
}
