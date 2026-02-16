using WealthStack.Domain.Exceptions;

public class InactiveAssetException : DomainException
{
    public InactiveAssetException(string identifier)
        : base($"The asset '{identifier}' is inactive and cannot be traded.") {}
}