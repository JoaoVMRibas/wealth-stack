using WealthStack.Domain.Enums;

namespace WealthStack.Domain.Exceptions;

public sealed class FractionalQuantityNotAllowedException : DomainException
{
    public FractionalQuantityNotAllowedException(
        AssetType assetType,
        decimal quantity
    )
        : base($"Assets of type '{assetType}' do not support fractional quantities. Quantity: {quantity}.")
    {
    }
}