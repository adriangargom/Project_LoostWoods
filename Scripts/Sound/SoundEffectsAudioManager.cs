

using System.Collections;
using UnityEngine;

public class SoundEffectsAudioManager : MonoBehaviour
{
    public static SoundEffectsAudioManager Instance { get; private set; }

    [field: SerializeField] public AudioSource AudioSource { get; private set; }

    [field: Header("Player Sound Effects")]
    [field: SerializeField] public AudioClip PlayerAttack { get; private set; }

    [field: Header("Items Sound Effects")]
    [field: SerializeField] public AudioClip HealthItemGrab { get; private set; }
    [field: SerializeField] public AudioClip GoldItemGrab { get; private set; }

    [field: Header("General Sound Effects")]
    [field: SerializeField] public AudioClip Die { get; private set; }

    [field: Header("Blob Sound Effects")]
    [field: SerializeField] public AudioClip BlobAttack { get; private set;}

    [field: Header("Blob Sound Effects")]
    [field: SerializeField] public AudioClip PlantAttack { get; private set;}

    [field: Header("Blob Sound Effects")]
    [field: SerializeField] public AudioClip SpikeMeleeAttack { get; private set;}

    [field: Header("Menu Sound Effects")]
    [field: SerializeField] public AudioClip OpenMenu { get; private set;}
    [field: SerializeField] public AudioClip CloseMenu { get; private set;}


    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        AudioSource = GetComponent<AudioSource>();    
    }


    public void PlaySoundEffect(AudioClip soundEffect)
    {
        AudioSource.PlayOneShot(soundEffect);
    }

    public void LaunchDelayedSoundEffect(AudioClip soundEffect, float delay)
    {
        StartCoroutine(PlayDelayedSoundEffect(soundEffect, delay));
    }

    IEnumerator PlayDelayedSoundEffect(AudioClip soundEffect, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioSource.PlayOneShot(soundEffect);
    }
}