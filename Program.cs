using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Net;

namespace KushidaLookAnamespace
{
    public class Program
    {
        public DiscordSocketClient bot;
        public ulong CreatedId;
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public Task Log(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
        public async Task MainAsync()
        {
            bot = new DiscordSocketClient();

            bot.Log += Log;

            await bot.LoginAsync(TokenType.Bot, "NjkxNTkzNzMyNjMzNjU3MzQ0.XniO2w.W2ONNLJ6k8OOZaiqIOaDjHb_wGs");
            await bot.SetStatusAsync(UserStatus.DoNotDisturb);
            await bot.SetGameAsync("Life is fun");
            await bot.StartAsync();
            bot.UserJoined += async (e) =>
            {
                if (!e.IsBot)
                {
                    await e.AddRoleAsync(bot.GetGuild(Convert.ToUInt64("741794858339008602")).GetRole(Convert.ToUInt64("741796510232084500")) as IRole, null);
                }
            };
            bot.UserVoiceStateUpdated += async (e, a, s) =>
            {
                await _client_UserVoiceStateUpdated(e, a, s);
            };
                await Task.Delay(-1);
        }
        public async Task Funcs()
        {
            
            await Task.Delay(-1);
        }
        private async Task _client_UserVoiceStateUpdated(SocketUser arg1, SocketVoiceState arg2, SocketVoiceState arg3)
        {
            ulong GuildId = 741794858339008602;
            ulong ChannelId = 741799885082329121;
            
            if (arg3.VoiceChannel.Id == ChannelId)
            {
                IVoiceChannel voiceChannel = await bot.GetGuild(GuildId).CreateVoiceChannelAsync("Nothing");
                IVoiceChannel channel = bot.GetGuild(GuildId).GetVoiceChannel(ChannelId);
                await voiceChannel.ModifyAsync(x =>
                {
                    x.Name = arg1.Username;
                    x.Position = bot.GetGuild(741794858339008602).GetVoiceChannel(741799885082329121).Position + 1; ;
                    x.CategoryId = 741799684624089138;
                    
                });
                IUser a = arg1;
                await voiceChannel.AddPermissionOverwriteAsync(a, OverwritePermissions.AllowAll(voiceChannel));
                CreatedId = channel.Id;
                var user = bot.GetGuild(GuildId).GetUser(arg1.Id);
                await user.ModifyAsync(User =>
                {
                    User.ChannelId = voiceChannel.Id;
                });
            }
            
        }


    }
}
