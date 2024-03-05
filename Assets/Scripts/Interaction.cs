using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] LayerMask ItemLayerMask;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Permet de ramasse un item en appuyant sur E
    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Je touche "+other);
    }
}
