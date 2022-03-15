using Discord.Commands;

namespace mikumiku.Commands;

public class Ping : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    [Alias("pong", "hello")]
    public Task PingAsync()
        => ReplyAsync("pong!");
}