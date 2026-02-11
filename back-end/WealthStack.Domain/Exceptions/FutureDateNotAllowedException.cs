namespace WealthStack.Domain.Exceptions;

public sealed class FutureDateNotAllowedException : DomainException
{
    public FutureDateNotAllowedException()
        : base("Reference date cannot be in the future.") {}
}