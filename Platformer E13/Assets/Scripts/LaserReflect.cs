using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LaserReflect : MonoBehaviour {

    public int maxReflectionCount = 5;
    public float maxStepDistance = 200;
    public LineRenderer line;
    private int linePosition = 0;

    private void Update()
    {
        line.positionCount = maxReflectionCount * 2;
        DrawPredictedReflectionPattern(this.transform.position, this.transform.right, maxReflectionCount);

    }

    //void OnDrawGizmos()
    //{
    //    Handles.color = Color.red;
    //    Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(this.transform.position, 0.25f);

    //    DrawPredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
    //}

    private void DrawPredictedReflectionPattern(Vector2 position, Vector2 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining == 0)
        {
            linePosition = 0;
            return;
        }

        Vector2 startingPosition = position;
        
        //Ray2D ray = new Ray2D(position, direction);
        RaycastHit2D hit = Physics2D.Raycast(position, direction);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Reflect")
            {
                direction = Vector2.Reflect(direction, hit.normal);
                position = hit.point;
            }
            else
            {
                direction = Vector2.zero;
                position = hit.point;
            }
        }
        else
        {
            position += direction * maxStepDistance;
        }
        line.SetPosition(linePosition, startingPosition);
        
        linePosition++;
        line.SetPosition(linePosition, position);
        linePosition++;
        //Gizmos.DrawLine(startingPosition, position);

        DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
    }
}
