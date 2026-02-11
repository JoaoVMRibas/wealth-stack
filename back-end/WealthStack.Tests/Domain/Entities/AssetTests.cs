using WealthStack.Domain.Entities;
using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;

namespace WealthStack.Tests.Domain.Entities;

public class AssetTests
{
    [Fact]
    public void Should_Create_Asset_With_Valid_Data()
    {
        // Arrange and Act
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);

        // Assert
        Assert.Equal("PETR4", asset.Ticker);
        Assert.Equal("Petrobras", asset.Name);
        Assert.Equal(AssetType.Stock, asset.Type);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void Should_Throw_When_Ticker_Is_Invalid(string invalidTicker)
    {
        // Arrange, Act and Assert
        var exception = Assert.Throws<RequiredFieldException>(
            ()=> new Asset(invalidTicker,"Petrobras",AssetType.Stock)
        );

        Assert.Contains("The 'ticker' field must be filled in, it cannot be empty.",exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void Should_Throw_When_Name_Is_Invalid(string invalidName)
    {
        // Arrange, Act and Assert
        var exception = Assert.Throws<RequiredFieldException>(
            ()=> new Asset("PETR4",invalidName,AssetType.Stock)
        );

        Assert.Contains("The 'name' field must be filled in, it cannot be empty.",exception.Message);
    }

    [Fact]
    public void Should_Normalize_Ticker()
    {
        // Arrange and Act
        var asset = new Asset(" pEtR4 ","Petrobras",AssetType.Stock);

        Assert.Equal("PETR4",asset.Ticker);
    }

    [Fact]
    public void Should_Normalize_Name()
    {
        // Arrange and Act
        var asset = new Asset("PETR4"," Petrobras ",AssetType.Stock);

        Assert.Equal("Petrobras",asset.Name);
    }
}