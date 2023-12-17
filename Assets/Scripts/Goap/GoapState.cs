using System.Collections.Generic;

public class GoapState
{
    private Dictionary<string, object> state = new Dictionary<string, object>();

    public void Agregar(string clave, object valor)
    {
        state[clave] = valor;
    }

    public object Obtener(string clave)
    {
        object valor;
        state.TryGetValue(clave, out valor);
        return valor;
    }
}
