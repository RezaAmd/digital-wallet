namespace DigitalWallet.Application.Models
{
    public class BaseResult
    {
        public BaseResult(bool isSuccess, List<string>? messages = default)
        {
            IsSuccess = isSuccess;
            if (messages != null && messages.Count > 0)
                Messages = messages;
        }
        public bool IsSuccess { get; private set; }
        public List<string> Messages { get; private set; } = new();
    }
    public class Result : BaseResult
    {
        #region Ctor

        public Result(bool isSuccess, List<string>? messages = default)
            : base(isSuccess, messages) { }

        #endregion
        #region Methods

        public static Result Ok()
            => new(true, []);
        public static Result<TData> Ok<TData>(TData? data = default)
            => new Result<TData>(true, data, []);
        public static Result<TData> Fail<TData>(string? error = null, TData? data = default)
            => new Result<TData>(false, data, [error!]);

        public static Result Fail(string? error = null)
            => new(false, [error!]);
        public static Result Fail(List<string> messages)
            => new(false, messages);

        #endregion
        #region Implicit

        public static implicit operator Result(bool isSuccess)
            => isSuccess ? Ok() : Fail();

        #endregion
    }
    public class Result<TData> : BaseResult
    {
        public TData? Data { get; set; } = default;

        #region Ctor

        public Result(bool isSuccess, TData? data = default, List<string>? messages = default)
            : base(isSuccess, messages)
        {
            Data = data;
        }

        #endregion
        #region Implicit

        public static implicit operator Result(Result<TData> result)
            => new Result(result.IsSuccess, result.Messages);
        public static implicit operator Result<TData>(Result result)
            => new Result<TData>(result.IsSuccess, default, result.Messages);

        #endregion
    }
}