using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------Audio Source------------")]
    [SerializeField] AudioSource MainTheme;
    [SerializeField] AudioSource SFXSound;

    [Header("------------Audio Clip------------")]
    public AudioClip Background;
    public AudioClip RecipeCompleted;
    public AudioClip Walk;


    private void Start()
    {
        MainTheme.clip = Background;
        MainTheme.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSound.PlayOneShot(clip);
    }


}
