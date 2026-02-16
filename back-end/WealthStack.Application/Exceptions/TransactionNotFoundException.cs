namespace WealthStack.Application.Exceptions;
public class TransactionNotFoundException : ApplicationException
{
    public TransactionNotFoundException() : base("Transaction not found.") {}
}