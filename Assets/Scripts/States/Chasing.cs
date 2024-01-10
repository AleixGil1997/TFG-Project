using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyFunctions;

public class Chasing : MonoBehaviour
{
    public float speed = 5.0f;

    [HideInInspector] public Vector3 lastKnownPlayerPosition; // Última posición conocida del jugador

    EnemyFunctions inst;
    Transform player; // Referencia al jugador
    Retrieving retrieving;

    // Start is called before the first frame update
    void Start()
    {
        inst = GetComponent<EnemyFunctions>();
        retrieving = GetComponent<Retrieving>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = (lastKnownPlayerPosition - transform.position).normalized;
        if(Vector3.Distance(transform.position, lastKnownPlayerPosition) > 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastKnownPlayerPosition, speed * Time.deltaTime);
        }

        if (moveDirection != Vector3.zero)
        {
            transform.LookAt(transform.position + moveDirection);
        }

        if (inst.PlayerDetected(transform))
        {
            lastKnownPlayerPosition = player.position;
        }
        else
        {
            retrieving.lastKnownPlayerPosition = lastKnownPlayerPosition;
            retrieving.enabled = true;
            enabled = false;
        }
    }
}
