using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// �����Ǘ�
public class Sound : Singleton<Sound>
{
    private const float SOUND_PLAY_INTERVAL = 0.05f;                    // ���Đ��Ԋu

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer audioMixer;

    public ClampedValue<float> bgmVolume;                               // BGM �̉���
    public ClampedValue<float> seVolume;                                // SE �̉���

    // AudioClip ���Ƃ̏��
    [System.Serializable]
    public class AudioClipData
    {
        public AudioClip audioClip;
        public float playedTime;                                        // �O��Đ������Ƃ��̎���

        // �Đ��Ԋu���o�߂�����( true:�o�ߍς� false:�o�߂��Ă��Ȃ�)
        public bool CheckInterval()
        {
            return (Time.realtimeSinceStartup - playedTime >= SOUND_PLAY_INTERVAL);
        }
    }

    public List<AudioClipData> clipData = new List<AudioClipData>();    // AudioClip ���Ƃ̏��� List

    private void Start()
    {
        // �e���ʂ̒l�Ɣ͈͂�ݒ�
        bgmVolume = new ClampedValue<float>(0.8f, 1, 0, nameof(bgmVolume));
        seVolume = new ClampedValue<float>(0.8f, 1, 0, nameof(seVolume));

        // �l���ω������Ƃ��� Action ��ݒ�
        bgmVolume.SetOnValueChanged(() => SetVolume("BGM", bgmVolume.GetValue_Float()));
        seVolume.SetOnValueChanged(() => SetVolume("SE", seVolume.GetValue_Float()));

        // �ŏ��ɉ��ʂ�ݒ�
        SetVolume("BGM", bgmVolume.GetValue_Float());
        SetVolume("SE", seVolume.GetValue_Float());
    }

    // �����t�@�C�����Đ�
    public IEnumerator Play(AudioClip audio)
    {
        if (audio == null) yield break;

        // �Đ��Ԋu���o�߂��Ă�����Đ�
        AudioClipData acd = GetClip(audio);
        if (acd.CheckInterval())
        {
            acd.playedTime = Time.realtimeSinceStartup;
            audioSource.PlayOneShot(acd.audioClip);
        }

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

    // List �Ɉ����� AudioClip ���܂܂�Ă�ΕԂ�
    // �܂܂�Ă��Ȃ���� List �ɒǉ����ĕԂ�
    private AudioClipData GetClip(AudioClip ac)
    {
        for (int i = 0; i < clipData.Count; i++)
        {
            // AudioClip ������������Ԃ�
            if (clipData[i].audioClip == ac)
            {
                return clipData[i];
            }
        }

        // AudioClip ��������Ȃ������� List �ɒǉ����ĕԂ�
        AudioClipData ad = new AudioClipData { audioClip = ac };
        clipData.Add(ad);
        return ad;
    }
}
