using System;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    private const string CHANNEL_ID = "notification_channel";
    public void ScheduleNotification(DateTime dateTime)
    {
        var notificationChannel = new AndroidNotificationChannel()
        {
            Id = CHANNEL_ID,
            Name = "Notification Channel",
            Description = "Placeholder description",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        var energyNotification = new AndroidNotification
        {
            Title = "Energies Recharged!",
            Text = "Your energy has fully recharged. Come back to play some more!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(energyNotification, CHANNEL_ID);
    }
}
