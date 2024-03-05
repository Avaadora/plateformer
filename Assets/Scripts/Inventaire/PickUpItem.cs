using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.instance.AddItem(1);
            Destroy(gameObject);
        }


        Debug.Log("Je touche "+collision);
        InteractInput();
    }
}
