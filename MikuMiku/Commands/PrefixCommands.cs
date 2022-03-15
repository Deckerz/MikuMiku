using DataAccess.Data;
using Discord;
using Discord.Commands;

namespace mikumiku.Commands;

[RequireUserPermission(GuildPermission.Administrator, Group = "Permission")]
public class PrefixCommands : ModuleBase<SocketCommandContext>
{
    private readonly Prefix _prefix;

    public PrefixCommands(Prefix prefix)
    {
        _prefix = prefix;
    }
    
    [Command("prefix get")]
    public async Task GetPrefix()
    {
        var str = await _prefix.GetServerPrefix((long)Context.Guild.Id);
        await ReplyAsync(str ?? "!");
    }
    
    [Command("prefix set")]
    public async Task SetPrefix(string prefix)
    {
        await _prefix.SetServerPrefix((long)Context.Guild.Id, prefix);
        await ReplyAsync($"Prefix set to {prefix}");
    }
}