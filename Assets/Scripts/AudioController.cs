using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip Sound; // Gestion du son à jouer

    [Range(0f, 1f)]
    [SerializeField] private float Volume; // Gestion du volume

    [Range(0.1f, 2.5f)]
    [SerializeField] private float Pitch; // Gestion de la vitesse du morceau

    private AudioSource Source; // Component qui reçoit les paramètres listés ci-dessus

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
       gameObject.AddComponent<AudioSource>();
       Source = GetComponent<AudioSource>();

       Volume = 0.5f;
       Pitch = 1f; 
    }

    // Start is called before the first frame update
    void Start()
    {
        Source.clip = Sound;
        Source.volume = Volume;
        Source.pitch = Pitch;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PlayAndPause();
        }

        Source.volume = Volume;
        Source.pitch = Pitch;
    }

    public void PlayAndPause()
    {
        if (!Source.isPlaying)
        {
            Source.Play();
        }
        else
        {
            Source.Pause();
        }
    }
}
