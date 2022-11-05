using DesafioBrainlaw.Application.Interfaces;
using Newtonsoft.Json;

namespace DesafioBrainlaw.Application.Models.Common
{
    public class AppServiceResponse<T> : IAppServiceResponse where T : class
    {
        #region Public Constructors

        public AppServiceResponse(T data, string message, bool success)
        {
            Data = data;
            Message = message;
            Success = success;
        }

        #endregion Public Constructors

        #region Public Properties

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        #endregion Public Properties
    }
}