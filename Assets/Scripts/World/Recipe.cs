using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    private static Recipe _Instance;
    public static Recipe Instance
    {
        get
        {
            if (_Instance == null)
            {
                var obj = new GameObject().AddComponent<Recipe>();
                obj.name = "Recipe Object";
                _Instance = obj.GetComponent<Recipe>();
            }
            return _Instance;
        }
    }
    private void Awake()
    {
        if (_Instance != null)
        {
            Debug.LogWarning("Second instance of Recipe created.Automatic self - destruct triggered.");
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        if (_Instance == this)
        {
            _Instance = null;
        }
    }
    void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    #region Recipe
    Item[] fly = new Item[4];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion
}
