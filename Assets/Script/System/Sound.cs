using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ‰¹‚ğŠÇ—
public class Sound : Singleton<Sound>
{
    [SerializeField] protected AudioSource audioSource; // AudioSource

    // ‰¹ºƒtƒ@ƒCƒ‹‚ğÄ¶
    public IEnumerator Play(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);

        yield return null;
    }
}
