using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> SFX;
    [SerializeField] AudioSource soundSource;
    // Template for sound effects
    public void PlaySound(int soundIndex)
    {
        soundSource.clip = SFX[soundIndex];
    }
}
