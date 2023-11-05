using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyStateMachine : MonoBehaviour
{
    public float visionAngle = 50;
    public float maxVisionDistance = 50;

    public Color visionColor;
    public LayerMask mask;

    public enum EnemyState
    {
        Searching,
        Chasing,
        Retrieving
    }

    public EnemyState currentState = EnemyState.Searching;

    public Transform player; // Referencia al jugador
    public float speed = 5.0f;
    private Vector3 lastKnownPlayerPosition; // Última posición conocida del jugador

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Searching:
                // Debug.Log("Searching");

                if (PlayerDetected())
                {
                    lastKnownPlayerPosition = player.position;
                    currentState = EnemyState.Chasing;
                }

                break;

            case EnemyState.Chasing:
                // Debug.Log("Chasing");

                Vector3 moveDirection = (lastKnownPlayerPosition - transform.position).normalized;
                transform.position = Vector3.MoveTowards(transform.position, lastKnownPlayerPosition, speed * Time.deltaTime);

                if (moveDirection != Vector3.zero)
                {
                    transform.LookAt(transform.position + moveDirection);
                }

                if (PlayerDetected())
                {
                    lastKnownPlayerPosition = player.position;
                }
                else
                {
                    currentState = EnemyState.Retrieving;
                }

                break;

            case EnemyState.Retrieving:
                // Debug.Log("Retrieving");

                transform.position = Vector3.MoveTowards(transform.position, lastKnownPlayerPosition, speed * Time.deltaTime);

                if (transform.position == lastKnownPlayerPosition)
                {
                    currentState = EnemyState.Searching;
                }
                else if (PlayerDetected())
                {
                    lastKnownPlayerPosition = player.position;
                    currentState = EnemyState.Chasing;
                }

                break;
        }
    }

    // Función para detectar al jugador
    private bool PlayerDetected()
    {
        bool ret = false;

        Vector3 targetDirection = GameObject.Find("Player").transform.position - transform.position;
        float angle = Vector3.Angle(targetDirection, transform.forward);

        if (angle < visionAngle)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, targetDirection, out hit, maxVisionDistance, mask))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.transform == GameObject.Find("Player").transform)
                    {
                        Debug.DrawRay(transform.position, targetDirection, Color.red);
                        ret = true;
                    }
                }
            }
        }

        return ret;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyStateMachine))]
public class EnemyVisionSensor : Editor
{
    public void OnSceneGUI()
    {
        var ai = target as EnemyStateMachine;

        Vector3 startPoint = Mathf.Cos(-ai.visionAngle * Mathf.Deg2Rad) * ai.transform.forward +
                             Mathf.Sin(ai.visionAngle * Mathf.Deg2Rad) * (-ai.transform.right);

        Handles.color = ai.visionColor;
        Handles.DrawSolidArc(ai.transform.position, Vector3.up, startPoint, ai.visionAngle * 2f, ai.maxVisionDistance);
    }
}
#endif