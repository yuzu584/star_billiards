using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 音を管理
public class Sound : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource; // AudioSource

    // 音声ファイルを再生
    public void Play(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}
