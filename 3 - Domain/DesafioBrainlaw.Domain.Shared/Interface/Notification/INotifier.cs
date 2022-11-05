namespace DesafioBrainlaw.Domain.Shared.Interface.Notification
{
    public interface INotifier
    {
        List<Notifications.Notification> GetAllNotifications();

        void Handle(Notifications.Notification notification);

        bool HasNotification();
    }
}