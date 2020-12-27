using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    #region Variables
    private float collissionSoundEffect = 1f;

    public float audioFootVolume = 1f;
    public float soundEffectPitchRandomness = 0.05f;

    private AudioSource audioSource;
    public AudioClip genericFoodSound;
    public AudioClip metalFootSound;

    #endregion

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void FootSound()
    {
        audioSource.volume = collissionSoundEffect * audioFootVolume;
        audioSource.pitch = Random.Range(1.0f - soundEffectPitchRandomness, 1.0f + soundEffectPitchRandomness);

        if(Random.Range(0,2) > 0)
        {
            audioSource.clip = genericFoodSound;
        }
        else
        {
            audioSource.clip = metalFootSound;
        }
    }




} // PlayerSounds class
