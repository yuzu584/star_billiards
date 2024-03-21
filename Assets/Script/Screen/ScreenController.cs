using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;
using UnityEngine.UI;

// ��ʂ̎�ނ��Ǘ�
[DefaultExecutionOrder(-100)]
public class ScreenController : Singleton<ScreenController>
{
    [SerializeField] private ScreenData scrData;
    [SerializeField] private Image switchImage;                         // ��ʑJ�ڎ��̉摜

    private StageController stageCon;
    private InputController input;

    [System.NonSerialized] public int oldScreenNum = 0;                 // �O��̉�ʔԍ�
    [System.NonSerialized] public int oldScreenLoot = 0;                // �O��̊K�w
    [System.NonSerialized] public int oldFrameScreenNum = 0;            // 1�t���[���O�̉�ʔԍ�
    [System.NonSerialized] public int oldFrameScreenLoot = 0;           // 1�t���[���O�̊K�w
    private Lerp lerp;

    // UI���`��\�����Ǘ�����z��
    // 0 : �^�C�g�����
    // 1 : ���C�����j���[
    // 2 : �X�e�[�W�I�����
    // 3 : �ݒ���
    // 4 : �X�L���I�����
    // 5 : �Q�[�����
    // 6 : �|�[�Y���
    // 7 : 
    // 8 : �X�e�[�W�N���A���
    // 9 : �Q�[���I�[�o�[���

    [SerializeField] private int screenNum;  // ��ʔԍ�
    [SerializeField] private int screenLoot; // ��ʂ̊K�w
    public int ScreenNum                     // ��ʔԍ��̃v���p�e�B
    {
        get { return screenNum; }
        set { SwitchProcess(value); }
    }
    public int ScreenLoot                    // ��ʂ̊K�w�̃v���p�e�B
    {
        get { return screenLoot; }
        set { screenLoot = value; }
    }
    public delegate void ChangeScreen();     // ��ʂ��J�ڂ����Ƃ��̃f���Q�[�g
    public delegate void ChangeLoot();       // �K�w���J�ڂ����Ƃ��̃f���Q�[�g
    public ChangeScreen changeScreen;
    public ChangeScreen changeLoot;
    
    // ��ʑJ�ڏ���
    private void SwitchProcess(int num)
    {
        // �J�ڐ�̉�ʂ܂��͌��݂̉�ʂ��J�ڎ��ɃA�j���[�V�������s�����ƂɂȂ��Ă�����
        if ((scrData.screenList[num].enterAnim) || (scrData.screenList[screenNum].exitAnim))
        {
            // �A�j���[�V�������s��
            lerp.StopAll();

            StartCoroutine(SetScreenNum(num, true));
        }
        else
        {
            StartCoroutine(SetScreenNum(num, false));
        }
    }

    // ��ʔԍ���ݒ�
    private IEnumerator SetScreenNum(int num, bool orPlay)
    {
        if (orPlay)
        {
            // ��ʑJ�ڃA�j���[�V����
            input.DisableInputs();                           // InputSystem�̓��͂𖳌���
            Color c1 = new Color(0, 0, 0, 0);                // ����
            Color c2 = new Color(0, 0, 0, 1);                // ��
            switchImage.gameObject.SetActive(true);          // ��ʃT�C�Y��Image��L����
            switchImage.raycastTarget = true;                // ��ʃT�C�Y��Image�ɔ����t�^���ă}�E�X���͂��󂯕t���Ȃ��悤��
            yield return StartCoroutine(SwitchAnim(c1, c2)); // �A�j���[�V�������I���܂ő҂�

            // ��u�҂�
            yield return new WaitForSecondsRealtime(0.2f);
        }

        // ��ʔԍ���ݒ�
        screenNum = num;
        
        if(orPlay)
        {
            // ��ʑJ�ڃA�j���[�V����
            Color c1 = new Color(0, 0, 0, 0);                // ����
            Color c2 = new Color(0, 0, 0, 1);                // ��
            switchImage.raycastTarget = false;               // ��ʃT�C�Y��Image�ɔ���������ă}�E�X���͂��󂯕t����悤��
            yield return StartCoroutine(SwitchAnim(c2, c1)); // �A�j���[�V�������I���܂ő҂�
            switchImage.gameObject.SetActive(false);         // ��ʃT�C�Y��Image�𖳌���
        }
    }

    // ��ʑJ�ڎ��̃A�j���[�V����
    private IEnumerator SwitchAnim(Color c1, Color c2)
    {
        yield return StartCoroutine(lerp.Color_Image(switchImage, c1, c2, 0.5f));
    }

    void Start()
    {
        stageCon = StageController.instance;
        input = InputController.instance;

        lerp = gameObject.AddComponent<Lerp>();
        input.game_OnPauseDele += OpenPause;
        stageCon.stageCrearDele += () => { screenNum = 8; };

        // UI_Negative���͎��̃C�x���g��o�^
        input.ui_OnNegativeDele += (float value) =>
        {
            // �K�w��0����Ȃ�1������
            if (ScreenLoot > 0)
                ScreenLoot -= (int)value;

            // ���C�����j���[�Ȃ�^�C�g����ʂɖ߂�
            else if (ScreenNum == 1)
            {
                ScreenNum = 0;
            }
        };
    }

    // �|�[�Y��ʂɑJ��
    void OpenPause(float value)
    {
        if (value > 0)
            screenNum = 6;
    }
}
