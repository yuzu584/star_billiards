using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// 音を管理
public class Sound : Singleton<Sound>
{
    private const float SOUND_PLAY_INTERVAL = 0.05f;                    // 音再生間隔

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer audioMixer;

    public ClampedValue<float> bgmVolume;                               // BGM の音量
    public ClampedValue<float> seVolume;                                // SE の音量

    // AudioClip ごとの情報
    [System.Serializable]
    public class AudioClipData
    {
        public AudioClip audioClip;
        public float playedTime;                                        // 前回再生したときの時間

        // 再生間隔を経過したか( true:経過済み false:経過していない)
        public bool CheckInterval()
        {
            return (Time.realtimeSinceStartup - playedTime >= SOUND_PLAY_INTERVAL);
        }
    }

    public List<AudioClipData> clipData = new List<AudioClipData>();    // AudioClip ごとの情報の List

    private void Start()
    {
        // 各音量の値と範囲を設定
        bgmVolume = new ClampedValue<float>(0.8f, 1, 0, nameof(bgmVolume));
        seVolume = new ClampedValue<float>(0.8f, 1, 0, nameof(seVolume));

        // 値が変化したときの Action を設定
        bgmVolume.SetOnValueChanged(() => SetVolume("BGM", bgmVolume.GetValue_Float()));
        seVolume.SetOnValueChanged(() => SetVolume("SE", seVolume.GetValue_Float()));

        // 最初に音量を設定
        SetVolume("BGM", bgmVolume.GetValue_Float());
        SetVolume("SE", seVolume.GetValue_Float());
    }

    // 音声ファイルを再生
    public IEnumerator Play(AudioClip audio)
    {
        if (audio == null) yield break;

        // 再生間隔を経過していたら再生
        AudioClipData acd = GetClip(audio);
        if (acd.CheckInterval())
        {
            acd.playedTime = Time.realtimeSinceStartup;
            audioSource.PlayOneShot(acd.audioClip);
        }

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

    // List に引数の AudioClip が含まれてれば返す
    // 含まれていなければ List に追加して返す
    private AudioClipData GetClip(AudioClip ac)
    {
        for (int i = 0; i < clipData.Count; i++)
        {
            // AudioClip が見つかったら返す
            if (clipData[i].audioClip == ac)
            {
                return clipData[i];
            }
        }

        // AudioClip が見つからなかったら List に追加して返す
        AudioClipData ad = new AudioClipData { audioClip = ac };
        clipData.Add(ad);
        return ad;
    }
}
