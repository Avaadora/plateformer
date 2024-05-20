using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject DialogueBox;
    [SerializeField] private TMP_Text LabelText;
    [SerializeField] private DialogueObject[] dialogueObject;

    private TypeWritterEffect typeWritterEffect;
    private bool hasShownDialogue;

    private void Start()
    {
        typeWritterEffect = GetComponent<TypeWritterEffect>();
        CloseDialogueBox();

        RecipeManager.Instance.OnCanGlideChanged.AddListener(OnCanGlideChangedHandler);
        RecipeManager.Instance.OnCanDigChanged.AddListener(OnCanDigChangedHandler);
        RecipeManager.Instance.OnCanFireChanged.AddListener(OnCanFireChangedHandler);
        RecipeManager.Instance.OnCanWallJumpChanged.AddListener(OnCanWallJumpChangedHandler);

        // GameManager.Instance.OnSceneTuto.AddListener(OnCanSceneChangedHandler);
        // GameManager.Instance.OnSceneLevel.AddListener(OnCanSceneChangedHandler);
        // StartCoroutine(WaitForGameManagerInitialization());
    }
    // private IEnumerator WaitForGameManagerInitialization()
    // {
    //     while (GameManager.Instance == null || GameManager.Instance.OnSceneTuto == null || GameManager.Instance.OnSceneLevel == null)
    //     {
    //         yield return null;
    //     }

    //     GameManager.Instance.OnSceneTuto.AddListener(OnCanSceneChangedHandler);
    //     GameManager.Instance.OnSceneLevel.AddListener(OnCanSceneChangedHandler);
    // }

    public void OnEventTriggered()
    {
        OnCanGlideChangedHandler();
        OnCanDigChangedHandler();
        OnCanFireChangedHandler();
        OnCanWallJumpChangedHandler();

        // OnCanSceneChangedHandler();
    }

    public void ShowDialogue(DialogueObject ShowdialogueObject)
    {
        DialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(ShowdialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject StepDialogueObject)
    {
        foreach (string dialogue in StepDialogueObject.getDialogue())
        {
            yield return typeWritterEffect.Run(dialogue, LabelText);
            yield return new WaitForSeconds(1.5f);
        }
        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {
        DialogueBox.SetActive(false);
        LabelText.text = string.Empty;
    }

    private void OnCanGlideChangedHandler()
    {
        if (RecipeManager.Instance.getCanGlide() && !hasShownDialogue)
        {
            ShowDialogue(dialogueObject[0]);
            hasShownDialogue = true;
        }
    }
    private void OnCanDigChangedHandler()
    {
        if (RecipeManager.Instance.getCanDig() && !hasShownDialogue)
        {
            ShowDialogue(dialogueObject[1]);
            hasShownDialogue = true;
        }
    }

    private void OnCanFireChangedHandler()
    {
        if (RecipeManager.Instance.getCanFire() && !hasShownDialogue)
        {
            ShowDialogue(dialogueObject[2]);
            hasShownDialogue = true;
        }
    }
    private void OnCanWallJumpChangedHandler()
    {
        if (RecipeManager.Instance.getCanWallJump() && !hasShownDialogue)
        {
            ShowDialogue(dialogueObject[4]);
            hasShownDialogue = true;
        }
    }

    // private void OnCanSceneChangedHandler()
    // {
    //     if (GameManager.Instance.getIsTutoScene() && !hasShownDialogue)
    //     {
    //         ShowDialogue(dialogueObject[3]);
    //         hasShownDialogue = true;
    //     }
    //     else
    //     {
    //         if (GameManager.Instance.getIsLevelScene() && !hasShownDialogue)
    //         {
    //             ShowDialogue(dialogueObject[5]);
    //             hasShownDialogue = true;
    //         }
    //     }
    // }
}
