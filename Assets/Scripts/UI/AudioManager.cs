using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _Instance;
    public static AudioManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                var obj = new GameObject().AddComponent<AudioManager>();
                obj.name = "AudioManager Object";
                _Instance = obj.GetComponent<AudioManager>();
            }
            return _Instance;
        }
    }
    private void Awake()
    {
        Debug.Log("AUDIO" + _Instance);
        if (_Instance != null)
        {
            Debug.LogWarning("Second instance of AudioManager created. Automatic self - destruct triggered.");
            Destroy(gameObject);
        }

    }
    private void OnDestroy()
    {
        if (_Instance == this)
        {
            _Instance = null;
        }
    }
    void OnEnable() => DontDestroyOnLoad(gameObject);


}
