using FluentAssertions;
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
    public void Should_Create_Portfolio_With_Valid_Data()
    {
        // Arrange
        var name = "Stock portfolios";
        var description = "Portfolio reserved for stocks.";

        // Act
        var portfolio = new Portfolio(name, description);

        // Assert
        portfolio.Name.Should().Be(name);
        portfolio.Description.Should().Be(description);
        portfolio.Transactions.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Should_Throw_When_Name_Is_Invalid(string invalidName)
    {
        // Act
        Action act = () => new Portfolio(invalidName, "Portfolio reserved for stocks.");

        // Assert
        var exception = act.Should().Throw<RequiredFieldException>().Which;
        exception.FieldName.Should().Be("name");
        exception.Message.Should().Be("The 'name' field must be filled in, it cannot be empty.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Should_Throw_When_Descrption_Is_Invalid(string invalidDescription)
    {
        // Act
        Action act = () => new Portfolio("Stock portfolios", invalidDescription);

        // Assert
        var exception = act.Should().Throw<RequiredFieldException>().Which;
        exception.FieldName.Should().Be("description");
        exception.Message.Should().Be("The 'description' field must be filled in, it cannot be empty.");
    }

    [Fact]
    public void Update_Should_Update_Properties_When_Data_Is_Valid()
    {
        // Arrange
        var portfolio = new Portfolio("Old Name", "Old Description");

        // Act
        portfolio.Update("New Name", "New Description");

        // Assert
        portfolio.Name.Should().Be("New Name");
        portfolio.Description.Should().Be("New Description");
    }

    [Fact]
    public void Update_Should_Throw_When_Name_Is_Invalid()
    {
        // Arrange and Act
        var portfolio = new Portfolio("Name", "Description");
        var act = () => portfolio.Update(string.Empty, "Valid description");

        // Assert
        var exception = act.Should().Throw<RequiredFieldException>().Which;
        exception.FieldName.Should().Be("name");
        exception.Message.Should().Be("The 'name' field must be filled in, it cannot be empty.");
    }

    [Fact]
    public void Update_Should_Throw_When_Description_Is_Invalid()
    {
        // Arrange and Act
        var portfolio = new Portfolio("Name", "Description");
        var act = () => portfolio.Update("Valid name", string.Empty);

        // Assert
        var exception = act.Should().Throw<RequiredFieldException>().Which;
        exception.FieldName.Should().Be("description");
        exception.Message.Should().Be("The 'description' field must be filled in, it cannot be empty.");
    }
}