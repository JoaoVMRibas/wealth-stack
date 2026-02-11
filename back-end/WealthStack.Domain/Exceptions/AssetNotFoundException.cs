namespace WealthStack.Domain.Exceptions;
public sealed class AssetNotFoundException : DomainException
{
    public AssetNotFoundException() : base("Asset not found.") {}
}