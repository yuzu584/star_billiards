using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// 音を管理
public class Sound : Singleton<Sound>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer audioMixer;

    public ClampedValue<float> bgmVolume;   // BGM の音量
    public ClampedValue<float> seVolume;    // SE の音量

    private void Start()
    {
        // 各音量の値と範囲を設定
        bgmVolume = new ClampedValue<float>(0, 0, -80, nameof(bgmVolume));
        seVolume = new ClampedValue<float>(0, 0, -80, nameof(seVolume));

        // 値が変化したときの Action を設定
        bgmVolume.onValueChange = () => SetVolume("BGM", bgmVolume.GetValue_Float());
        seVolume.onValueChange = () => SetVolume("SE", seVolume.GetValue_Float());
    }

    // 音声ファイルを再生
    public IEnumerator Play(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);

        yield return null;
    }

    // AudioMixer の音量を設定
    public void SetVolume(string mixerType, float volume)
    {
        audioMixer.SetFloat(mixerType, volume);
    }
}
