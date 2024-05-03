using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private Vector3 respawnPosition;

    private void Start()
    {
        // Sauvegarde la position de respawn initiale
        respawnPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Met à jour la position du joueur à la position de respawn
            collision.transform.position = respawnPosition;
        }
    }
}
