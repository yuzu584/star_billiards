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
        bgmVolume = new ClampedValue<float>(0.8f, 1, 0, nameof(bgmVolume));
        seVolume = new ClampedValue<float>(0.8f, 1, 0, nameof(seVolume));

        // 値が変化したときの Action を設定
        bgmVolume.onValueChange = () => SetVolume("BGM", bgmVolume.GetValue_Float());
        seVolume.onValueChange = () => SetVolume("SE", seVolume.GetValue_Float());

        // 最初に音量を設定
        SetVolume("BGM", bgmVolume.GetValue_Float());
        SetVolume("SE", seVolume.GetValue_Float());
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
        // volumeをデシベルに変換
        float DB = CalcGetVolume(volume);
        DB = ConvertVolume2dB(DB);

        // 音量を設定
        audioMixer.SetFloat(mixerType, DB);
    }

    // volume(0～-1)からデシベル(-80～0)に変換
    float ConvertVolume2dB(float volume) => Mathf.Clamp(20f * Mathf.Log10(Mathf.Clamp(volume, 0f, 1f)), -80f, 0f);

    // デシベル(-80～0)からvolume(0～-1)に変換
    float ConvertDB2Volume(float db) => Mathf.Clamp(Mathf.Pow(10, Mathf.Clamp(db, -80, 0) / 20f), 0, 1);

    /// <summary>
    /// ラウドネスを考慮したボリュームを計算して取得
    /// </summary>
    /// <param name="volLvRatio">ボリュームレベル比率 (0.0-1.0)</param>
    /// <returns>ラウドネスを考慮したボリューム (0.0-1.0)</returns>
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
