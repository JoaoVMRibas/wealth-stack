namespace WealthStack.Application.Exceptions;
public class InvalidAssetTypeException : ApplicationException
{
    public InvalidAssetTypeException(string assetType) : base($"Invalid Asset Type: {assetType}") {}
}