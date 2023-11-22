using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyStateMachine;

public class Chasing : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 lastKnownPlayerPosition; // Última posición conocida del jugador

    private EnemyFunctions inst;

    public Transform player; // Referencia al jugador

    Retrieving retrieving;

    // Start is called before the first frame update
    void Start()
    {
        inst = new EnemyFunctions();
        retrieving.GetComponent<Retrieving>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = (lastKnownPlayerPosition - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, lastKnownPlayerPosition, speed * Time.deltaTime);

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
            retrieving.enabled = true;
            enabled = false;
        }
    }
}
