using System.Collections.Generic;
using System.Linq;

public class GoapPlanner
{
    public Queue<GoapAction> Planificar(GoapAgentState estadoActual, GoapAgentState estadoMeta, List<GoapAction> accionesDisponibles)
    {
        // Lógica para planificar acciones y devolver un plan
        // Aquí deberías implementar un algoritmo de planificación adecuado, como A* o BFS
        // Este ejemplo simplemente elige la primera acción disponible como plan

        Queue<GoapAction> plan = new Queue<GoapAction>();
        GoapAction primeraAccion = accionesDisponibles.FirstOrDefault();

        if (primeraAccion != null)
        {
            plan.Enqueue(primeraAccion);
        }

        return plan;
    }
}
