using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    internal sealed class OwnerCommands : CommandsBase<OwnerCommands>
    {
        [RestrictedCommand(Group.Moderator, 0, "respawnall", Usage = "")]
        private void RespawnAllCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireOwner();

            Chat.Of(this.BotBits).RespawnAll();
            source.Reply("Respawned all.");
        }

        [RestrictedCommand(Group.Moderator, 0, "killall", "killemall", Usage = "")]
        private void KillAllCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireOwner();

            Chat.Of(this.BotBits).KillAll();
            source.Reply("Killed everyone.");
        }
    }
}