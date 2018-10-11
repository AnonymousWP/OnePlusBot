﻿using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using System.Threading.Tasks;

namespace OnePlusBot.Modules
{
    public class PurgeModule : ModuleBase<SocketCommandContext>
    {

        [Command("purge", RunMode = RunMode.Async)]
        [Summary("Deletes specified amount of messages.")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task PurgeAsync([Remainder] double delmsg)
        {
           
            if (delmsg > 100)
            {
                var EmoteFalse = new Emoji("⚠");
                await Context.Message.RemoveAllReactionsAsync();
                await Context.Message.AddReactionAsync(EmoteFalse);
                await ReplyAsync("Use a number below 100.");
                return;
            }

            try
            {
                int delmsgInt = (int)delmsg;

                // Declare the beginning message, and the emote to react with, react and then wait a second.
                ulong oldmessage = Context.Message.Id;
                var Emote = new Emoji(":success:499567039451758603");
                await Context.Message.AddReactionAsync(Emote);
                await Task.Delay(1000);

                // Download all messages that the user asks for to delete, 
                var messages = await Context.Channel.GetMessagesAsync(oldmessage, Direction.Before, delmsgInt).FlattenAsync();
                await (Context.Channel as ITextChannel).DeleteMessagesAsync(messages);
                await Task.Delay(2000);

                messages = await Context.Channel.GetMessagesAsync(1).FlattenAsync();
                await (Context.Channel as ITextChannel).DeleteMessagesAsync(messages);
            }
            catch(Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }
    }
}

