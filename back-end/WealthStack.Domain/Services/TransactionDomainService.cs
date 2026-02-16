using WealthStack.Domain.Entities;
using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Domain.Services;

public class TransactionDomainService : ITransactionDomainService
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
        if(asset.Type != AssetType.Crypto && quantity % 1 != 0)
            throw new FractionalQuantityNotAllowedException(asset.Type,quantity);

        return new Transaction(asset,portfolio,quantity,unitPrice,fees,type,date);
    }
}