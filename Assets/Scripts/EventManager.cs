using System;

namespace Assets.Scripts
{
    public static class EventManager
    {
        public static Action<int> OnPuntajeGanado;
        public static event Action OnSceneChanged;
        public static event Action<PlayerDataSO> OnGameStarted;
        public static event Action OnGameEnded;

        public static void PuntajeGanado(int puntos)
        {
            OnPuntajeGanado?.Invoke(puntos);
        }
        public static void InvokeSceneChanged()
        {
            OnSceneChanged?.Invoke();
        }

        public static void InvokeGameStarted(PlayerDataSO playerData)
        {
            OnGameStarted?.Invoke(playerData);
        }

        public static void InvokeGameEnded()
        {
            OnGameEnded?.Invoke();
        }
    }
}

