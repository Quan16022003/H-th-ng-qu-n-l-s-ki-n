namespace Domain.ValueObjects
{
    public class Result
    {
        protected Result(bool success, Error error)
        {
            ArgumentNullException.ThrowIfNull(error, "Error cannot be null");
            IsSuccess = success;
            Error = error;
        }

        private Result() : this(true, Error.None) { }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public static Result Success() => new();

        public static Result Failure(Error error) => new(false, error);

        // Ensure Error.None is defined appropriately elsewhere in your project.
    }

    public class Result<T> : Result
    {
        private readonly T _value;

        private Result(T value) : base(true, Error.None)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value), "Value cannot be null when result is successful");
        }

        private Result(Error error) : base(false, error) {
            _value = default!;
        }

        public T Value
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException("Cannot access value when result is a failure.");
                }
                return _value;
            }
        }

        public static Result<T> Success(T value) => new(value);

        public static new Result<T> Failure(Error error) => new(error);
    }
}