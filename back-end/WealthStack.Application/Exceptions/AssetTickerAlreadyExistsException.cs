namespace WealthStack.Application.Exceptions;

public class AssetTickerAlreadyExistsException : ApplicationException
{
    public AssetTickerAlreadyExistsException(string ticker) : base($"Asset with ticker {ticker} already exists.") {}
}