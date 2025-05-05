using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;

public class NotificationManager : MonoBehaviour
{
    private const string ChannelId = "result_channel";

    private void Awake()
    {
        RequestAuthorization();
        CrearCanal();
    }

    public void RequestAuthorization()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
#endif
    }

    private void CrearCanal()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = ChannelId,
            Name = "Notificaciones de Resultado",
            Importance = Importance.Default,
            Description = "Notifica resultados al final de la partida",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void EnviarNotificacionRondaTerminada(int puntaje)
    {
        var notification = new AndroidNotification()
        {
            Title = "Ronda Terminada",
            Text = "Puntaje: " + puntaje,
            LargeIcon = "icon_score",
            FireTime = System.DateTime.Now.AddSeconds(1)
        };

        AndroidNotificationCenter.SendNotification(notification, ChannelId);
    }

    public void EnviarNotificacionNuevoRecord(int puntaje)
    {
        var notification = new AndroidNotification()
        {
            Title = "Nuevo Puntaje Máximo",
            Text = "Puntaje: " + puntaje,
            SmallIcon = "icon_highscore",
            FireTime = System.DateTime.Now.AddSeconds(1)
        };

        AndroidNotificationCenter.SendNotification(notification, ChannelId);
    }
}
