namespace WealthStack.Domain.Exceptions;
public class AssetNotFoundException : DomainException
{
    public AssetNotFoundException() : base("Asset not found.") {}
}