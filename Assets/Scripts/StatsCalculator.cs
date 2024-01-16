using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatsCalculator : MonoBehaviour
{
    private float tiempoTotal;
    private float tiempoConJugadorVisible;
    private float sumaTotal;
    private int bucles;

    // private int prev;

    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        tiempoTotal = 0f;
        tiempoConJugadorVisible = 0f;
        sumaTotal = 0f;
        bucles = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tiempoTotal += Time.deltaTime;
        VerificarVisibilidadJugador();

        if (tiempoTotal >= 60f)
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
        sumaTotal += tiempoConJugadorVisible;
        bucles += 1;
        float porcentajeTiempoVisible = (tiempoConJugadorVisible / tiempoTotal) * 100f;
        Debug.Log("Tiempo: " + tiempoTotal);
        Debug.Log("Porcentaje de tiempo con el jugador a la vista: " + porcentajeTiempoVisible + "%");
        float porcentajeMedio = (sumaTotal / (tiempoTotal * bucles)) * 100f;
        Debug.Log("Porcentaje de tiempo medio: " + porcentajeMedio + "%");
    }
}
