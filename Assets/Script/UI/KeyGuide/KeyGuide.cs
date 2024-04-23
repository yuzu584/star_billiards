using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �L�[����̃K�C�h���Ǘ�
public class KeyGuide : MonoBehaviour
{
    [SerializeField] private KeyGuideIconSetter iconSetter;

    public Image[] image;
    public Text text;

    // �L�[����K�C�hUI�̃A�C�R���̎��
    public enum KeyGuideIconType
    {
        ui_positive,
        ui_negative,
        move,
        game_shot,
        game_acceleration_deceleration,
        game_use_skill,
        game_change_skill_left,
        game_change_skill_right,
    }

    // �L�[����K�C�hUI�̃e�L�X�g�̎��
    public enum KeyGuideTextType
    {
        decision,
        back,
        move_cursol,
        return_to_previous_screen,
        shot,
        acceleration_deceleration,
        use_skill,
        change_skill,
        increase_decrease_value,
        select_skill,
    }

    [System.Serializable]
    public struct KeyGuideIconAndTextType
    {
        public KeyGuideTextType text;
        public KeyGuideIconType[] icon;

        // �A�C�R���̗񋓌^�z�񂪓���������
        public bool CheckIconEquals(KeyGuideIconType[] _icon)
        {
            if(icon == null || _icon == null) return false;  // null �`�F�b�N
            if(icon.Length != _icon.Length) return false;    // �z��̒����`�F�b�N

            int count = 0;
            for(int i = 0; i < icon.Length; ++i)
            {
                // �����A�C�R���Ȃ�J�E���g
                if (icon[i] == _icon[i]) ++count;
            }

            return count >= icon.Length;
        }
    }

    private KeyGuideIconAndTextType iconAndText;
    public KeyGuideIconAndTextType IconAndText
    {
        get { return iconAndText; }
        set
        {
            iconAndText = value;
            iconSetter.SetIcon();
            iconSetter.SetText();
        }
    }

    // �摜�𕡐�
    public void DuplicateImage(KeyGuideIconAndTextType type)
    {
        // �z��̒����`�F�b�N
        if(type.icon.Length < 1) return;

        // 0 �Ԗڂ� Image �͕ۑ����Ă���
        Image save = image[0];
        image = new Image[type.icon.Length];
        image[0] = save;

        for (int i = 1; i < image.Length; ++i)
        {
            // �C���X�^���X����
            image[i] = Instantiate(image[0]);

            // �e�I�u�W�F�N�g��ݒ肵�Đ擪��
            image[i].transform.SetParent(transform, false);
            image[i].transform.SetAsFirstSibling();
        }

        // �摜��ݒ�
        iconAndText = type;
    }

    // ��\���ɂ���
    public void GuideEnabled(bool isEnabled)
    {
        // �e�L�X�g���\��
        text.enabled = isEnabled;

        // �A�C�R�����\��
        for (int i = 0; i < image.Length; ++i )
            image[i].enabled = isEnabled;
    }
}
