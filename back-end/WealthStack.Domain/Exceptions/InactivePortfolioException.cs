using WealthStack.Domain.Exceptions;

public class InactivePortfolioException : DomainException
{
    public InactivePortfolioException(string identifier)
        : base($"The portfolio '{identifier}' is inactive.") {}
}