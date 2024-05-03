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
    [SerializeField] private Item[] GlideRecipe, DigRecipe, FireRecipe; // Recette à valider
    [SerializeField] private Item Cookie, ChocolateBar, Watermelon, Chicken, Pie, Sushi, Cherry, Pepper, DragonFruit;

    [SerializeField] private Image[] GlideImage, DigImage, FireImage;
    [SerializeField] private Image CookieSprite, ChocolateBarSprite, WatermelonSprite, ChickenSprite, PieSprite, SushiSprite, CherrySprite, PepperSprite, DragonFruitSprite, Check, Wing, Shovel, Fire;

    private bool canGlide, isGliding, canDig, isDigging, canFire, isFiring;


    private int GlideIndex, DigIndex, FireIndex = 0;

    public UnityEvent OnCanGlideChanged, OnCanDigChanged, OnCanFireChanged;

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
        // ----- Troisième recette -----
        FireRecipe = new Item[3];
        FireRecipe[0] = Cherry;
        FireRecipe[1] = Pepper;
        FireRecipe[2] = DragonFruit;
        // Setup des Sprites
        // ----- Première recette -----
        GlideImage = new Image[3];
        GlideImage[0] = CookieSprite;
        GlideImage[1] = ChocolateBarSprite;
        GlideImage[2] = WatermelonSprite;
        // ----- Deuxième recette -----
        DigImage = new Image[3];
        DigImage[0] = ChickenSprite;
        DigImage[1] = PieSprite;
        DigImage[2] = SushiSprite;
        // ----- Troisème recette -----
        FireImage = new Image[3];
        FireImage[0] = CherrySprite;
        FireImage[1] = PepperSprite;
        FireImage[2] = DragonFruitSprite;
    }

    public void CheckForGlideRecipe(Item ItemToPickUp)
    {
        if (GlideIndex < GlideRecipe.Length && ItemToPickUp == GlideRecipe[GlideIndex])
        {
            Instantiate(Check, GlideImage[GlideIndex].transform);
            GlideIndex++;
            if (GlideIndex == GlideRecipe.Length)
            {
                Instantiate(Check, Wing.transform);
                canGlide = true;
                OnCanGlideChanged.Invoke();
            }
        }
        else
        {
            canGlide = false;
        }
    }

    public void CheckForDigRecipe(Item ItemToPickUp)
    {
        if (DigIndex < DigRecipe.Length && ItemToPickUp == DigRecipe[DigIndex])
        {
            Instantiate(Check, DigImage[DigIndex].transform);
            DigIndex++;
            if (DigIndex == DigRecipe.Length)
            {
                Instantiate(Check, Shovel.transform);
                canDig = true;
                OnCanDigChanged.Invoke();
            }
        }
        else
        {
            canDig = false;
        }
    }

        public void CheckForFireRecipe(Item ItemToPickUp)
    {
        if (FireIndex < FireRecipe.Length && ItemToPickUp == FireRecipe[FireIndex])
        {
            Instantiate(Check, FireImage[FireIndex].transform);
            FireIndex++;
            if (DigIndex == FireRecipe.Length)
            {
                Instantiate(Check, Fire.transform);
                canFire = true;
                OnCanFireChanged.Invoke();
            }
        }
        else
        {
            canFire = false;
        }
    }

    // Lier l'UI aux Item
    public void UpdateRecipeUI(Sprite Cookie, Sprite ChocolateBar, Sprite Watermelon, Sprite Chicken, Sprite Pie, Sprite Sushi, Sprite Cherry, Sprite Pepper, Sprite DragonFruit)
    {
        CookieSprite.sprite = Cookie;
        ChocolateBarSprite.sprite = ChocolateBar;
        WatermelonSprite.sprite = Watermelon;

        ChickenSprite.sprite = Chicken;
        PieSprite.sprite = Pie;
        SushiSprite.sprite = Sushi;

        CherrySprite.sprite = Cherry;
        PepperSprite.sprite = Pepper;
        DragonFruitSprite.sprite = DragonFruit;
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
    public bool getCanDig()
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

    public bool getCanFire()
    {
        return canFire;
    }

    public void setCanFire(bool canFire)
    {
        this.canFire = canFire;
    }

    public bool getIsFiring()
    {
        return isFiring;
    }

    public void setIsFiring(bool isFiring)
    {
        this.isFiring = isFiring;
    }

}
