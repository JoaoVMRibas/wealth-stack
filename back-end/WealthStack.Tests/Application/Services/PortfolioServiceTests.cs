using FluentAssertions;
using NSubstitute;
using WealthStack.Application.DTOs.Portfolios.Requests;
using WealthStack.Application.Services;
using WealthStack.Domain.Entities;
using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Tests.Application.Services;

public class PortfolioServiceTests
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PortfolioService _service;

    public PortfolioServiceTests()
    {
        _portfolioRepository = Substitute.For<IPortfolioRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();

        _service = new PortfolioService(_portfolioRepository, _unitOfWork);
    }

    [Fact]
    public async Task CreateAsync_Should_Create_Portfolio_When_Valid()
    {
        // Arrange
        var request = new PortfolioRequest("Tech Portfolio", "Long term investments");

        // Act
        var result = await _service.CreateAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Tech Portfolio");
        result.Description.Should().Be("Long term investments");

        await _portfolioRepository.Received(1).AddAsync(Arg.Any<Portfolio>());
        await _unitOfWork.Received(1).CommitAsync();
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Portfolio_When_Found()
    {
        // Arrange
        var portfolio = new Portfolio("Tech Portfolio", "Long term");

        _portfolioRepository.GetByIdAsync(1).Returns(portfolio);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Tech Portfolio");
    }

    [Fact]
    public async Task GetByIdAsync_Should_Throw_When_Not_Found()
    {
        // Arrange
        _portfolioRepository.GetByIdAsync(1).Returns((Portfolio?)null);

        // Act
        var act = () => _service.GetByIdAsync(1);

        // Assert
        await act.Should().ThrowAsync<PortfolioNotFoundException>();
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_Mapped_List()
    {
        // Arrange
        var portfolios = new List<Portfolio>
        {
            new Portfolio("Tech", "Desc1"),
            new Portfolio("Crypto", "Desc2")
        };

        _portfolioRepository.GetAllAsync().Returns(portfolios);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.First().Name.Should().Be("Tech");
        result.Last().Name.Should().Be("Crypto");
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_Empty_When_No_Data()
    {
        // Arrange
        _portfolioRepository.GetAllAsync().Returns(new List<Portfolio>());

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_And_Commit_When_Found()
    {
        // Arrange
        var portfolio = new Portfolio("Old", "OldDesc");

        _portfolioRepository.GetByIdAsync(1).Returns(portfolio);

        // Act
        var request = new PortfolioRequest("New", "NewDesc");

        await _service.UpdateAsync(1, request);

        // Assert
        _portfolioRepository.Received(1).Update(portfolio);
        await _unitOfWork.Received(1).CommitAsync();
    }

    [Fact]
    public async Task UpdateAsync_Should_Throw_When_Not_Found()
    {
        // Arrange
        _portfolioRepository.GetByIdAsync(1).Returns((Portfolio?)null);

        var request = new PortfolioRequest("New", "NewDesc");

        // Act
        var act = () => _service.UpdateAsync(1, request);

        await act.Should().ThrowAsync<PortfolioNotFoundException>();

        // Assert
        _portfolioRepository.DidNotReceive().Update(Arg.Any<Portfolio>());
        await _unitOfWork.DidNotReceive().CommitAsync();
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_And_Commit_When_Found()
    {
        // Arrange
        var portfolio = new Portfolio("Tech", "Desc");

        _portfolioRepository.GetByIdAsync(1).Returns(portfolio);

        // Act
        await _service.DeleteAsync(1);

        // Assert
        _portfolioRepository.Received(1).Remove(portfolio);
        await _unitOfWork.Received(1).CommitAsync();
    }

    [Fact]
    public async Task DeleteAsync_Should_Throw_When_Not_Found()
    {
        // Arrange
        _portfolioRepository.GetByIdAsync(1).Returns((Portfolio?)null);

        // Act
        var act = () => _service.DeleteAsync(1);

        // Assert
        await act.Should().ThrowAsync<PortfolioNotFoundException>();

        _portfolioRepository.DidNotReceive().Remove(Arg.Any<Portfolio>());
        await _unitOfWork.DidNotReceive().CommitAsync();
    }
}
