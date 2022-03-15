namespace DataAccess.Data;

public class Prefix
{
    private readonly PrefixDatabase _db;

    public Prefix(PrefixDatabase db)
    {
        _db = db;
    }

    public async Task<string?> GetServerPrefix(long guildId)
    {
        const string sql = @"select prefix from prefix where guild_id = @GuildId";
        return await _db.LoadDataSingle<string, dynamic>(sql, new { GuildId = guildId });
    }
    
    public async Task<bool> SetServerPrefix(long guildId, string prefix)
    {
        const string sql = @"INSERT OR REPLACE INTO prefix (guild_id, prefix) values (@GuildId, @Prefix)";
        return await _db.SaveData(sql, new { GuildId = guildId, Prefix = prefix });
    }
}