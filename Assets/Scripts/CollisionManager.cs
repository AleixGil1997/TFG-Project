using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    void Start()
    {
        Collider jugadorCollider = GameObject.Find("Player").GetComponent<Collider>();
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemigo in enemigos)
        {
            Collider enemigoCollider = enemigo.GetComponent<Collider>();
            Physics.IgnoreCollision(jugadorCollider, enemigoCollider, true);

            foreach (GameObject enemy in enemigos)
            {
                if (enemy != enemigo)
                {
                    Physics.IgnoreCollision(enemy.GetComponent<Collider>(), enemigoCollider, true);
                }
            }
        }
    }
}
