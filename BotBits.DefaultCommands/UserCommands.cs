using BotBits.ChatExtras;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    internal sealed class UserCommands : CommandsBase<UserCommands>
    {
        [RestrictedCommand(Group.Trusted, 1, "kick", Usages = new[] { "username [reason]" })]
        private void KickCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireOwner();

            var name = request.GetUsernameIn(this.BotBits, 0);
            this.RequireEqualRank(source, name);

            var reason = request.GetTrail(1);
            if (string.IsNullOrEmpty(reason)) reason = "Tsk tsk tsk";
            reason += " ~" + source.Name;
            Chat.Of(this.BotBits).Kick(name, reason);
        }

        [RestrictedCommand(Group.Moderator, 1, "kill", Usage = "username")]
        private void KillCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireOwner();

            var names = request.GetUsernamesIn(this.BotBits, 0);
            foreach (var name in names) Chat.Of(this.BotBits).Kill(name);
            source.Reply("Killed {0}.", string.Join(", ", names));
        }

        [RestrictedCommand(Group.Moderator, 1, "giveedit", "ge", Usage = "username")]
        private void GiveEditCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireOwner();

            var name = request.GetUsernameIn(this.BotBits, 0);

            Chat.Of(this.BotBits).GiveEdit(name);
            source.Reply("Gave edit to {0}.", name);
        }

        [RestrictedCommand(Group.Moderator, 1, "removeedit", "re", Usage = "username")]
        private void RemoveEditCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireOwner();

            var name = request.GetUsernameIn(this.BotBits, 0);

            Chat.Of(this.BotBits).RemoveEdit(name);
            source.Reply("Removed edit from {0}.", name);
        }

        [RestrictedCommand(Group.Moderator, 1, "givegod", Usage = "username")]
        private void GiveGodCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireOwner();

            var name = request.GetUsernameIn(this.BotBits, 0);

            Chat.Of(this.BotBits).GiveGod(name);
            source.Reply("Gave god to {0}.", name);
        }

        [RestrictedCommand(Group.Moderator, 1, "removegod", Usage = "username")]
        private void RemoveGodCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireOwner();

            var name = request.GetUsernameIn(this.BotBits, 0);

            Chat.Of(this.BotBits).RemoveGod(name);
            source.Reply("Removed god from {0}.", name);
        }

        [RestrictedCommand(Group.Moderator, 1, "tp", "teleport", Usages = new[] { "username [target]", "username x y" })]
        private void TeleportCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireOwner();

            var username = request.GetUsernameIn(this.BotBits, 0);

            if (request.Count >= 3)
            {
                var x = request.GetInt(1);
                var y = request.GetInt(2);

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
                    var x = playerSource.Player.BlockX;
                    var y = playerSource.Player.BlockX;

                    Chat.Of(this.BotBits).Teleport(username, x, y);
                }
                else
                {
                    Chat.Of(this.BotBits).Teleport(username);
                }
            }

            source.Reply("Teleported {0}.", Player.GetChatName(username));
        }
    }
}