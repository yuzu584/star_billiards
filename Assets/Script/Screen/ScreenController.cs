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

    private InputController input;
    private FOV fov;
    private Lerp lerp;

    [System.NonSerialized] public ScreenType oldScreen = 0;             // �O��̉��
    [System.NonSerialized] public int oldScreenLoot = 0;                // �O��̊K�w
    [System.NonSerialized] public ScreenType oldFrameScreen = 0;        // 1�t���[���O�̉��
    [System.NonSerialized] public int oldFrameScreenLoot = 0;           // 1�t���[���O�̊K�w


    public enum ScreenType
    {
        Title,          // �^�C�g�����
        MainMenu,       // ���C�����j���[
        StageSelect,    // �X�e�[�W�I�����
        Options,        // �ݒ���
        SkillSelect,    // �X�L���I�����
        InGame,         // �Q�[�����
        Pause,          // �|�[�Y���
        PlanetInfo,     // �f�������
        StageClear,     // �X�e�[�W�N���A���
        GameOver,       // �Q�[���I�[�o�[���
    }

    [SerializeField] private ScreenType screen;     // ���
    [SerializeField] private int screenLoot;        // ��ʂ̊K�w
    public ScreenType Screen                        // ��ʂ̃v���p�e�B
    {
        get { return screen; }
        set { SwitchProcess(value); }
    }
    public int ScreenLoot                           // ��ʂ̊K�w�̃v���p�e�B
    {
        get { return screenLoot; }
        set { screenLoot = value; }
    }
    public delegate void ChangeScreen();            // ��ʂ��J�ڂ����Ƃ��̃f���Q�[�g
    public delegate void ChangeLoot();              // �K�w���J�ڂ����Ƃ��̃f���Q�[�g
    public ChangeScreen changeScreen;
    public ChangeScreen changeLoot;
    
    // ��ʑJ�ڏ���
    private void SwitchProcess(ScreenType scr)
    {
        // �J�ڐ�̉�ʂ܂��͌��݂̉�ʂ��J�ڎ��ɃA�j���[�V�������s�����ƂɂȂ��Ă�����
        if ((scrData.screenList[(int)scr].enterAnim) || (scrData.screenList[(int)screen].exitAnim))
        {
            // �A�j���[�V�������s��
            lerp.StopAll();

            StartCoroutine(SetScreen(scr, true));
        }
        else
        {
            StartCoroutine(SetScreen(scr, false));
        }
    }

    // ��ʂ�ݒ�
    private IEnumerator SetScreen(ScreenType scr, bool orPlay)
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

        // ��ʂ�ݒ�
        screen = scr;
        
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
        input = InputController.instance;
        fov = FOV.instance;

        lerp = gameObject.AddComponent<Lerp>();
        input.game_OnPauseDele += OpenPause;

        // UI_Negative���͎��̃C�x���g��o�^
        input.ui_OnNegativeDele += (float value) =>
        {
            // �K�w��0����Ȃ�1������
            if (ScreenLoot > 0)
                ScreenLoot -= (int)value;

            // ���C�����j���[�Ȃ�^�C�g����ʂɖ߂�
            else if (Screen == ScreenType.MainMenu)
            {
                Screen = ScreenType.Title;
            }
        };

        // ��ʑJ�ڎ��ɊK�w�����Z�b�g
        changeScreen += () =>
        {
            ScreenLoot = 0;
        };

        // ��ʑJ�ڐ悪����p�����Z�b�g�����ʂȂ王��p�����Z�b�g
        changeScreen += () =>
        {
            if (scrData.screenList[(int)Screen].resetFov)
                fov.ResetFOV();
        };
    }

    // �|�[�Y��ʂɑJ��
    void OpenPause(float value)
    {
        if (value > 0)
            screen = ScreenType.Pause;
    }
}
