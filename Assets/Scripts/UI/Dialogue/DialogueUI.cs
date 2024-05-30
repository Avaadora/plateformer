using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

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

        SceneController.Instance.OnSceneIntro.AddListener(OnCanSceneIntroChangedHandler);
        SceneController.Instance.OnSceneEnd.AddListener(OnCanSceneEndChangedHandler);

        GameManager.Instance.OnSceneTuto.AddListener(OnCanSceneChangedHandler);
        GameManager.Instance.OnSceneLevel.AddListener(OnCanSceneChangedHandler);

        RecipeManager.Instance.OnCanGlideChanged.AddListener(OnCanGlideChangedHandler);
        RecipeManager.Instance.OnCanDigChanged.AddListener(OnCanDigChangedHandler);
        RecipeManager.Instance.OnCanFireChanged.AddListener(OnCanFireChangedHandler);
        RecipeManager.Instance.OnCanWallJumpChanged.AddListener(OnCanWallJumpChangedHandler);
    }

    public void OnEventTriggered()
    {
        OnCanSceneIntroChangedHandler();
        OnCanSceneEndChangedHandler();
        OnCanSceneChangedHandler();

        OnCanGlideChangedHandler();
        OnCanDigChangedHandler();
        OnCanFireChangedHandler();
        OnCanWallJumpChangedHandler();
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
            yield return new WaitForSeconds(2f);
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
            ShowDialogue(dialogueObject[1]);
            hasShownDialogue = true;
        }
        if (hasShownDialogue)
        {
            hasShownDialogue = false;

        }
    }
    private void OnCanDigChangedHandler()
    {
        if (RecipeManager.Instance.getCanDig() && !hasShownDialogue)
        {
            ShowDialogue(dialogueObject[0]);
            hasShownDialogue = true;
        }
        if (hasShownDialogue)
        {
            hasShownDialogue = false;

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
        if (hasShownDialogue)
        {
            hasShownDialogue = false;

        }
    }

    private void OnCanSceneIntroChangedHandler()
    {
        if (SceneController.Instance.getIsSceneIntro() && !hasShownDialogue)
        {
            ShowDialogue(dialogueObject[6]);
            hasShownDialogue = true;
        }
    }

    private void OnCanSceneEndChangedHandler()
    {
        if (SceneController.Instance.getIsSceneEnd() && !hasShownDialogue)
        {
            ShowDialogue(dialogueObject[7]);
            hasShownDialogue = true;
        }
    }

    private void OnCanSceneChangedHandler()
    {
        if (GameManager.Instance.getIsTutoScene() && !hasShownDialogue)
        {
            ShowDialogue(dialogueObject[3]);
            hasShownDialogue = true;
        }
        else
        {
            if (GameManager.Instance.getIsLevelScene() && !hasShownDialogue)
            {
                ShowDialogue(dialogueObject[5]);
                hasShownDialogue = true;
            }
        }

        if (hasShownDialogue)
        {
            hasShownDialogue = false;
        }
    }
}
