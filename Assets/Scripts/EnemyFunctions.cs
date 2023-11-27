using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class EnemyFunctions : MonoBehaviour
{
    public float visionAngle = 50;
    public float maxVisionDistance = 50;

    public LayerMask mask;
    public Color visionColor;

    Transform player; // Referencia al jugador

    public bool PlayerDetected(Transform enemy)
    {
        bool ret = false;
        player = GameObject.Find("Player").transform;

        Vector3 targetDirection = player.position - enemy.position;
        float angle = Vector3.Angle(targetDirection, enemy.forward);

        if (angle < visionAngle)
        {
            RaycastHit hit;

            if (Physics.Raycast(enemy.position, targetDirection, out hit, maxVisionDistance, mask))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.transform == player)
                    {
                        Debug.DrawRay(enemy.position, targetDirection, Color.red);
                        ret = true;
                    }
                }
            }
        }
        return ret;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyFunctions))]
public class EnemyVisionSensor : Editor
{
    public void OnSceneGUI()
    {
        var ai = target as EnemyFunctions;

        Vector3 startPoint = Mathf.Cos(-ai.visionAngle * Mathf.Deg2Rad) * ai.transform.forward +
                             Mathf.Sin(ai.visionAngle * Mathf.Deg2Rad) * (-ai.transform.right);

        Handles.color = ai.visionColor;
        Handles.DrawSolidArc(ai.transform.position, Vector3.up, startPoint, ai.visionAngle * 2f, ai.maxVisionDistance);
    }
}
#endif