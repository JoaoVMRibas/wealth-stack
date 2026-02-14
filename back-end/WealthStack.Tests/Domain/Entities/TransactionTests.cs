using FluentAssertions;
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
        var portfolio = new Portfolio("Stock Portfolio","Portfolio de ações");
        var transactionDate = DateTime.UtcNow;

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
        transaction.AssetId.Should().Be(asset.Id);
        transaction.PortfolioId.Should().Be(portfolio.Id);
        transaction.Quantity.Should().Be(10);
        transaction.UnitPrice.Should().Be(25.50m);
        transaction.Fees.Should().Be(2.5m);
        transaction.Type.Should().Be(TransactionType.Buy);
        transaction.TransactionDate.Should().Be(transactionDate);
        transaction.TotalValue.Should().Be(257.50m);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_Quantity_Is_Invalid(decimal invalidQuantity)
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        var portfolio = new Portfolio("Stock Portfolio","Portfolio de ações");

        // Act
        var act = () => _transactionDomainService.Create(
            asset,
            portfolio,
            invalidQuantity,
            25.50m,
            2.5m,
            TransactionType.Buy,
            DateTime.UtcNow
        );

        // Assert
        act.Should().Throw<InvalidValueException>().WithMessage("The quantity must be greater than zero.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_UnitPrice_Is_Invalid(decimal invalidUnitPrice)
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        var portfolio = new Portfolio("Stock Portfolio","Portfolio de ações");

        // Act
        var act = () => _transactionDomainService.Create(
            asset,
            portfolio,
            10,
            invalidUnitPrice,
            2.5m,
            TransactionType.Buy,
            DateTime.UtcNow
        );

        // Assert
        act.Should().Throw<InvalidValueException>().WithMessage("The price per unit must be greater than zero.");
    }

    [Fact]
    public void Should_Throw_When_Fees_Is_Negative()
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        var portfolio = new Portfolio("Stock Portfolio","Portfolio de ações");

        // Act
        var act = () => _transactionDomainService.Create(
            asset,
            portfolio,
            10,
            12.55m,
            -1,
            TransactionType.Buy,
            DateTime.UtcNow
        );

        // Assert
        act.Should().Throw<InvalidValueException>().WithMessage("Fees cannot be negative.");
    }
}