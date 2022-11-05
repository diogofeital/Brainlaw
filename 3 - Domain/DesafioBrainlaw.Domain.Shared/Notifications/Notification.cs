namespace DesafioBrainlaw.Domain.Shared.Notifications
{
    public class Notification
    {
        #region Public Constructors

        public Notification(string message)
        {
            Message = message;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Message { get; }

        #endregion Public Properties
    }
}