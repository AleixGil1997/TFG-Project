using System.Collections.Generic;
using System.Linq;

public class GoapPlanner
{
    public Queue<GoapAction> Planificar(GoapAgentState estadoActual, GoapAgentState estadoMeta, List<GoapAction> accionesDisponibles)
    {
        // L�gica para planificar acciones y devolver un plan
        // Aqu� deber�as implementar un algoritmo de planificaci�n adecuado, como A* o BFS
        // Este ejemplo simplemente elige la primera acci�n disponible como plan

        Queue<GoapAction> plan = new Queue<GoapAction>();
        GoapAction primeraAccion = accionesDisponibles.FirstOrDefault();

        if (primeraAccion != null)
        {
            plan.Enqueue(primeraAccion);
        }

        return plan;
    }
}
