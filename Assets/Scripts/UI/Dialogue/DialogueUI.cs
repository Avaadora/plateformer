using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject DialogueBox;
    [SerializeField] private TMP_Text LabelText;
    [SerializeField] private DialogueObject dialogueObject;

    private TypeWritterEffect typeWritterEffect;

    private void Start()
    {
        typeWritterEffect = GetComponent<TypeWritterEffect>();
        CloseDialogueBox();
    }

    private void OnEnable()
    {
        ShowDialogue(dialogueObject);
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
}
