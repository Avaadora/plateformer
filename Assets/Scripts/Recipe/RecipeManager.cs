using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    private static RecipeManager _instance;
    public static RecipeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject().AddComponent<RecipeManager>();
                obj.name = "RecipeManager Object";
                _instance = obj.GetComponent<RecipeManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("Second instance of Recipe created. Automatic self - destruct triggered.");
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    [Header("------------Recipe------------")]
    [SerializeField] private Item[] recipe; // Recette à valider
    [SerializeField] private Image[] image;
    [SerializeField] private Item Cookie, ChocolateBar, Watermelon;
    [SerializeField] private Image CookieSprite, ChocolateBarSprite, WatermelonSprite, Check, Wing;

    private bool canGlide;

    private int currentIndex = 0;

    void Start()
    {
        // Setup des recettes
        recipe = new Item[3];
        recipe[0] = Cookie;
        recipe[1] = ChocolateBar;
        recipe[2] = Watermelon;

        image = new Image[3];
        image[0] = CookieSprite;
        image[1] = ChocolateBarSprite;
        image[2] = WatermelonSprite;
    }

    public void CheckForCraftRecipe(Item ItemToPickUp)
    {
        if (currentIndex < recipe.Length && ItemToPickUp == recipe[currentIndex])
        {
            Debug.Log("Ingrédient collecté : " + ItemToPickUp.ItemName);
            Instantiate(Check, image[currentIndex].transform);
            currentIndex++;
            if (currentIndex == recipe.Length)
            {
                Debug.Log("Recette terminée avec succès !");
                Instantiate(Check, Wing.transform);
                canGlide = true;
            }
        }
        else
        {
            Debug.Log("Ingrédient incorrect ou hors d'ordre.");
            canGlide = false;
        }
    }

    // Lier l'UI aux Item
    public void UpdateRecipeUI(Sprite Cookie, Sprite ChocolateBar, Sprite Watermelon)
    {
        CookieSprite.sprite = Cookie;
        ChocolateBarSprite.sprite = ChocolateBar;
        WatermelonSprite.sprite = Watermelon;
    }

    public bool getCanGlide()
    {
        return canGlide;
    }

    public void setCanGlide(bool canGlide)
    {
        this.canGlide = canGlide;
    }

}
