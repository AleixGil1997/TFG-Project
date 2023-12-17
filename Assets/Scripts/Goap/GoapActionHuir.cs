using System.Collections;
using UnityEngine;

public class GoapActionHuir : GoapAction
{
    private bool huyendo = false;

    // Quaternion angle; S'han d'agafar des de una altra classe que sigui MonoBehaviour (probablement GoapHuirAgente)
    // float cronometro = 0;
    public float speed = 10.0f;

    public GoapActionHuir()
    {
        precondiciones.Agregar("EnemigoCerca", true);
        efectos.Agregar("EnemigoCerca", false);
        efectos.Agregar("Escapando", true);
    }

    public override bool RequisitosEvaluados(GoapAgentState estado)
    {
        return (bool)estado.Obtener("EnemigoCerca");
    }

    public override IEnumerator Ejecutar(GameObject agente)
    {
        if (!huyendo)
        {
            huyendo = true;
            // Debug.Log("Huyendo del enemigo...");

            GameObject enemy = GameObject.Find("Enemy");

            // Lógica específica de huida
            Vector3 direccionContraria = agente.transform.position - enemy.transform.position;

            // Continuar huyendo hasta que el script "Chasing" del enemigo esté activo
            if (enemy.GetComponent<Chasing>() != null && !enemy.GetComponent<Searching>().enabled)
            {
                // Debug.Log(GoapHuirAgente.Instancia.GetCrono());

                GoapHuirAgente.Instancia.SetCrono(GoapHuirAgente.Instancia.GetCrono() + Time.deltaTime);
                if (GoapHuirAgente.Instancia.GetCrono() >= 2)
                {
                    GoapHuirAgente.Instancia.SetAngle(Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0));
                    GoapHuirAgente.Instancia.SetCrono(0);
                }
                agente.transform.rotation = Quaternion.RotateTowards(agente.transform.rotation, GoapHuirAgente.Instancia.GetAngle(), 1f);
                agente.transform.Translate(Vector3.forward * Time.deltaTime * speed);

                RaycastHit hit;
                if (Physics.Raycast(agente.transform.position, agente.transform.forward, out hit, 1f))
                {
                    agente.transform.Rotate(Vector3.up, 90f);
                    GoapHuirAgente.Instancia.SetAngle(agente.transform.rotation);
                    GoapHuirAgente.Instancia.SetCrono(0);
                    Debug.Log("Huyendo del enemigo...");
                }

                yield return null;
            }

            huyendo = false;
        }

        yield return null;
    }
}
