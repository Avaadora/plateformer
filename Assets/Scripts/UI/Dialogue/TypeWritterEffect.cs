using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TypeWritterEffect : MonoBehaviour
{
    private float WrittingSpeed = 20f;

    public Coroutine Run(string textToType, TMP_Text LabelText)
    {
        return StartCoroutine(TypeText(textToType, LabelText));
    }

    private IEnumerator TypeText(string textToType, TMP_Text LabelText)
    {
        LabelText.text = string.Empty;

        float lastTimeWritting = 0f;  // Timer de la dernière fois qu'on a écrit qqch
        int characterIndex = 0; // Nombre de caractères à écrire

        while (characterIndex < textToType.Length)
        {
            lastTimeWritting += Time.deltaTime * WrittingSpeed;
            characterIndex = Mathf.FloorToInt(lastTimeWritting);
            characterIndex = Mathf.Clamp(characterIndex, 0, textToType.Length);

            LabelText.text = textToType.Substring(0, characterIndex);

            yield return null;
        }

        LabelText.text = textToType;
    }
}
