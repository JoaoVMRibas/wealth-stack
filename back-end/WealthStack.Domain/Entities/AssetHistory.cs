using WealthStack.Domain.Exceptions;

namespace WealthStack.Domain.Entities;

public class AssetHistory
{
    public int Id { get; private set; }
    public int AssetId { get; private set; }
    public Asset Asset { get; private set; } = null!;
    public decimal Price { get; private set; }
    public DateTime ReferenceDate { get; private set; }

    private AssetHistory() {}

    public AssetHistory(int assetId, decimal price, DateTime referenceDate)
    {
        if(price <= 0) throw InvalidValueException.PriceMustBeGreaterThanZero();

        if(referenceDate > DateTime.UtcNow.AddMinutes(5)) throw new FutureDateNotAllowedException();

        AssetId = assetId;
        Price = price;
        ReferenceDate = referenceDate;
    }
}