namespace WealthStack.Application.Exceptions;
public class InvalidTransactionTypeException : ApplicationException
{
    public InvalidTransactionTypeException(string transactionType) : base($"Invalid Transaction Type: {transactionType}") {}
}