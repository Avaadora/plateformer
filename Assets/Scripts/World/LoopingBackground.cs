using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    private Transform SpriteChild;
    private float Width;
    private float OriginX;

    [SerializeField] private float SpeedMultiplier = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        OriginX = transform.position.x;
        int NbSpriteChild = transform.childCount;

        for (int i = 0; i < NbSpriteChild; i++)
        {
            SpriteChild = transform.GetChild(i);
            Width = SpriteChild.GetComponent<SpriteRenderer>().bounds.size.x;
            Instantiate(SpriteChild, new Vector3(SpriteChild.position.x + Width, SpriteChild.position.y, 0), SpriteChild.transform.rotation, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * SpeedMultiplier * Time.deltaTime);
        // if (transform.position.x < OriginX - Width)
        // {
        //     transform.position = new Vector3(OriginX, transform.position.y, 0);
        // }

    }
}
