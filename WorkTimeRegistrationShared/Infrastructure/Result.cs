namespace WorkTimeRegistrationShared.Infrastructure
{
    public abstract class Result
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
    public abstract class Result<T> : Result where T : class
    {
        public T? ResultValue { get; set; }
        public string? InfoMessage { get; set; }
    }
    public class Success<T> : Result<T> where T : class
    {
        public Success(T resultValue, string? infoMessage = null)
        {
            IsSuccess = true;
            ResultValue = resultValue;
            ErrorMessage = string.Empty;
            InfoMessage = infoMessage;
        }
    }
    public class Failure<T> : Result<T> where T : class
    {
        public Failure(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }

    public class Success : Result
    {
        public Success()
        {
            IsSuccess = true;
        }
    }
}
