using System;

namespace Assets.Scripts
{
    public static class EventManager
    {
        public static Action<int> OnPuntajeGanado;

        public static void PuntajeGanado(int puntos)
        {
            OnPuntajeGanado?.Invoke(puntos);
        }
    }
}

