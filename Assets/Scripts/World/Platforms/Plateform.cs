using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform : MonoBehaviour
{
    protected Collider2D collider2D;
    private float ChangeColState = 0.5f;
    protected InputController MyInputAction;

    // Start is called before the first frame update
    protected void Start()
    {
        collider2D = GetComponent<Collider2D>();

        MyInputAction = new InputController();
        MyInputAction.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && MyInputAction.Player.VerticalMove.IsPressed())
        {
            StartCoroutine(ChangeCollisionState());
        }
    }

    private IEnumerator ChangeCollisionState()
    {
        collider2D.enabled = false;
        yield return new WaitForSeconds(ChangeColState);
        collider2D.enabled = true;
    }
}
