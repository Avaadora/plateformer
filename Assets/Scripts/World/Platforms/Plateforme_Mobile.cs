using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plateforme_Mobile : Plateform
{
    protected Dictionary<Transform, Transform> Famille;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (TestTag(other.gameObject))
        {
            Famille.Add(other.gameObject.transform, other.gameObject.transform.parent);
            other.gameObject.transform.SetParent(transform, true); // Conservation de la position de l'enfant par rapport au monde
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (TestTag(other.gameObject))
        {
            Transform AncienParent;
            Famille.TryGetValue(other.gameObject.transform, out AncienParent);
            other.gameObject.transform.SetParent(AncienParent, true); // Conservation de la position de l'enfant par rapport au monde
            Famille.Remove(other.gameObject.transform);
        }
    }

    private bool TestTag(GameObject go)
    {
        return (go.gameObject.CompareTag("Player") || go.gameObject.CompareTag("Ennemy"));
    }
}
