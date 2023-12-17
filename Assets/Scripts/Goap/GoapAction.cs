using System.Collections;
using UnityEngine;

public abstract class GoapAction
{
    public GoapState precondiciones = new GoapState();
    public GoapState efectos = new GoapState();

    public abstract bool RequisitosEvaluados(GoapAgentState estado);

    public abstract IEnumerator Ejecutar(GameObject agente);
}
