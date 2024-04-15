using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// �����Ǘ�
public class Sound : Singleton<Sound>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer audioMixer;

    public ClampedValue<float> bgmVolume;   // BGM �̉���
    public ClampedValue<float> seVolume;    // SE �̉���

    private void Start()
    {
        // �e���ʂ̒l�Ɣ͈͂�ݒ�
        bgmVolume = new ClampedValue<float>(0.8f, 1, 0, nameof(bgmVolume));
        seVolume = new ClampedValue<float>(0.8f, 1, 0, nameof(seVolume));

        // �l���ω������Ƃ��� Action ��ݒ�
        bgmVolume.onValueChange = () => SetVolume("BGM", bgmVolume.GetValue_Float());
        seVolume.onValueChange = () => SetVolume("SE", seVolume.GetValue_Float());

        // �ŏ��ɉ��ʂ�ݒ�
        SetVolume("BGM", bgmVolume.GetValue_Float());
        SetVolume("SE", seVolume.GetValue_Float());
    }

    // �����t�@�C�����Đ�
    public IEnumerator Play(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);

        yield return null;
    }

    // AudioMixer �̉��ʂ�ݒ�
    public void SetVolume(string mixerType, float volume)
    {
        // volume���f�V�x���ɕϊ�
        float DB = CalcGetVolume(volume);
        DB = ConvertVolume2dB(DB);

        // ���ʂ�ݒ�
        audioMixer.SetFloat(mixerType, DB);
    }

    // volume(0�`-1)����f�V�x��(-80�`0)�ɕϊ�
    float ConvertVolume2dB(float volume) => Mathf.Clamp(20f * Mathf.Log10(Mathf.Clamp(volume, 0f, 1f)), -80f, 0f);

    // �f�V�x��(-80�`0)����volume(0�`-1)�ɕϊ�
    float ConvertDB2Volume(float db) => Mathf.Clamp(Mathf.Pow(10, Mathf.Clamp(db, -80, 0) / 20f), 0, 1);

    /// <summary>
    /// ���E�h�l�X���l�������{�����[�����v�Z���Ď擾
    /// </summary>
    /// <param name="volLvRatio">�{�����[�����x���䗦 (0.0-1.0)</param>
    /// <returns>���E�h�l�X���l�������{�����[�� (0.0-1.0)</returns>
    float CalcGetVolume(float volLvRatio)
    {
        if (volLvRatio <= 0.0f)
        {
            return 0.0f;
        }
        else if (volLvRatio >= 1.0f)
        {
            return 1.0f;
        }
        else
        {
            return Mathf.Pow(10.0f, -Mathf.Log(1.0f / volLvRatio, 2.0f) / 2.0f);
        }
    }
}
