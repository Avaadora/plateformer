using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink_Plateforme : Plateform
{
    private SpriteRenderer SR;

    private Color OriginalColor;
    private Color GhostColor;

    private bool IsSolid = true;

    [SerializeField] private float GhostAlpha = 0.5f;
    [SerializeField] private float SwitchInterval = 2f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        SR = GetComponent<SpriteRenderer>();

        OriginalColor = SR.color;
        GhostColor.a = GhostAlpha;

        InvokeRepeating("SwitchState", SwitchInterval, SwitchInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BeSolid()
    {
        collider2D.enabled = true;
        SR.color = OriginalColor;
    }

    private void BeGhost()
    {
        collider2D.enabled = false;
        SR.color = GhostColor;
    }

    private void SwitchState()
    {
        if (IsSolid)
        {
            BeGhost();
        }
        else
        {
            BeSolid();
        }
        IsSolid = !IsSolid;
    }

}
