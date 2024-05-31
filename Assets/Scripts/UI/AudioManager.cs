using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------Audio Source------------")]
    [SerializeField] public AudioSource MainTheme, SFXSound, WalkSound;

    [Header("------------Audio Clip------------")]
    public AudioClip Background, RecipeCompleted, Walk, PickUp1, PickUp2, PickUp3, UiButton;

    public static AudioManager _Instance;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        MainTheme.clip = Background;
        MainTheme.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSound.PlayOneShot(clip);
    }

    public void PlayWalkSound(float pitch)
    {
        WalkSound.pitch = pitch;
        if (!WalkSound.isPlaying)
        {
            WalkSound.clip = Walk;
            WalkSound.loop = true; // Boucle le son de marche
            WalkSound.Play();
        }
    }

    public void StopWalkSound()
    {
        WalkSound.Stop();
    }
}
