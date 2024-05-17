using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private Vector3 respawnPosition;
    private GameObject Player;

    private void Start()
    {
        // Sauvegarde la position de respawn initiale
        respawnPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Met à jour la position du joueur à la position de respawn
            StartCoroutine(RespawnPlayer(0.5f));
        }
        CheckPowerUpAndUI();
    }

    private void CheckPowerUpAndUI()
    {
        if (!(RecipeManager.Instance.getCanGlide() || RecipeManager.Instance.getCanDig() || RecipeManager.Instance.getCanFire() || RecipeManager.Instance.getCanWallJump()))
        {
            RecipeManager.Instance.ClearCheckmarks();
            RecipeManager.Instance.ClearUI();
        }
    }

    IEnumerator RespawnPlayer(float respawn)
    {
        yield return new WaitForSeconds(respawn);
        Player.transform.position = respawnPosition;
    }
}
