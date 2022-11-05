namespace DesafioBrainlaw.Application.Interfaces
{
    public interface IAppServiceResponse
    {
        #region Public Properties

        string Message { get; set; }
        bool Success { get; set; }

        #endregion Public Properties
    }
}