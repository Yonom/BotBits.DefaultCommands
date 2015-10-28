using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BotBits.ChatExtras;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    internal sealed class OwnerCommands : CommandsBase<OwnerCommands>
    {
        [Command(0, "respawnall", Usage = "")]
        void RespawnAllCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();

            Chat.Of(this.BotBits).RespawnAll();
            source.Reply("Respawned all.");
        }

        [Command(0, "killall", "killemall", Usage = "")]
        void KillAllCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();

            Chat.Of(this.BotBits).KillAll();
            source.Reply("Killed everyone.");
        }
    }
}
