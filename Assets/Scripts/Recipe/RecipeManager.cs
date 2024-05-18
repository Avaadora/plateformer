using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class RecipeManager : MonoBehaviour
{
    private AudioManager audioManager;

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
            // DontDestroyOnLoad(gameObject);
        }
    }
    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    [Header("------------Recipe------------")]
    [SerializeField] private Item[] GlideRecipe, DigRecipe, FireRecipe, WallJumpRecipe; // Recette à valider
    [SerializeField]
    private Item Cookie, ChocolateBar, Watermelon,
                                    Chicken, Pie, Sushi,
                                    Cherry, Pepper, DragonFruit,
                                    Apple, Radish, Pinapple;

    [SerializeField] private Image[] GlideImage, DigImage, FireImage, WallJumpImage;
    [SerializeField]
    private Image CookieSprite, ChocolateBarSprite, WatermelonSprite,
                                    ChickenSprite, PieSprite, SushiSprite,
                                    CherrySprite, PepperSprite, DragonFruitSprite,
                                    AppleSprite, RadishSprite, PinappleSprite,
                                    Check, Wing, Shovel, Fire, Arrow;

    private bool canGlide, isGliding,
                    canDig, isDigging,
                    canFire, isFiring,
                    canWallJump, isWallJumping,
                    isInOrder;

    static int GlideIndex, DigIndex, FireIndex, WallJumpIndex = 0;

    public UnityEvent OnCanGlideChanged, OnCanDigChanged, OnCanFireChanged, OnCanWallJumpChanged;

    [Header("------------Scene Management------------")]
    [SerializeField] private GameObject tutorialObject;
    [SerializeField] private GameObject levelObject;



    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        tutorialObject.SetActive(false);
        levelObject.SetActive(false);

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
        // ----- Quatrième recette -----
        WallJumpRecipe = new Item[3];
        WallJumpRecipe[0] = Apple;
        WallJumpRecipe[1] = Radish;
        WallJumpRecipe[2] = Pinapple;

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
        // ----- Quatrième recette -----
        WallJumpImage = new Image[3];
        WallJumpImage[0] = AppleSprite;
        WallJumpImage[1] = RadishSprite;
        WallJumpImage[2] = PinappleSprite;

    }

    private void FixedUpdate()
    {
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 1:
                tutorialObject.SetActive(true);
                levelObject.SetActive(false);
                break;
            case 2:
                tutorialObject.SetActive(false);
                levelObject.SetActive(true);
                break;
            default:
                // Par défaut, désactive tous les enfants
                tutorialObject.SetActive(false);
                levelObject.SetActive(false);
                break;
        }
    }

    public void RestartGameRecipe()
    {
        if (GlideIndex >= 2)
        {
            GlideIndex = 0;
            canGlide = false;
        }

        if (DigIndex >= 2)
        {
            DigIndex = 0;
            canDig = false;
        }

        if (FireIndex >= 2)
        {
            FireIndex = 0;
            canFire = false;
        }

        if (WallJumpIndex >= 2)
        {
            WallJumpIndex = 0;
            canWallJump = false;
        }
    }


    public void CheckForGlideRecipe(Item ItemToPickUp)
    {
        if (GlideIndex < GlideRecipe.Length && ItemToPickUp == GlideRecipe[GlideIndex])
        {
            GlideIndex++;
            isInOrder = true;
            if (GlideIndex == GlideRecipe.Length)
            {
                Instantiate(Check, Wing.transform);
                canGlide = true;
                OnCanGlideChanged.Invoke();

                StartCoroutine(RecipeCompleted(0.5f));
            }
        }
        else
        {
            canGlide = false;
            isInOrder = false;
        }
    }

    public void CheckForDigRecipe(Item ItemToPickUp)
    {
        if (DigIndex < DigRecipe.Length && ItemToPickUp == DigRecipe[DigIndex])
        {
            isInOrder = true;
            DigIndex++;
            if (DigIndex == DigRecipe.Length)
            {
                Instantiate(Check, Shovel.transform);
                canDig = true;
                OnCanDigChanged.Invoke();

                StartCoroutine(RecipeCompleted(0.5f));
            }
        }
        else
        {
            isInOrder = false;
        }
    }

    public void CheckForFireRecipe(Item ItemToPickUp)
    {
        if (FireIndex < FireRecipe.Length && ItemToPickUp == FireRecipe[FireIndex] && canDig)
        {
            isInOrder = true;
            FireIndex++;
            if (FireIndex == FireRecipe.Length)
            {
                Instantiate(Check, Fire.transform);
                canFire = true;
                OnCanFireChanged.Invoke();

                StartCoroutine(RecipeCompleted(0.5f));
            }
        }
        else
        {
            canFire = false;
            isInOrder = false;
        }
    }

    public void CheckForWallJumpRecipe(Item ItemToPickUp)
    {
        if (WallJumpIndex < WallJumpRecipe.Length && ItemToPickUp == WallJumpRecipe[WallJumpIndex])
        {
            WallJumpIndex++;
            isInOrder = true;
            if (WallJumpIndex == WallJumpRecipe.Length)
            {
                Instantiate(Check, Arrow.transform);
                canWallJump = true;
                OnCanWallJumpChanged.Invoke();

                StartCoroutine(RecipeCompleted(0.5f));
            }
        }
        else
        {
            canWallJump = false;
            isInOrder = false;
        }
    }

    // Lier l'UI aux Item
    public void UpdateRecipeUI(Sprite Cookie, Sprite ChocolateBar, Sprite Watermelon,
                                Sprite Chicken, Sprite Pie, Sprite Sushi,
                                Sprite Cherry, Sprite Pepper, Sprite DragonFruit,
                                Sprite Apple, Sprite Radish, Sprite Pinapple)
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

        AppleSprite.sprite = Apple;
        RadishSprite.sprite = Radish;
        PinappleSprite.sprite = Pinapple;
    }

    public void ClearCheckmarks()
    {
        GameObject[] checkmarks = GameObject.FindGameObjectsWithTag("Check");
        foreach (GameObject checkmark in checkmarks)
        {
            Destroy(checkmark);
        }
    }

    public void ClearUI()
    {
        foreach (Image item in WallJumpImage)
        {
            Debug.Log(item.name);
            item.color = new Color(0,0,0);
        }
    }

    IEnumerator RecipeCompleted(float time)
    {
        yield return new WaitForSeconds(time);
        AudioManager._Instance.PlaySFX(audioManager.RecipeCompleted);
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

    public bool getIsWallJumping()
    {
        return isWallJumping;
    }

    public void setIsWallJumping(bool isWallJumping)
    {
        this.isWallJumping = isWallJumping;
    }

    public bool getCanWallJump()
    {
        return canWallJump;
    }

    public void setCanWallJump(bool canWallJump)
    {
        this.canWallJump = canWallJump;
    }

    public bool getIsInOrder()
    {
        return this.isInOrder;
    }

    public void setIsInOrder(bool isInOrder)
    {
        this.isInOrder = isInOrder;
    }

}
