using WealthStack.Domain.Entities;
using WealthStack.Domain.Enums;

namespace WealthStack.Domain.Interfaces;

public interface ITransactionDomainService
{
    Transaction Create(
        Asset asset,
        Portfolio portfolio,
        decimal quantity,
        decimal unitPrice,
        decimal fees,
        TransactionType type,
        DateTime date
    );
}