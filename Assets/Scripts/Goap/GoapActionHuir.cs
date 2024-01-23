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

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            bool enemySawPlayer = false;

            foreach (GameObject enemy in enemies)
            {
                if (!enemy.GetComponent<Searching>().enabled)
                {
                    enemySawPlayer = true;
                    break;
                }
            }

            // Lógica específica de huida
            // Vector3 direccionContraria = agente.transform.position - enemy.transform.position;

            // Continuar huyendo hasta que el script "Chasing" del enemigo no esté activo
            if (enemySawPlayer)
            {
                // Debug.Log(GoapHuirAgente.Instancia.GetCrono());
                /*
                GoapHuirAgente.Instancia.SetCrono(GoapHuirAgente.Instancia.GetCrono() + Time.deltaTime);
                if (GoapHuirAgente.Instancia.GetCrono() >= 2)
                {
                    GoapHuirAgente.Instancia.SetAngle(Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0));
                    GoapHuirAgente.Instancia.SetCrono(0);
                }
                */
                agente.transform.rotation = Quaternion.RotateTowards(agente.transform.rotation, GoapHuirAgente.Instancia.GetAngle(), 1f);
                agente.transform.Translate(Vector3.forward * Time.deltaTime * speed);

                if (GoapHuirAgente.Instancia.GetCrono() >= 1)
                {
                    GoapHuirAgente.Instancia.SetCrono(0);
                    GoapHuirAgente.Instancia.SetDirection(Random.Range(0, 2));
                }

                RaycastHit hit;

                if (Physics.Raycast(agente.transform.position, Quaternion.Euler(0, 45f, 0) * agente.transform.forward, out hit, 7f))
                {
                    GoapHuirAgente.Instancia.SetTryLeft(true);
                    GoapHuirAgente.Instancia.SetCronoLeft(0);
                }
                if (Physics.Raycast(agente.transform.position, Quaternion.Euler(0, -45f, 0) * agente.transform.forward, out hit, 7f))
                {
                    GoapHuirAgente.Instancia.SetTryRight(true);
                    GoapHuirAgente.Instancia.SetCronoRight(0);
                }

                if (GoapHuirAgente.Instancia.GetCronoLeft() >= 1)
                {
                    GoapHuirAgente.Instancia.SetTryLeft(false);
                }
                if (GoapHuirAgente.Instancia.GetCronoRight() >= 1)
                {
                    GoapHuirAgente.Instancia.SetTryRight(false);
                }

                if (Physics.Raycast(agente.transform.position, Quaternion.Euler(0, 15f, 0) * agente.transform.forward, out hit, 3f))
                {
                    agente.transform.Rotate(Vector3.up, -5f);
                    GoapHuirAgente.Instancia.SetAngle(agente.transform.rotation);
                    GoapHuirAgente.Instancia.SetCrono(0);
                }
                else if (Physics.Raycast(agente.transform.position, Quaternion.Euler(0, -15f, 0) * agente.transform.forward, out hit, 3f))
                {
                    agente.transform.Rotate(Vector3.up, 5f);
                    GoapHuirAgente.Instancia.SetAngle(agente.transform.rotation);
                    GoapHuirAgente.Instancia.SetCrono(0);
                }

                else if (Physics.Raycast(agente.transform.position, Quaternion.Euler(0, 45f, 0) * agente.transform.forward, out hit, 1.5f))
                {
                    agente.transform.Rotate(Vector3.up, -2f);
                    GoapHuirAgente.Instancia.SetAngle(agente.transform.rotation);
                    GoapHuirAgente.Instancia.SetCrono(0);
                }
                else if (Physics.Raycast(agente.transform.position, Quaternion.Euler(0, -45f, 0) * agente.transform.forward, out hit, 1.5f))
                {
                    agente.transform.Rotate(Vector3.up, 2f);
                    GoapHuirAgente.Instancia.SetAngle(agente.transform.rotation);
                    GoapHuirAgente.Instancia.SetCrono(0);
                }

                else if (GoapHuirAgente.Instancia.GetDirection() == 0)
                {
                    if (GoapHuirAgente.Instancia.GetTryLeft() && !Physics.Raycast(agente.transform.position, Quaternion.Euler(0, 45f, 0) * agente.transform.forward, out hit, 6f))
                    {
                        agente.transform.Rotate(Vector3.up, 2f);
                        GoapHuirAgente.Instancia.SetAngle(agente.transform.rotation);
                        GoapHuirAgente.Instancia.SetCrono(0);
                    }
                    else if (GoapHuirAgente.Instancia.GetTryRight() && !Physics.Raycast(agente.transform.position, Quaternion.Euler(0, -45f, 0) * agente.transform.forward, out hit, 6f))
                    {
                        agente.transform.Rotate(Vector3.up, -2f);
                        GoapHuirAgente.Instancia.SetAngle(agente.transform.rotation);
                        GoapHuirAgente.Instancia.SetCrono(0);
                    }
                }
                else if (GoapHuirAgente.Instancia.GetDirection() == 1)
                {
                    if (GoapHuirAgente.Instancia.GetTryRight() && !Physics.Raycast(agente.transform.position, Quaternion.Euler(0, -45f, 0) * agente.transform.forward, out hit, 6f))
                    {
                        agente.transform.Rotate(Vector3.up, -2f);
                        GoapHuirAgente.Instancia.SetAngle(agente.transform.rotation);
                        GoapHuirAgente.Instancia.SetCrono(0);
                    }
                    else if (GoapHuirAgente.Instancia.GetTryLeft() && !Physics.Raycast(agente.transform.position, Quaternion.Euler(0, 45f, 0) * agente.transform.forward, out hit, 6f))
                    {
                        agente.transform.Rotate(Vector3.up, 2f);
                        GoapHuirAgente.Instancia.SetAngle(agente.transform.rotation);
                        GoapHuirAgente.Instancia.SetCrono(0);
                    }
                }

                yield return null;
            }

            huyendo = false;
        }

        yield return null;
    }
}
