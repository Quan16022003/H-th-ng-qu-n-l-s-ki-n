namespace Domain.ValueObjects
{
    public static class ErrorExtension
    {
        public static bool HasError(this Error error1, Error error2)
        {
            return error1 == error2;
        }
    }
}