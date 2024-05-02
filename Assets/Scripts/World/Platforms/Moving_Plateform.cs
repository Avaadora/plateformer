using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

public class Moving_Plateform : Plateforme_Mobile
{
    private List<Vector2> LastKnowChildPostion = new List<Vector2>();
    private int NextCheckpoint = 0;
    private float Speed = 5.5f;
    private int Sens = 1;

    [SerializeField] private Color textColor = Color.white;
    [SerializeField] private Vector2 TextOffset;

    [SerializeField] private int TextSize = 3;
    [SerializeField] private bool IsPingPong;

    [SerializeField] private float Delay;
    [SerializeField] private float checkPointDelay;

    // Start is called before the first frame update
    void Start()
    {
        LastKnowChildPostion.Add(transform.position);
        for (int i = 0; i < transform.childCount; i++)
        {
            LastKnowChildPostion.Add(transform.GetChild(i).position);
        }
        StartCoroutine(Move());
    }

    private IEnumerator Move() 
    {
        float step = Speed * Time.deltaTime;
        Vector2 target = LastKnowChildPostion[NextCheckpoint];
        transform.position = Vector2.MoveTowards(transform.position, target, step);

        if ((Vector2)transform.position == target)
        {
            yield return new WaitForSeconds(checkPointDelay);

            if (NextCheckpoint == 0)
            {
                yield return new WaitForSeconds(Delay);
            }

            if (NextCheckpoint == LastKnowChildPostion.Count - 1)
            {
                yield return new WaitForSeconds(Delay);
            }

            if ((Sens > 0 && NextCheckpoint < LastKnowChildPostion.Count - 1) || (Sens < 0 && NextCheckpoint > 0))
            {
                NextCheckpoint += Sens;
            }
            else
            {
                if (IsPingPong)
                {
                    Sens -= Sens;
                }
                else
                {
                    NextCheckpoint = 0;
                }
            }
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Move());
    }

    private void OnDrawGizmos()
    {
        GUI.color = textColor;
        GUIStyle labelStyle = GUI.skin.label;
        labelStyle.fontStyle = FontStyle.Bold;
        labelStyle.fontSize = TextSize;

        for (int i = 0; i < transform.childCount; i++)
        {
            Handles.Label((Vector2)transform.GetChild(i).position + TextOffset, i.ToString(), labelStyle);
            if (i < transform.childCount-1)
            {
                Handles.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position, 2f);
            }
            else
            {
                if (!IsPingPong)
                {
                    Handles.DrawLine(transform.GetChild(i).position, transform.GetChild(0).position, 2f);
                }
            }
        }
    }
}
