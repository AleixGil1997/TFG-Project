using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static EnemyFunctions;
using static UnityEngine.GraphicsBuffer;

public class Searching : MonoBehaviour
{
    Quaternion angle;
    float cronometro;
    public float speed = 5.0f;

    [HideInInspector] public Vector3 lastKnownPlayerPosition; // Última posición conocida del jugador

    EnemyFunctions inst;
    Transform player; // Referencia al jugador
    Chasing chasing;

    // Start is called before the first frame update
    void Start()
    {
        inst = GetComponent<EnemyFunctions>();
        chasing = GetComponent<Chasing>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (inst.PlayerDetected(transform))
        {
            lastKnownPlayerPosition = player.position;
            chasing.lastKnownPlayerPosition = lastKnownPlayerPosition;
            chasing.enabled = true;
            enabled = false;
        }
        else
        {
            cronometro += Time.deltaTime;
            if (cronometro >= 2)
            {
                angle = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
                cronometro = 0;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 1f);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
            {
                transform.Rotate(Vector3.up, 90f);
                angle = transform.rotation;
                cronometro = 0;
            }
        }
    }
}