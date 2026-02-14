using FluentAssertions;
using WealthStack.Domain.Entities;
using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;

namespace WealthStack.Tests.Domain.Entities;

public class AssetHistoryTests
{
    [Fact]
    public void Should_Create_AssetHistory_With_Valid_Data()
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        var referenceDate = DateTime.UtcNow;

        // Act
        var history = new AssetHistory(asset.Id,27.2m,referenceDate);

        // Assert
        history.AssetId.Should().Be(asset.Id);
        history.Price.Should().Be(27.2m);
        history.ReferenceDate.Should().Be(referenceDate);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_Price_Is_Invalid(decimal invalidPrice)
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        var referenceDate = DateTime.UtcNow;

        // Act
        var act = () => new AssetHistory(asset.Id,invalidPrice,referenceDate);

        // Assert
        act.Should().Throw<InvalidValueException>().WithMessage("Price must be greater than zero.");
    }

    [Fact]
    public void Should_Allow_Date_Up_To_3_Minutes_In_Future()
    {
        var allowedFutureDate = DateTime.UtcNow.AddMinutes(2);

        var history = new AssetHistory(
            assetId: 1,
            price: 100,
            referenceDate: allowedFutureDate
        );

        history.ReferenceDate.Should().Be(allowedFutureDate);
    }

    [Fact]
    public void Should_Allow_Past_Date()
    {
        var pastDate = DateTime.UtcNow.AddDays(-10);

        var history = new AssetHistory(
            assetId: 1,
            price: 100,
            referenceDate: pastDate
        );

        history.ReferenceDate.Should().Be(pastDate);
    }

    [Fact]
    public void Should_Throw_When_ReferenceDate_Is_In_The_Future()
    {
        // Arrange
        var asset = new Asset("PETR4", "Petrobras", AssetType.Stock);
        var invalidFutureDate = DateTime.UtcNow.AddMinutes(10);

        // Act
        var act = () => new AssetHistory(asset.Id, 27.2m, invalidFutureDate);

        // Assert
        act.Should().Throw<FutureDateNotAllowedException>().WithMessage("Reference date cannot be in the future.");
    }
}