using System;
using BotBits.ChatExtras;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    sealed class UserCommands : CommandsBase<UserCommands>
    {
        [Command(1, "kick", Usages = new[] { "username [reason]" })]
        void KickCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Trusted.RequireFor(source);
            this.RequireOwner();

            var name = request.GetUsernameIn(this.BotBits, 0);
            this.RequireEqualRank(source, name);

            var reason = request.GetTrail(1);
            if (String.IsNullOrEmpty(reason))
                reason = "Tsk tsk tsk";
            reason += " ~" + source.Name;
            Chat.Of(this.BotBits).Kick(name, reason);
        }

        [Command(1, "kill", Usage = "username")]
        void KillCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();

            var names = request.GetUsernamesIn(this.BotBits, 0);
            foreach (var name in names)
                Chat.Of(this.BotBits).Kill(name);
            source.Reply("Killed {0}.", String.Join(", ", names));
        }

        [Command(1, "giveedit", "ge", Usage = "username")]
        void GiveEditCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();

            var name = request.GetUsernameIn(this.BotBits, 0);

            Chat.Of(this.BotBits).GiveEdit(name);
            source.Reply("Gave edit to {0}.", name);
        }

        [Command(1, "removeedit", "re", Usage = "username")]
        void RemoveEditCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();

            var name = request.GetUsernameIn(this.BotBits, 0);

            Chat.Of(this.BotBits).RemoveEdit(name);
            source.Reply("Removed edit from {0}.", name);
        }

        [Command(1, "givegod", Usage = "username")]
        void GiveGodCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();

            var name = request.GetUsernameIn(this.BotBits, 0);

            Chat.Of(this.BotBits).GiveGod(name);
            source.Reply("Gave god to {0}.", name);
        }

        [Command(1, "removegod", Usage = "username")]
        void RemoveGodCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();

            var name = request.GetUsernameIn(this.BotBits, 0);

            Chat.Of(this.BotBits).RemoveGod(name);
            source.Reply("Removed god from {0}.", name);
        }

        [Command(1, "tp", "teleport", Usages = new [] { "username [target]", "username x y"})]
        void TeleportCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireOwner();

            var username = request.GetUsernameIn(this.BotBits, 0);

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
                    Chat.Of(this.BotBits).Teleport(username, target.BlockX, target.BlockY);
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
