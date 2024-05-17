using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------Audio Source------------")]
    [SerializeField] AudioSource MainTheme, SFXSound;

    [Header("------------Audio Clip------------")]
    public AudioClip Background, RecipeCompleted, Walk, PickUp1, PickUp2, PickUp3, UiButton, Dig, Glide, Spawn;

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


}
