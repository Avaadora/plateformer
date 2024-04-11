using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    private static RecipeManager _Instance;
    public static RecipeManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                var obj = new GameObject().AddComponent<RecipeManager>();
                obj.name = "Recipe Object";
                _Instance = obj.GetComponent<RecipeManager>();
            }
            return _Instance;
        }
    }
    private void Awake()
    {
        if (_Instance != null)
        {
            Debug.LogWarning("Second instance of Recipe created. Automatic self - destruct triggered.");
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


    #region Recipe
    [SerializeField] private Item[] recipe; // La recette à valider
    [SerializeField] private Item Cookie, ChocolateBar, Watermelon;

    private int currentIndex = 0;
    private bool IsValidate;

    void Start()
    {
        // Setup de la première recette
        recipe = new Item[3];
        recipe[0] = Cookie;
        recipe[1] = ChocolateBar;
        recipe[2] = Watermelon;
    }

    public void CheckForCraftRecipe(Item ItemToPickUp)
    {
        if (currentIndex < recipe.Length && ItemToPickUp == recipe[currentIndex])
        {
            Debug.Log("Ingrédient collecté : " + ItemToPickUp.ItemName);
            currentIndex++;
            if (currentIndex == recipe.Length)
            {
                Debug.Log("Recette terminée avec succès !");
                IsValidate = true;
            }
        }
        else
        {
            Debug.Log("Ingrédient incorrect ou hors d'ordre.");
        }
    }
    #endregion

}
