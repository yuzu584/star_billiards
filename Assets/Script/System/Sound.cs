using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����Ǘ�
public class Sound : Singleton<Sound>
{
    [SerializeField] protected AudioSource audioSource; // AudioSource

    // �����t�@�C�����Đ�
    public IEnumerator Play(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);

        yield return null;
    }
}
