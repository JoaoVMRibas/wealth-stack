using WealthStack.Domain.Entities;
using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Services;

namespace WealthStack.Tests.Domain.Entities;

public class TransactionTests
{
    private readonly TransactionDomainService _transactionDomainService;
    public TransactionTests()
    {
        _transactionDomainService = new TransactionDomainService();
    }

    [Fact]
    public void Should_Create_Transaction_With_Valid_Data()
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        var portfolio = new Portfolio("Stock Portfolio");
        var transactionDate = DateTime.Now;

        // Act
        var transaction = _transactionDomainService.Create
        (
            asset: asset,
            portfolio: portfolio,
            quantity: 10m,
            unitPrice: 25.50m,
            fees: 2.5m,
            TransactionType.Buy,
            transactionDate
        );

        // Assert
        Assert.Equal(asset.Id, transaction.AssetId);
        Assert.Equal(portfolio.Id, transaction.PortfolioId);
        Assert.Equal(10, transaction.Quantity);
        Assert.Equal(25.50m, transaction.UnitPrice);
        Assert.Equal(2.5m, transaction.Fees);
        Assert.Equal(TransactionType.Buy, transaction.Type);
        Assert.Equal(transactionDate,transaction.TransactionDate);
        Assert.Equal(257.50m,transaction.TotalValue);
    }

    [Fact]
    public void Should_Create_Transaction_When_Fractional_Quantity_For_Crypto_Asset()
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Crypto);
        var portfolio = new Portfolio("Stock Portfolio");
        var transactionDate = DateTime.Now;

        // Act
        var transaction = _transactionDomainService.Create
        (
            asset: asset,
            portfolio: portfolio,
            quantity: 3.36m,
            unitPrice: 25.50m,
            fees: 2.5m,
            TransactionType.Buy,
            transactionDate
        );

        // Assert
        Assert.Equal(asset.Id, transaction.AssetId);
        Assert.Equal(portfolio.Id, transaction.PortfolioId);
        Assert.Equal(3.36m, transaction.Quantity);
        Assert.Equal(25.50m, transaction.UnitPrice);
        Assert.Equal(2.5m, transaction.Fees);
        Assert.Equal(TransactionType.Buy, transaction.Type);
        Assert.Equal(transactionDate,transaction.TransactionDate);
    }

    [Theory]
    [InlineData(AssetType.FixedIncome)]
    [InlineData(AssetType.RealEstateFund)]
    [InlineData(AssetType.Stock)]
    [InlineData(AssetType.Etf)]
    public void Should_Throw_When_Fractional_Quantity_For_NonCrypto_Asset(AssetType invalidType)
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",invalidType);
        var portfolio = new Portfolio("Stock Portfolio");
        var qtd = 10.24m;
        
        // Act and Assert
        var exception = Assert.Throws<FractionalQuantityNotAllowedException>(
            () => _transactionDomainService.Create
                (
                    asset: asset,
                    portfolio: portfolio,
                    quantity: qtd,
                    unitPrice: 25.50m,
                    fees: 2.5m,
                    TransactionType.Buy,
                    DateTime.Now
                )   
        );

        Assert.Contains($"Assets of type '{asset.Type}' do not support fractional quantities. Quantity: {qtd}.", exception.Message);
    }

    [Fact]
    public void Should_Throw_When_Asset_Is_Null()
    {
        // Arrange
        var portfolio = new Portfolio("Stock Portfolio");
        
        // Act and Assert
        var exception = Assert.Throws<AssetNotFoundException>(
            () => _transactionDomainService.Create
                (
                    asset: null!,
                    portfolio: portfolio,
                    quantity: 10,
                    unitPrice: 25.50m,
                    fees: 2.5m,
                    TransactionType.Buy,
                    DateTime.Now
                )   
        );

        Assert.Contains("Asset not found.", exception.Message);
    }

    [Fact]
    public void Should_Throw_When_Portfolio_Is_Null()
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        
        // Act and Assert
        var exception = Assert.Throws<PortfolioNotFoundException>(
            () => _transactionDomainService.Create
                (
                    asset: asset,
                    portfolio: null!,
                    quantity: 10,
                    unitPrice: 25.50m,
                    fees: 2.5m,
                    TransactionType.Buy,
                    DateTime.Now
                )   
        );

        Assert.Contains("Portfolio not found.", exception.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_Quantity_Is_Invalid(decimal invalidQuantity)
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        var portfolio = new Portfolio("Stock Portfolio");

        var exception = Assert.Throws<InvalidValueException>(
            () => _transactionDomainService.Create
                (
                    asset: asset,
                    portfolio: portfolio,
                    quantity: invalidQuantity,
                    unitPrice: 25.50m,
                    fees: 2.5m,
                    TransactionType.Buy,
                    DateTime.Now
                )   
        );

        Assert.Contains("The quantity must be greater than zero.", exception.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_UnitPrice_Is_Invalid(decimal invalidUnitPrice)
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        var portfolio = new Portfolio("Stock Portfolio");

        var exception = Assert.Throws<InvalidValueException>(
            () => _transactionDomainService.Create
                (
                    asset: asset,
                    portfolio: portfolio,
                    quantity: 10,
                    unitPrice: invalidUnitPrice,
                    fees: 2.5m,
                    TransactionType.Buy,
                    DateTime.Now
                )   
        );

        Assert.Contains("The price per unit must be greater than zero.", exception.Message);
    }

    [Fact]
    public void Should_Throw_When_Fees_Is_Negative()
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        var portfolio = new Portfolio("Stock Portfolio");

        var exception = Assert.Throws<InvalidValueException>(
            () => _transactionDomainService.Create
                (
                    asset: asset,
                    portfolio: portfolio,
                    quantity: 10,
                    unitPrice: 25.50m,
                    fees: -1,
                    TransactionType.Buy,
                    DateTime.Now
                )   
        );

        Assert.Contains("Fees cannot be negative.", exception.Message);
    }
}