using System;

namespace HaveItMain; // Use your actual namespace

public static class NotificationService
{
    public static event Action<string>? OnShowNotification;

    public static void Show(string message)
    {
        OnShowNotification?.Invoke(message);
    }
}