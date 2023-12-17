public class GoapAgentState
{
    private GoapState state = new GoapState();

    public void Establecer(string clave, object valor)
    {
        state.Agregar(clave, valor);
    }

    public object Obtener(string clave)
    {
        return state.Obtener(clave);
    }
}
