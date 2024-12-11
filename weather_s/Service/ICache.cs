namespace Service
{
    public interface ICache
    {
         Task<string> GetJsonResponseAsync(string city);
    }
}

