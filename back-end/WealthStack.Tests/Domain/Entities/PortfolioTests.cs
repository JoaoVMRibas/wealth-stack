using WealthStack.Domain.Entities;
using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Services;

namespace WealthStack.Tests.Domain.Entities;

public class PortfolioTests
{
    private readonly TransactionDomainService _transactionDomainService;
    public PortfolioTests()
    {
        _transactionDomainService = new TransactionDomainService();
    }

    [Fact]
    public void Should_Create_Portfolio_With_Valid_Name()
    {
        // Arrange and Act
        var portfolio = new Portfolio( "Stock portfolios","Portfolio de ações");

        // Assert
        Assert.Equal("Stock portfolios",portfolio.Name);
        Assert.NotNull(portfolio.Transactions);
        Assert.Empty(portfolio.Transactions);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Should_Throw_When_Name_Is_Invalid(string invalidName)
    {
        // Arrrange, Act and Assert
        var exception = Assert.Throws<RequiredFieldException>(
            () => new Portfolio(invalidName,"Portfolio de ações")
        );

        Assert.Equal("name", exception.FieldName);
        Assert.Contains($"The '{exception.FieldName}' field must be filled in, it cannot be empty.",exception.Message);
    }

    [Fact]
    public void Should_Add_Transaction_To_Portfolio()
    {
        // Arrange
        var portfolio = new Portfolio("Stock Portfolio","Portfolio de ações");
        var asset = new Asset("PETR4", "Petrobras", AssetType.Stock);
        
        // Usamos o Service para criar uma transação válida respeitando o 'internal'
        var transaction = _transactionDomainService.Create(
            asset, 
            portfolio, 
            10m, 
            25.50m, 
            2.5m, 
            TransactionType.Buy, 
            DateTime.Now
        );

        // Act
        portfolio.AddTransaction(transaction);

        // Assert
        Assert.Single(portfolio.Transactions);
        Assert.Contains(transaction, portfolio.Transactions);
    }
}