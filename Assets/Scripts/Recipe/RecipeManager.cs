using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RecipeManager : MonoBehaviour
{
    [Header("------------Singleton------------")]
    private static RecipeManager _instance;
    public static RecipeManager Instance
    {
        get
        {
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
    [SerializeField] private Item[] GlideRecipe, DigRecipe; // Recette à valider
    [SerializeField] private Image[] GlideImage, DigImage;
    [SerializeField]
    private Item Cookie, ChocolateBar, Watermelon,
                                    Chicken, Pie, Sushi;
    [SerializeField]
    private Image CookieSprite, ChocolateBarSprite, WatermelonSprite,
                                    ChickenSprite, PieSprite, SushiSprite,
                                    Check, Wing;

    private bool canGlide, isGliding, canDig, isDigging;

    private int GlideIndex, DigIndex = 0;

    public UnityEvent OnCanGlideChanged;

    void Start()
    {
        // Setup des recettes
        // ----- Première recette -----
        GlideRecipe = new Item[3];
        GlideRecipe[0] = Cookie;
        GlideRecipe[1] = ChocolateBar;
        GlideRecipe[2] = Watermelon;
        // ----- Deuxième recette -----
        DigRecipe = new Item[3];
        DigRecipe[0] = Chicken;
        DigRecipe[1] = Pie;
        DigRecipe[2] = Sushi;


        GlideImage = new Image[3];
        GlideImage[0] = CookieSprite;
        GlideImage[1] = ChocolateBarSprite;
        GlideImage[2] = WatermelonSprite;

        DigImage = new Image[3];
        DigImage[0] = ChickenSprite;
        DigImage[1] = PieSprite;
        DigImage[2] = SushiSprite;
    }

    public void CheckForGlideRecipe(Item ItemToPickUp)
    {
        if (GlideIndex < GlideRecipe.Length && ItemToPickUp == GlideRecipe[GlideIndex])
        {
            Debug.Log("Ingrédient collecté : " + ItemToPickUp.ItemName);
            Instantiate(Check, GlideImage[GlideIndex].transform);
            GlideIndex++;
            if (GlideIndex == GlideRecipe.Length)
            {
                Instantiate(Check, Wing.transform);
                canGlide = true;
                Debug.Log("RECETTE DIG");
                OnCanGlideChanged.Invoke();
            }
        }
        else
        {
            Debug.Log("Ingrédient incorrect ou hors d'ordre.");
            canGlide = false;
        }
    }

    public void CheckForDigRecipe(Item ItemToPickUp)
    {
        if (DigIndex < DigRecipe.Length && ItemToPickUp == DigRecipe[DigIndex])
        {
            Debug.Log("Ingrédient collecté : " + ItemToPickUp.ItemName);
            Instantiate(Check, DigImage[DigIndex].transform);
            DigIndex++;
            if (DigIndex == DigRecipe.Length)
            {
                Debug.Log("RECETTE DIG");
                // Instantiate(Check, Wing.transform);
                canDig = true;

                // OnCanGlideChanged.Invoke();
            }
        }
        else
        {
            Debug.Log("Ingrédient incorrect ou hors d'ordre.");
            canDig = false;
        }
    }

    // Lier l'UI aux Item
    public void UpdateRecipeUI(Sprite Cookie, Sprite ChocolateBar, Sprite Watermelon,
                                Sprite Chicken, Sprite Pie, Sprite Sushi)
    {
        CookieSprite.sprite = Cookie;
        ChocolateBarSprite.sprite = ChocolateBar;
        WatermelonSprite.sprite = Watermelon;

        ChickenSprite.sprite = Chicken;
        PieSprite.sprite = Pie;
        SushiSprite.sprite = Sushi;
    }

    public bool getCanGlide()
    {
        return canGlide;
    }

    public void setCanGlide(bool canGlide)
    {
        this.canGlide = canGlide;
    }

    // Pour le player
    public bool getIsGliding()
    {
        return isGliding;
    }

    public void setIsGliding(bool isGliding)
    {
        this.isGliding = isGliding;
    }
    public bool isCanDig()
    {
        return canDig;
    }

    public void setCanDig(bool canDig)
    {
        this.canDig = canDig;
    }

    public bool getIsDigging()
    {
        return isDigging;
    }

    public void setIsDigging(bool isDigging)
    {
        this.isDigging = isDigging;
    }
}
