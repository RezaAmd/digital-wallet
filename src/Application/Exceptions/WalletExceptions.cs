namespace DigitalWallet.Application.Exceptions;

#region Insufficient Balance
public class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException()
        : base()
    { }

    public InsufficientBalanceException(string message)
        : base(message)
    { }

    public InsufficientBalanceException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
#endregion