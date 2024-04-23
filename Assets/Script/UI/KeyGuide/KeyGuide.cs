using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// キー操作のガイドを管理
public class KeyGuide : MonoBehaviour
{
    [SerializeField] private KeyGuideIconSetter iconSetter;

    public Image[] image;
    public Text text;

    // キー操作ガイドUIのアイコンの種類
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

    // キー操作ガイドUIのテキストの種類
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

        // アイコンの列挙型配列が同じか判定
        public bool CheckIconEquals(KeyGuideIconType[] _icon)
        {
            if(icon == null || _icon == null) return false;  // null チェック
            if(icon.Length != _icon.Length) return false;    // 配列の長さチェック

            int count = 0;
            for(int i = 0; i < icon.Length; ++i)
            {
                // 同じアイコンならカウント
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

    // 画像を複製
    public void DuplicateImage(KeyGuideIconAndTextType type)
    {
        // 配列の長さチェック
        if(type.icon.Length < 1) return;

        // 0 番目の Image は保存しておく
        Image save = image[0];
        image = new Image[type.icon.Length];
        image[0] = save;

        for (int i = 1; i < image.Length; ++i)
        {
            // インスタンス生成
            image[i] = Instantiate(image[0]);

            // 親オブジェクトを設定して先頭に
            image[i].transform.SetParent(transform, false);
            image[i].transform.SetAsFirstSibling();
        }

        // 画像を設定
        iconAndText = type;
    }

    // 非表示にする
    public void GuideEnabled(bool isEnabled)
    {
        // テキストを非表示
        text.enabled = isEnabled;

        // アイコンを非表示
        for (int i = 0; i < image.Length; ++i )
            image[i].enabled = isEnabled;
    }
}
