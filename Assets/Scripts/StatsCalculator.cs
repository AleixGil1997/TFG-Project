using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatsCalculator : MonoBehaviour
{
    private float tiempoTotal;
    private float tiempoConJugadorVisible;

    // private int prev;

    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        tiempoTotal = 0f;
        tiempoConJugadorVisible = 0f;
        // prev = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tiempoTotal += Time.deltaTime;
        VerificarVisibilidadJugador();

        /*if((int)tiempoTotal % 10 == 0 && (int)tiempoTotal != prev)
        {
            Debug.Log(tiempoTotal);
            Debug.Log(tiempoConJugadorVisible);
            prev = (int)tiempoTotal;
        }*/

        if (tiempoTotal >= 120f)
        {
            CalcularPorcentajeTiempo();
            tiempoTotal = 0f;
            tiempoConJugadorVisible = 0f;
        }
    }

    void VerificarVisibilidadJugador()
    {
        if (enemy.GetComponent<Chasing>().enabled)
        {
            tiempoConJugadorVisible += Time.deltaTime;
        }
    }

    void CalcularPorcentajeTiempo()
    {
        float porcentajeTiempoVisible = (tiempoConJugadorVisible / tiempoTotal) * 100f;
        Debug.Log("Tiempo: " + tiempoTotal);
        Debug.Log("Porcentaje de tiempo con el jugador a la vista: " + porcentajeTiempoVisible + "%");
    }
}
