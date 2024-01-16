using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyFunctions;

public class Retrieving : MonoBehaviour
{
    public float speed = 5.0f;
    bool turnOnly;

    [HideInInspector] public Vector3 lastKnownPlayerPosition; // Última posición conocida del jugador

    EnemyFunctions inst;
    Transform player; // Referencia al jugador
    Searching searching;
    Chasing chasing;
    float cronometro;

    // Start is called before the first frame update
    void Start()
    {
        inst = GetComponent<EnemyFunctions>();
        searching = GetComponent<Searching>();
        chasing = GetComponent<Chasing>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, lastKnownPlayerPosition, speed * Time.deltaTime);

        if (Math.Abs(transform.position.x - lastKnownPlayerPosition.x) < 0.1f &&
            Math.Abs(transform.position.y - lastKnownPlayerPosition.y) < 0.1f &&
            Math.Abs(transform.position.z - lastKnownPlayerPosition.z) < 0.1f)
        {
            searching.lastKnownPlayerPosition = lastKnownPlayerPosition;
            searching.enabled = true;
            enabled = false;
        }
        else if (inst.PlayerDetected(transform))
        {
            lastKnownPlayerPosition = player.position;
            chasing.lastKnownPlayerPosition = lastKnownPlayerPosition;
            chasing.enabled = true;
            enabled = false;
        }

        cronometro += Time.deltaTime;
        if (cronometro >= 5)
        {
            cronometro = 0;
            int x = UnityEngine.Random.Range(0, 2);
            float tmp = inst.dontGetStuck(x, true, true);
            if (tmp != 0)
            {
                searching.enabled = true;
                enabled = false;
            }
        }
    }
}
