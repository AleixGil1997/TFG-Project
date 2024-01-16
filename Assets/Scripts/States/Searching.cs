using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static EnemyFunctions;
using static UnityEngine.GraphicsBuffer;

public class Searching : MonoBehaviour
{
    Quaternion angle;
    float cronometro;
    int direction;
    public float speed = 5.0f;
    float cronoLeft;
    float cronoRight;
    bool tryLeft;
    bool tryRight;

    [HideInInspector] public Vector3 lastKnownPlayerPosition; // Última posición conocida del jugador

    EnemyFunctions inst;
    Transform player; // Referencia al jugador
    Chasing chasing;

    // Start is called before the first frame update
    void Start()
    {
        cronometro = 0;
        cronoLeft = 0;
        cronoRight = 0;
        direction = 0;
        tryLeft = false;
        tryRight = false;
        inst = GetComponent<EnemyFunctions>();
        chasing = GetComponent<Chasing>();
        player = GameObject.Find("Player").transform;

        angle = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
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
            cronoLeft += Time.deltaTime;
            cronoRight += Time.deltaTime;
            /*
            if (cronometro >= 2)
            {
                angle = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
                cronometro = 0;
            }
            */
            transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 1f);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Quaternion.Euler(0, 45f, 0) * transform.forward, out hit, 7f))
            {
                tryLeft = true;
                cronoLeft = 0;
            }
            if (Physics.Raycast(transform.position, Quaternion.Euler(0, -45f, 0) * transform.forward, out hit, 7f))
            {
                tryRight = true;
                cronoRight = 0;
            }

            // Debug.Log("Right: " + tryRight);
            // Debug.Log("Left: " + tryLeft);

            if (cronoLeft >= 1)
            {
                tryLeft = false;
            }
            if(cronoRight >= 1)
            {
                tryRight= false;
            }

            if (cronometro >= 1)
            {
                cronometro = 0;
                direction = Random.Range(0, 2);
            }
            float tmp = inst.dontGetStuck(direction, tryLeft, tryRight);
            if (tmp != 0)
            {
                transform.Rotate(Vector3.up, tmp);
                angle = transform.rotation;
                // cronometro = 0;
            }
        }
    }
}