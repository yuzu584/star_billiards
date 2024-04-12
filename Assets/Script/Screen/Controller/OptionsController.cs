using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Button;

// �ݒ��ʂ��Ǘ�
public class OptionsController : Singleton<OptionsController>
{
    public OptionsUIController opUICon;
    private ScreenController scrCon;
    private InputController input;

    private void Start()
    {
        input = InputController.instance;
        scrCon = ScreenController.instance;
    }

    // �\������K�w��؂�ւ�
    public void SwitchLoot(int loot)
    {
        if (opUICon == null) return;

        // �K�w��ݒ�
        scrCon.ScreenLoot = loot;
    }

    // �߂�{�^���̃N���b�N���̓����ύX
    public void SetBuckButtonAction(BackButton backBtn)
    {
        backBtn.action = () =>
        {
            if (scrCon.ScreenLoot == 0)
            {
                // �K�w��0�ȉ����I�u�W�F�N�g���L���Ȃ�
                if ((scrCon.ScreenLoot <= 0) && (backBtn.gameObject.activeInHierarchy))
                {
                    //�����Đ�
                    backBtn.PlayBtnSound(BtnSounds.ClickSound);

                    // �O�̉�ʂɖ߂�
                    scrCon.Screen = backBtn.oldScreen;
                }
            }
            else if (scrCon.ScreenLoot > 0)
            {
                SwitchLoot(0);
            }
        };
    }
}
