using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
    }
}
