using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    public AudioSource walkingAudioSource;

    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip shootKunaiSound;
    public AudioClip collectCrystalSound;
    public AudioClip walkingSound;
    public AudioClip reachGroundSound;
    public AudioClip dashSound;
    public AudioClip teleportSound;
    public AudioClip kunaiImpactSound;
    public AudioClip barrierSound;

    //Singleton
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicAudioSource.volume = PlayerPrefs.GetInt("Volume", 1)/3;    
        sfxAudioSource.volume = PlayerPrefs.GetInt("Volume", 1)/3; 
        walkingAudioSource.volume = PlayerPrefs.GetInt("Volume", 1);
    }

    public void MuteSounds()
    {
        musicAudioSource.volume = 0;
        sfxAudioSource.volume = 0;
        walkingAudioSource.volume = 0;
    }

    public void UnmuteSounds()
    {
        musicAudioSource.volume = PlayerPrefs.GetInt("Volume", 1)/3;
        sfxAudioSource.volume = PlayerPrefs.GetInt("Volume", 1)/3;
        walkingAudioSource.volume = PlayerPrefs.GetInt("Volume", 1);
    }

    public void PlayJumpSound()
    {
        sfxAudioSource.PlayOneShot(jumpSound);
    }

    public void PlayShootKunaiSound()
    {
        sfxAudioSource.PlayOneShot(shootKunaiSound);
    }

    public void PlayCollectCrystalSound()
    {
        sfxAudioSource.PlayOneShot(collectCrystalSound);
    }

    public void PlayWalkingSound()
    {
        walkingAudioSource.mute = false;
    }

    public void StopWalkingSound()
    {
        walkingAudioSource.mute = true;
    }

    public void PlayReachGroundSound()
    {
        sfxAudioSource.PlayOneShot(reachGroundSound);
    }

    public void PlayDashSound()
    {
        sfxAudioSource.PlayOneShot(dashSound);
    }

    public void PlayTeleportSound()
    {
        sfxAudioSource.PlayOneShot(teleportSound);
    }

    public void PlayKunaiImpactSound()
    {
        sfxAudioSource.PlayOneShot(kunaiImpactSound);
    }

    public void PlayBarrierSound()
    {
        //sfxAudioSource.PlayOneShot(barrierSound);
    }

}
