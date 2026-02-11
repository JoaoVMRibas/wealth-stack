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
        var referenceDate = DateTime.Now;

        // Act
        var assetHistory = new AssetHistory(asset.Id,27.2m,referenceDate);

        // Assert
        Assert.Equal(asset.Id,assetHistory.AssetId);
        Assert.Equal(27.2m,assetHistory.Price);
        Assert.Equal(referenceDate,assetHistory.ReferenceDate);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_When_Price_Is_Invalid(decimal invalidPrice)
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        var referenceDate = DateTime.Now;

        // Act and Assert
        var exception = Assert.Throws<InvalidValueException>(()=>new AssetHistory(asset.Id,invalidPrice,referenceDate));

        Assert.Contains("Price must be greater than zero.",exception.Message);
    }

    [Fact]
    public void Should_Throw_When_ReferenceDate_Is_In_The_Future()
    {
        // Arrange
        var asset = new Asset("PETR4", "Petrobras", AssetType.Stock);
        var futureDate = DateTime.UtcNow.AddMinutes(10); // 10 min no futuro

        // Act and Assert
        var exception = Assert.Throws<FutureDateNotAllowedException>(() => 
            new AssetHistory(asset.Id, 27.2m, futureDate)
        );

        Assert.Contains("Reference date cannot be in the future.", exception.Message);
    }
}