using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SoundManager : MonoBehaviour
{
    AudioSource soundSource;
    [SerializeField] GameObject audioManager;
    [SerializeField] List<AudioClip> SFX;
    [SerializeField] GameObject mainCamera;

    // Template for sound effects
    public void PlaySound(int soundIndex)
    {
        // Create the sound source
        soundSource = Instantiate(audioManager, mainCamera.transform).GetComponent<AudioSource>();
        // Put the desired sound effect on it from a list of sounds
        soundSource.clip = SFX[soundIndex];
        // Play the sound effect
        soundSource.Play();
    }

}
