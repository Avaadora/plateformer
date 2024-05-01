using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private Vector3 lastknownPosition;

    private void Start()
    {
        lastknownPosition = Player.transform.position;
        Debug.Log("START "+lastknownPosition);
    }

    // Il faut destroy le GO pour le ré-intantier ça marche faut suivre le tuto
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(Player);
            Instantiate(Player, lastknownPosition, Quaternion.identity);
        }
        Debug.Log("COLLISION "+Player.transform.position);
    }
}
