using FluentAssertions;
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
        var asset = new Asset("PeTr4 "," Petrobras ",AssetType.Stock);

        // Assert
        asset.Ticker.Should().MatchRegex("^[A-Z0-9]+$");
        asset.Ticker.Should().Be("PETR4");
        asset.Name.Should().Be("Petrobras");
        asset.Type.Should().Be(AssetType.Stock);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void Should_Throw_When_Ticker_Is_Invalid(string invalidTicker)
    {
        // Arrange and Act
        Action act = ()=> new Asset(invalidTicker,"Petrobras",AssetType.Stock);

        // Assert
        act.Should().Throw<RequiredFieldException>().WithMessage("The 'ticker' field must be filled in, it cannot be empty.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void Should_Throw_When_Name_Is_Invalid(string invalidName)
    {
        // Arrange and Act
        var act = ()=> new Asset("PETR4",invalidName,AssetType.Stock);

        // Assert
        act.Should().Throw<RequiredFieldException>().WithMessage("The 'name' field must be filled in, it cannot be empty.");
    }

    [Fact]
    public void Should_Normalize_Ticker()
    {
        // Arrange and Act
        var asset = new Asset(" pEtR4 ","Petrobras",AssetType.Stock);

        // Assert
        asset.Ticker.Should().MatchRegex("^[A-Z0-9]+$");
        asset.Ticker.Should().Be("PETR4");
    }

    [Fact]
    public void Should_Normalize_Name()
    {
        // Arrange and Act
        var asset = new Asset("PETR4"," Petrobras ",AssetType.Stock);

        // Assert
        asset.Name.Should().Be("Petrobras");
    }

    [Fact]
    public void Should_Update_With_Valid_Data()
    {
        // Arrange
        var asset = new Asset("PETR4", "Petrobras", AssetType.Stock);

        // Act
        asset.Update("vale3", " Vale ", AssetType.Stock);

        // Assert
        asset.Ticker.Should().MatchRegex("^[A-Z0-9]+$");
        asset.Ticker.Should().Be("VALE3");
        asset.Name.Should().Be("Vale");
    }

    [Fact]
    public void Should_Not_Allow_Invalid_Update()
    {
        // Arrange
        var asset = new Asset("PETR4", "Petrobras", AssetType.Stock);

        // Act
        var act = () => asset.Update(string.Empty, "Vale", AssetType.Stock);
    
        // Assert
        act.Should().Throw<RequiredFieldException>().WithMessage("The 'ticker' field must be filled in, it cannot be empty.");
    }
}