using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapHuirAgente : MonoBehaviour
{
    private GoapPlanner planner;
    private GoapAgentState currentState;
    private Queue<GoapAction> currentPlan;

    private static GoapHuirAgente instancia;
    private float cronometro;
    private Quaternion angle;

    private void Start()
    {
        planner = new GoapPlanner();
        cronometro = 0;
    }

    private void Update()
    {
        currentState = CrearEstadoActual();
        Planificar();
        EjecutarPlan();
    }

    private GoapAgentState CrearEstadoActual()
    {
        GoapAgentState estado = new GoapAgentState();

        // Comprueba si el enemigo lo está persiguiendo
        bool enemigoCerca = GameObject.Find("Enemy").GetComponent<Chasing>().enabled;

        estado.Establecer("EnemigoCerca", enemigoCerca); // Ajustar según la lógica de detección
        return estado;
    }

    private void Planificar()
    {
        List<GoapAction> accionesDisponibles = new List<GoapAction>
        {
            new GoapActionHuir()
            // Agrega más acciones según sea necesario
        };

        currentPlan = planner.Planificar(currentState, new GoapAgentState(), accionesDisponibles);
    }

    private void EjecutarPlan()
    {
        if (currentPlan != null && currentPlan.Count > 0)
        {
            GoapAction accionActual = currentPlan.Dequeue();
            StartCoroutine(EjecutarAccion(accionActual));
        }
    }

    private IEnumerator EjecutarAccion(GoapAction accion)
    {
        yield return StartCoroutine(accion.Ejecutar(gameObject));
    }

    public float GetCrono()
    {
        return cronometro;
    }

    public void SetCrono(float crono)
    {
        cronometro = crono;
    }

    public Quaternion GetAngle()
    {
        return angle;
    }

    public void SetAngle(Quaternion ang)
    {
        angle = ang;
    }

    public static GoapHuirAgente Instancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = FindObjectOfType<GoapHuirAgente>();
            }
            return instancia;
        }
    }
}
