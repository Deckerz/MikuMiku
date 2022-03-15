using System.Reflection;
using DataAccess.Data;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace mikumiku.Services;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private readonly IServiceProvider _services;

    public CommandHandler(IServiceProvider services, CommandService commands, DiscordSocketClient client)
    {
        _commands = commands;
        _services = services;
        _client = client;
    }

    public async Task InitializeAsync()
    {
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        _client.MessageReceived += HandleCommandAsync;
        _commands.CommandExecuted += CommandExecutedAsync;
    }

    private async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext ctx, IResult result)
    {
        if (!command.IsSpecified)
            return;

        if (result.IsSuccess)
            return;

        await ctx.Channel.SendMessageAsync($"error: {result}");
    }

    public async Task HandleCommandAsync(SocketMessage msg)
    {
        var prefixDb = _services.GetService<Prefix>();
        if (prefixDb == null) return;

        if (msg is not SocketUserMessage { Source: MessageSource.User } message) return;
        if (message.Channel is not SocketGuildChannel channel) return;

        var prefix = await prefixDb.GetServerPrefix((long)channel.Guild.Id) ?? "!";

        var argPos = 0;
        if (!message.HasStringPrefix(prefix, ref argPos))
            return;

        var context = new SocketCommandContext(_client, message);
        await _commands.ExecuteAsync(context, argPos, _services);
    }
}