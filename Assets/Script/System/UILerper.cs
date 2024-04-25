using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// UI�̐��`��Ԃ��s��
public class UILerper : MonoBehaviour
{
    private Lerp lerp;

    // ���`�⊮�� "�F" ��ύX���鎞�Ɏg�p����\����
    [System.Serializable]
    public struct LerpColor
    {
        public bool use;                                    // ���`�⊮�ŐF��ύX���邩
        [System.NonSerialized]
        public Color defaultColor;                          // �����F
        public Color changedColor;                          // �ω���̐F
        public float fadeTime;                              // �t�F�[�h����
    }

    // ���`�⊮�� "���W" ��ύX���鎞�Ɏg�p����\����
    [System.Serializable]
    public struct LerpPosition
    {
        public bool use;                                    // ���`�⊮�ō��W��ύX���邩
        [System.NonSerialized]
        public Vector3 defaultPos;                          // �������W
        public Vector3 posFluctuation;                      // ���W�ω���
        public float fadeTime;                              // �t�F�[�h����
    }

    // ���`�⊮�� "�X�P�[��" ��ύX���鎞�Ɏg�p����\����
    [System.Serializable]
    public struct LerpScale
    {
        public bool use;                                    // ���`�⊮�ŃX�P�[����ύX���邩
        [System.NonSerialized] public Vector2 defaultScale; // �����X�P�[��
        public Vector2 scaleFluctuation;                    // �X�P�[���ω���
        public float fadeTime;                              // �t�F�[�h����
    }

    // Inspector �ɕ\��������`�⊮�p�N���X
    [System.Serializable]
    public class LerpStruct
    {
        public UIBehaviour ui;                              // ���`��Ԃ��s��UI
        public LerpColor color;                             // ���`�⊮�� "�F" ��ύX���鎞�Ɏg�p����\����
        public LerpPosition position;                       // ���`�⊮�� "���W" ��ύX���鎞�Ɏg�p����\����
        public LerpScale scale;                             // ���`�⊮�� "�X�P�[��" ��ύX���鎞�Ɏg�p����\����
        public bool onPointerDraw;                          // �|�C���^�[������Ă���Ƃ��̂ݕ`�悷�邩
        public Type uiType;                                 // ui �̌^

        // �������֐�
        public void Init()
        {
            // UI�̌^���擾���đ��
            uiType = ui.GetType();

            // �����F�E�������W�E�����X�P�[�����擾
            if(uiType == typeof(Image))
            {
                Image image = (Image)ui;
                color.defaultColor = image.color;
                position.defaultPos = image.rectTransform.localPosition;
                scale.defaultScale = image.transform.localScale;
            }
            else if (uiType == typeof(Text))
            {
                Text text = (Text)ui;
                color.defaultColor = text.color;
                position.defaultPos = text.rectTransform.localPosition;
                scale.defaultScale = text.transform.localScale;
            }
        }
    }

    public LerpStruct[] lerpStructs;

    private void Awake()
    {
        lerp = gameObject.AddComponent<Lerp>();

        // �N���X�z��̏�����
        for (int i = 0; i < lerpStructs.Length; i++)
            lerpStructs[i].Init();
    }

    // �A�j���[�V��������
    public void AnimProcess(bool orEnter)
    {
        // Lerp �R���|�[�l���g�� null �Ȃ�擾
        lerp ??= gameObject.AddComponent<Lerp>();

        lerp.StopAll();

        // �|�C���^�[������Ă���Ƃ��̂ݕ`�悷��Ȃ�`��
        for (int i = 0; i < lerpStructs.Length; i++)
        {
            if ((lerpStructs[i].onPointerDraw) && (lerpStructs[i].ui.enabled != orEnter))
                lerpStructs[i].ui.enabled = orEnter;
        }

        // �A�j���[�V��������
        void Anim(LerpStruct lerpStructs)
        {
            Color c1, c2;                                                               // �J�n���ƏI�����̐F
            c1 = lerpStructs.color.defaultColor;                                        // �����F
            c2 = lerpStructs.color.changedColor;                                        // �ω���̐F

            Vector3 p1, p2;                                                             // �J�n���ƏI�����̍��W
            p1 = lerpStructs.position.defaultPos;                                       // �������W
            p2 = lerpStructs.position.defaultPos + lerpStructs.position.posFluctuation; // �������W + ���W�ω���

            Vector2 s1, s2;                                                             // �J�n���ƏI�����̃X�P�[��
            s1 = lerpStructs.scale.defaultScale;                                        // �����X�P�[��
            s2 = lerpStructs.scale.defaultScale + lerpStructs.scale.scaleFluctuation;   // �����X�P�[�� + �X�P�[���ω���

            // �^�� Image �Ȃ� Image �p�̐��`��Ԃ��s��
            if (lerpStructs.uiType == typeof(Image))
            {
                // "�F" �̐��`��Ԃ��g�p���邩�|�C���^�[�� "�������" �Ȃ�A���`��Ԃ��s��
                if ((lerpStructs.color.use) && (orEnter))
                { StartCoroutine(lerp.Color_Image((Image)lerpStructs.ui, c1, c2, lerpStructs.color.fadeTime)); }

                // "�F" �̐��`��Ԃ��g�p���邩�|�C���^�[�� "���ꂽ" ���Ȃ�A���`��Ԃ��s��
                else if ((lerpStructs.color.use) && (!orEnter))
                { StartCoroutine(lerp.Color_Image((Image)lerpStructs.ui, c2, c1, lerpStructs.color.fadeTime)); }

                // "���W" �̐��`��Ԃ��g�p���邩�|�C���^�[�� "�������" �Ȃ�A���`��Ԃ��s��
                if ((lerpStructs.position.use) && (orEnter))
                { StartCoroutine(lerp.Position_Image((Image)lerpStructs.ui, p1, p2, lerpStructs.position.fadeTime)); }

                // "���W" �̐��`��Ԃ��g�p���邩�|�C���^�[�� "���ꂽ��" �Ȃ�A���`��Ԃ��s��
                else if ((lerpStructs.position.use) && (!orEnter))
                { StartCoroutine(lerp.Position_Image((Image)lerpStructs.ui, p2, p1, lerpStructs.position.fadeTime)); }

                // "�X�P�[��" �̐��`��Ԃ��g�p���邩�|�C���^�[�� "�������" �Ȃ�A���`��Ԃ��s��
                if ((lerpStructs.scale.use) && (orEnter))
                { StartCoroutine(lerp.Scale_Image((Image)lerpStructs.ui, s1, s2, lerpStructs.scale.fadeTime)); }

                // "�X�P�[��" �̐��`��Ԃ��g�p���邩�|�C���^�[�� "���ꂽ��" �Ȃ�A���`��Ԃ��s��
                else if ((lerpStructs.scale.use) && (!orEnter))
                { StartCoroutine(lerp.Scale_Image((Image)lerpStructs.ui, s2, s1, lerpStructs.scale.fadeTime)); }
            }

            // �^�� Text �Ȃ� Text �p�̐��`��Ԃ��s��
            if (lerpStructs.uiType == typeof(Text))
            {
                // "�F" �̐��`��Ԃ��g�p���邩�|�C���^�[�� "�������" �Ȃ�A���`��Ԃ��s��
                if ((lerpStructs.color.use) && (orEnter))
                { StartCoroutine(lerp.Color_Text((Text)lerpStructs.ui, c1, c2, lerpStructs.color.fadeTime)); }

                // "�F" �̐��`��Ԃ��g�p���邩�|�C���^�[�� "���ꂽ" ���Ȃ�A���`��Ԃ��s��
                else if ((lerpStructs.color.use) && (!orEnter))
                { StartCoroutine(lerp.Color_Text((Text)lerpStructs.ui, c2, c1, lerpStructs.color.fadeTime)); }

                // "���W" �̐��`��Ԃ��g�p���邩�|�C���^�[�� "�������" �Ȃ�A���`��Ԃ��s��
                if ((lerpStructs.position.use) && (orEnter))
                { StartCoroutine(lerp.Position_Text((Text)lerpStructs.ui, p1, p2, lerpStructs.position.fadeTime)); }

                // "���W" �̐��`��Ԃ��g�p���邩�|�C���^�[�� "���ꂽ��" �Ȃ�A���`��Ԃ��s��
                else if ((lerpStructs.position.use) && (!orEnter))
                { StartCoroutine(lerp.Position_Text((Text)lerpStructs.ui, p2, p1, lerpStructs.position.fadeTime)); }

                // "�X�P�[��" �̐��`��Ԃ��g�p���邩�|�C���^�[�� "�������" �Ȃ�A���`��Ԃ��s��
                if ((lerpStructs.scale.use) && (orEnter))
                { StartCoroutine(lerp.Scale_Text((Text)lerpStructs.ui, s1, s2, lerpStructs.scale.fadeTime)); }

                // "�X�P�[��" �̐��`��Ԃ��g�p���邩�|�C���^�[�� "���ꂽ��" �Ȃ�A���`��Ԃ��s��
                else if ((lerpStructs.scale.use) && (!orEnter))
                { StartCoroutine(lerp.Scale_Text((Text)lerpStructs.ui, s2, s1, lerpStructs.scale.fadeTime)); }
            }
        }

        // UI�v�f�̃A�j���[�V����
        for (int i = 0; i < lerpStructs.Length; i++)
        {
            Anim(lerpStructs[i]);
        }
    }

    // UI�v�f�̏���������
    public void Init()
    {
        // UI�̗v�f��������
        // ���`�⊮�A�j���[�V�������g�p����UI�v�f�̐��J��Ԃ�
        for (int i = 0; i < lerpStructs.Length; i++)
        {
            // UI�v�f�̌^�� Image �Ȃ�
            if (lerpStructs[i].uiType == typeof(Image))
            {
                // �l��ύX���邽�߂Ɍ^�ϊ����đ�����Ă���
                Image image = (Image)lerpStructs[i].ui;

                if (lerpStructs[i].color.use) image.color = lerpStructs[i].color.defaultColor;                                // �F��������
                if (lerpStructs[i].position.use) image.rectTransform.position = lerpStructs[i].position.defaultPos;           // ���W��������
                if (lerpStructs[i].scale.use) image.rectTransform.localScale = lerpStructs[i].scale.defaultScale;             // �X�P�[����������

                if ((lerpStructs[i].onPointerDraw) && (lerpStructs[i].ui.enabled)) lerpStructs[i].ui.enabled = false;       // �|�C���^�[������Ă���Ƃ��̂ݕ`�悷��Ȃ��\��
            }

            // UI�v�f�̌^�� Text �Ȃ�
            if (lerpStructs[i].uiType == typeof(Text))
            {
                // �l��ύX���邽�߂Ɍ^�ϊ����đ�����Ă���
                Text text = (Text)lerpStructs[i].ui;

                if (lerpStructs[i].color.use) text.color = lerpStructs[i].color.defaultColor;                                 // �F��������
                if (lerpStructs[i].position.use) text.rectTransform.position = lerpStructs[i].position.defaultPos;            // ���W��������
                if (lerpStructs[i].scale.use) text.rectTransform.localScale = lerpStructs[i].scale.defaultScale;              // �X�P�[����������

                if ((lerpStructs[i].onPointerDraw) && (lerpStructs[i].ui.enabled)) lerpStructs[i].ui.enabled = false;       // �|�C���^�[������Ă���Ƃ��̂ݕ`�悷��Ȃ��\��
            }
        }
    }
}
