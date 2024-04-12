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
        bgmVolume = new ClampedValue<float>(0, 0, -80, nameof(bgmVolume));
        seVolume = new ClampedValue<float>(0, 0, -80, nameof(seVolume));

        // �l���ω������Ƃ��� Action ��ݒ�
        bgmVolume.onValueChange = () => SetVolume("BGM", bgmVolume.GetValue_Float());
        seVolume.onValueChange = () => SetVolume("SE", seVolume.GetValue_Float());
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
        audioMixer.SetFloat(mixerType, volume);
    }
}
