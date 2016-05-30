using System;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    internal sealed class EditCommands : CommandsBase<EditCommands>
    {
        [RestrictedCommand(Group.Moderator, 1, "godmode", Usage = "enable")]
        void GodModeCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireEdit();

            bool enabled;

            if (request.Count >= 1)
            {
                try
                {
                    enabled = Boolean.Parse(request.Args[0]);
                }
                catch (Exception ex)
                {
                    throw new CommandException("Unable to parse parameter: enabled", ex);
                }
            }
            else
            {
                enabled = !Players.Of(this.BotBits).OwnPlayer.GodMode;
            }

            Actions.Of(this.BotBits).GodMode(enabled);

            source.Reply("God mode was set to {0}.", enabled);
        }

        [RestrictedCommand(Group.Moderator, 0, "modmode", Usage = "")]
        void ModModeCommand(IInvokeSource source, ParsedRequest request)
        {
            Actions.Of(this.BotBits).ToggleModMode();
            source.Reply("Mod mode was toggled.");
        }

        [RestrictedCommand(Group.Moderator, 0, "adminmode", Usage = "")]
        void AdminModeCommand(IInvokeSource source, ParsedRequest request)
        {
            Actions.Of(this.BotBits).ToggleAdminMode();
            source.Reply("Admin mode was toggled.");
        }
    }
}
