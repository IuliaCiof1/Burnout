using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    public AudioSource soundFXObject;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void PlaySoundFXClip(AudioClip audioclip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioclip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLenth = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLenth);
    }
    public void PlaySoundFXClips(AudioClip[] audioclip, Transform spawnTransform, float volume)
    {
        int rand = UnityEngine.Random.Range(0, audioclip.Length);
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioclip[rand];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLenth = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLenth);
    }
}
