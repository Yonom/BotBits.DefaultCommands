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
    internal class OwnerCommands : CommandsBase<UtilityCommands>
    {
        [Command(1, "kick", Usages = new[] { "username", "username reason" })]
        void KickCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Trusted.RequireFor(source);
            var name = request.GetUsernameIn(this.BotBits, 0);
            this.RequireEqualRank(source, name);

            var reason = request.GetTrail(1);
            Chat.Of(this.BotBits).Kick(name, reason);
        }

        [Command(0, "clear", Usage = "")]
        private void AccessCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();
            Room.Of(this.BotBits).Clear();
            source.Reply("Cleared level.");
        }

        [Command(0, "loadlevel", Usage = "")]
        void LoadlevelCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Trusted.RequireFor(source);
            Chat.Of(this.BotBits).LoadLevel();
        }

        [Command(1, "kill", Usage = "username" )]
        void KillCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();
            var name = request.GetUsernameIn(this.BotBits, 0);
            Chat.Of(this.BotBits).Kill(name);
        }

        [Command(1, "giveedit", "ge", Usage = "username")]
        void GiveEditCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();
            var name = request.GetUsernameIn(this.BotBits, 0);
            Chat.Of(this.BotBits).GiveEdit(name);
        }

        [Command(1, "removeedit", "re", Usage = "username")]
        void RemoveEditCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();
            var name = request.GetUsernameIn(this.BotBits, 0);
            Chat.Of(this.BotBits).RemoveEdit(name);
        }
    }
}
