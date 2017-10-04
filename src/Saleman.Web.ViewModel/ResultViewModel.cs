using Newtonsoft.Json;

namespace Saleman.Web.ViewModel
{
    public class ResultViewModel
    {
        protected ResultViewModel() { }

        [JsonProperty("status")]
        public bool Status { get; set; }

        public static readonly ResultViewModel Success = new ResultViewModel { Status = true };
        public static readonly ResultViewModel Fail = new ResultViewModel { Status = false };
    }

    public sealed class ResultViewModel<TResult> : ResultViewModel
    {
        public static ResultViewModel<T> CreateFailedResult<T>(T data) => new ResultViewModel<T> { Status = false, Data = data };

        public static ResultViewModel<T> CreateSuccessResult<T>(T result) => new ResultViewModel<T>() { Status = true, Data = result };

        [JsonProperty("data")]
        public TResult Data { get; set; }
    }
}
