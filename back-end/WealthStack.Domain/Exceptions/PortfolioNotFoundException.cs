namespace WealthStack.Domain.Exceptions;
public class PortfolioNotFoundException : DomainException
{
    public PortfolioNotFoundException() : base("Portfolio not found.") {}
}