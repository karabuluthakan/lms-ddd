#pragma warning disable CS8603

#pragma warning disable CS8618

namespace Infrastructure.CrossCuttingConcerns.Caching.Redis;

public class RedisCacheProvider : ICacheProvider
{
    private ConnectionMultiplexer _connection;
    private IDatabase _database;
    private readonly object _lockObject;
    private readonly CacheSettings _cacheSettings;
    private readonly SemaphoreSlim connectionLock = new(1, 1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cacheSettings"></param>
    public RedisCacheProvider(IOptions<CacheSettings> cacheSettings)
    {
        _cacheSettings = Guard.Against.Null(cacheSettings.Value);
        _lockObject = new object();
        Connect();
    }

    private void Connect()
    {
        if (_database is not null)
        {
            return;
        }

        connectionLock.Wait();

        try
        {
            _connection = ConnectionMultiplexer.Connect(_cacheSettings.ConnectionString);

            _database = _connection.GetDatabase();
        }
        finally
        {
            connectionLock.Release();
        }
    }

    private async Task ConnectAsync(CancellationToken token = default)
    {
        if (_database is not null)
        {
            return;
        }

        token.ThrowIfCancellationRequested();

        await connectionLock.WaitAsync(token);

        try
        {
            _connection = await ConnectionMultiplexer.ConnectAsync(_cacheSettings.ConnectionString);

            _database = _connection.GetDatabase();
        }
        finally
        {
            connectionLock.Release();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="data"></param>
    /// <param name="expiry"></param>
    /// <returns></returns>
    public bool Set(string key, object data, TimeSpan? expiry = null)
    {
        Connect();

        lock (_lockObject)
        {
            return _database.StringSet(key, data.AsJson(), expiry, When.Always);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async ValueTask<T> GetAsync<T>(string key)
    {
        await ConnectAsync();

        var data = await _database.StringGetAsync(key);
        return data.HasValue
            ? data.ToString().FromJson<T>()
            : default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async ValueTask<bool> IsExistsAsync(string key)
    {
        await ConnectAsync();

        return await _database.KeyExistsAsync(key);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async ValueTask<bool> RemoveAsync(string key)
    {
        await ConnectAsync();

        return await _database.KeyDeleteAsync(key);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public async ValueTask RemoveByPatternAsync(string pattern)
    {
        await ConnectAsync();

        var keysToRemove = GetRemoveKeys(pattern);
        if (keysToRemove.Any())
        {
            await _database.KeyDeleteAsync(keysToRemove.ToArray());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> CheckStatus()
    {
        await ConnectAsync();
        return _connection.IsConnecting;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        _connection?.Close();
    }

    private List<RedisKey> GetRemoveKeys(string pattern)
    {
        var endPoint = _connection.GetEndPoints().FirstOrDefault();
        return _connection.GetServer(endPoint).Keys(pattern: $"*{pattern}*").ToList();
    }
}