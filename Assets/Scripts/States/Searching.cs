using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyStateMachine;

public class Searching : MonoBehaviour
{
    public Quaternion angle;
    public float cronometro;

    private EnemyFunctions inst;

    public Transform player; // Referencia al jugador
    public float speed = 5.0f;
    private Vector3 lastKnownPlayerPosition; // Última posición conocida del jugador

    Chasing chasing;
    bool chasingBool;

    // Start is called before the first frame update
    void Start()
    {
        inst = GetComponent<EnemyFunctions>();
        chasing = GetComponent<Chasing>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inst.PlayerDetected(transform))
        {
            lastKnownPlayerPosition = player.position;
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
