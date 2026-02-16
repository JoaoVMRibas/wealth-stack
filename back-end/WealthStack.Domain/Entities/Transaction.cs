using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;

namespace WealthStack.Domain.Entities;
public class Transaction
{
    public int Id { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Fees { get; private set; }
    public TransactionType Type { get; private set; }
    public decimal TotalValue => (Quantity * UnitPrice) + Fees;

    public int PortfolioId { get; private set; }
    public Portfolio Portfolio { get; private set; } = null!;
    public int AssetId { get; private set; }
    public Asset Asset { get; private set; } = null!;

    private Transaction() {}

    internal Transaction(Asset asset, Portfolio portfolio, decimal quantity, decimal unitPrice, decimal fees, TransactionType type, DateTime transactionDate)
    {
        if(quantity <= 0) throw InvalidValueException.QuantityMustBeGreaterThanZero();
        if(unitPrice <= 0) throw InvalidValueException.UnitPriceMustBeGreaterThanZero();
        if(fees < 0) throw InvalidValueException.FeesCannotBeNegative();
        if(asset == null) throw new AssetNotFoundException();
        if(portfolio == null) throw new PortfolioNotFoundException();

        Asset = asset;
        Portfolio = portfolio;
        AssetId = asset.Id;
        PortfolioId = portfolio.Id;

        Quantity = quantity;
        UnitPrice = unitPrice;
        Fees = fees;
        Type = type;
        TransactionDate = transactionDate;
    }
}