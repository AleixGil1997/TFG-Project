using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public Collider jugadorCollider = GameObject.Find("Player").GetComponent<Collider>();
    public Collider enemigoCollider = GameObject.Find("Enemy").GetComponent<Collider>();

    void Start()
    {
        Physics.IgnoreCollision(jugadorCollider, enemigoCollider, true);
    }

    void Update()
    {
        
    }
}
