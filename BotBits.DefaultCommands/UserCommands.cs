using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotBits.ChatExtras;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    class UserCommands : CommandsBase<UtilityCommands>
    {
        [Command(1, "kick", Usages = new[] { "username [reason]" })]
        void KickCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Trusted.RequireFor(source);
            var name = request.GetUsernameIn(this.BotBits, 0);
            this.RequireEqualRank(source, name);

            var reason = request.GetTrail(1);
            Chat.Of(this.BotBits).Kick(name, reason);
        }

        [Command(1, "kill", Usage = "username")]
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

        [Command(1, "tp", "teleport", Usages = new [] { "username [target]", "username x y"})]
        void TeleportCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();
            var username = request.GetUsernameIn(this.BotBits, 0);
            this.RequireEqualRank(source, username);

            if (request.Count >= 3)
            {
                int x = request.GetInt(1);
                int y = request.GetInt(2);

                Chat.Of(this.BotBits).Teleport(username, x, y);
            }
            else if (request.Count == 2)
            {
                try
                {
                    var target = request.GetPlayerIn(this.BotBits, 1);
                    Chat.Of(this.BotBits).Teleport(target.Username, target.BlockX, target.BlockY);
                }
                catch (CommandException ex)
                {
                    throw new CommandException(ex.Message + " Parameter: target", ex);
                }
            }
            else
            {
                var playerSource = source as PlayerInvokeSource;
                if (playerSource != null)
                {
                    int x = playerSource.Player.BlockX;
                    int y = playerSource.Player.BlockX;

                    Chat.Of(this.BotBits).Teleport(username, x, y);
                }
                else
                {
                    Chat.Of(this.BotBits).Teleport(username);
                }
            }

            source.Reply("Teleported {0}.", PlayerUtils.GetChatName(username));
        }
    }
}
