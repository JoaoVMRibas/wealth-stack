using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Policies;

namespace WealthStack.Domain.Entities;

public class AssetHistory
{
    public int Id { get; private set; }
    public int AssetId { get; private set; }
    public Asset Asset { get; private set; } = null!;
    public decimal Price { get; private set; }
    public DateTime ReferenceDate { get; private set; }

    private AssetHistory() {}

    public AssetHistory(Asset asset, decimal price, DateTime referenceDate)
    {
        if(price <= 0) throw InvalidValueException.PriceMustBeGreaterThanZero();
        if(IsReferenceDateInFuture(referenceDate,DateTime.UtcNow)) throw new FutureDateNotAllowedException();
        if(asset == null) throw new AssetNotFoundException();

        Asset = asset;
        AssetId = asset.Id;

        Price = price;
        ReferenceDate = referenceDate;
    }

    private bool IsReferenceDateInFuture(DateTime referenceDate, DateTime now)
    {
        return referenceDate > now.AddMinutes(AssetHistoryPolicy.AllowedFutureToleranceMinutes);
    }
}