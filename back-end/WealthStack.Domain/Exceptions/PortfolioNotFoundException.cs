namespace WealthStack.Domain.Exceptions;
public sealed class PortfolioNotFoundException : DomainException
{
    public PortfolioNotFoundException() : base("Portfolio not found.") {}
}