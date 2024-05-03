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
    }

    public void OnEventTriggered()
    {
        if (RecipeManager.Instance.getCanGlide() && !hasShownDialogue)
        {
            ShowDialogue(dialogueObject[0]);
            hasShownDialogue = true;
        }
        else
        {
            if (RecipeManager.Instance.getCanDig() && !hasShownDialogue)
            {
                ShowDialogue(dialogueObject[1]);
                hasShownDialogue = true;

            }
            else
            {
                if (RecipeManager.Instance.getCanFire() && !hasShownDialogue)
                {
                    ShowDialogue(dialogueObject[2]);
                    hasShownDialogue = true;

                }

            }
        }
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
            yield return new WaitForSeconds(2);
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
        if (RecipeManager.Instance.getCanGlide())
        {
            ShowDialogue(dialogueObject[0]);
        }
    }
    private void OnCanDigChangedHandler()
    {
        if (RecipeManager.Instance.getCanDig())
        {
            ShowDialogue(dialogueObject[1]);
        }
    }

    private void OnCanFireChangedHandler()
    {
        if (RecipeManager.Instance.getCanFire())
        {
            ShowDialogue(dialogueObject[2]);
        }
    }
        private void OnCanWallJumpChangedHandler()
    {
        if (RecipeManager.Instance.getCanWallJump())
        {
            ShowDialogue(dialogueObject[4]);
        }
    }
}
