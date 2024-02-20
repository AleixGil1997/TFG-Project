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
    private float cronoLeft;
    private float cronoRight;
    private bool tryLeft;
    private bool tryRight;
    private Quaternion angle;
    private int direction;

    private void Start()
    {
        planner = new GoapPlanner();
        cronometro = 0;
        cronoLeft = 0;
        cronoRight = 0;
        tryLeft = false;
        tryRight = false;
    }

    private void Update()
    {
        currentState = CrearEstadoActual();
        Planificar();
        EjecutarPlan();
        cronometro += Time.deltaTime;
        cronoLeft += Time.deltaTime;
        cronoRight+= Time.deltaTime;
    }

    private GoapAgentState CrearEstadoActual()
    {
        GoapAgentState estado = new GoapAgentState();

        // Comprueba si el enemigo lo está persiguiendo
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bool enemyClose = false;

        foreach (GameObject enemy in enemies)
        {
            if (!enemy.GetComponent<Searching>().enabled)
            {
                enemyClose = true;
                break;
            }
        }

        estado.Establecer("EnemigoCerca", enemyClose);
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
        yield return accion.Ejecutar(gameObject);
    }

    public float GetCrono()
    {
        return cronometro;
    }

    public void SetCrono(float crono)
    {
        cronometro = crono;
    }

    public float GetCronoLeft()
    {
        return cronoLeft;
    }

    public void SetCronoLeft(float crono)
    {
        cronoLeft = crono;
    }

    public float GetCronoRight()
    {
        return cronoRight;
    }

    public void SetCronoRight(float crono)
    {
        cronoRight = crono;
    }

    public bool GetTryLeft()
    {
        return tryLeft;
    }

    public void SetTryLeft(bool t)
    {
        tryLeft = t;
    }

    public bool GetTryRight()
    {
        return tryRight;
    }

    public void SetTryRight(bool t)
    {
        tryRight = t;
    }

    public Quaternion GetAngle()
    {
        return angle;
    }

    public void SetAngle(Quaternion ang)
    {
        angle = ang;
    }

    public int GetDirection()
    {
        return direction;
    }

    public void SetDirection(int dir)
    {
        direction = dir;
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
