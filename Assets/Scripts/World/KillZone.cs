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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player.transform.position = lastknownPosition;
        }
        Debug.Log("COLLISION "+Player.transform.position);
    }
}
