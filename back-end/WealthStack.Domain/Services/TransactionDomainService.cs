using WealthStack.Domain.Entities;
using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;

namespace WealthStack.Domain.Services;

public class TransactionDomainService
{
    public Transaction Create
    (
        Asset asset,
        Portfolio portfolio,
        decimal quantity,
        decimal unitPrice,
        decimal fees,
        TransactionType type,
        DateTime date
    )
    {
        if(asset == null) throw new AssetNotFoundException();
        if(portfolio == null) throw new PortfolioNotFoundException();

        if(asset.Type != AssetType.Crypto && quantity % 1 != 0)
            throw new FractionalQuantityNotAllowedException(asset.Type,quantity);

        return new Transaction(asset.Id,portfolio.Id,quantity,unitPrice,fees,type,date);
    }
}