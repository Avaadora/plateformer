using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && RecipeManager.Instance.getCanDig() && RecipeManager.Instance.getCanGlide() && RecipeManager.Instance.getCanFire())
        {
            SceneController.Instance.LoadNextScene();
        }
    }
}
