using System;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    internal sealed class WorldOptionsCommands : CommandsBase<WorldOptionsCommands>
    {
        [Command(1, "name", Usage = "name")]
        void NameCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireWorldOptions();

            string name = request.GetTrail(0);
            Room.Of(this.BotBits).SetName(name);
            source.Reply("Name changed to: {0}", name);
        }

        [Command(0, "save", Usage = "")]
        private void SaveCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Operator.RequireFor(source);
            this.RequireWorldOptions();

            Room.Of(this.BotBits).Save();
            source.Reply("Saved.");
        }

        [Command(0, "clear", Usage = "")]
        private void AccessCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireWorldOptions();

            Room.Of(this.BotBits).Clear();
            source.Reply("Cleared level.");
        }

        [Command(0, "loadlevel", Usage = "")]
        void LoadlevelCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireWorldOptions();

            Chat.Of(this.BotBits).LoadLevel();
            source.Reply("Loaded level.");
        }

        [Command(0, "resetall", Usage = "")]
        void ResetCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            this.RequireWorldOptions();

            Chat.Of(this.BotBits).ResetAll();
            source.Reply("Level reset.");
        }

        [RestrictedCommand(Group.Moderator, 1, "visible", Usage = "visibility")]
        void VisibleCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireWorldOptions();

            bool visible;
            try
            {
                visible = bool.Parse(request.Args[1]);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse parameter: visibility", ex);
            }

            Room.Of(this.BotBits).SetRoomVisible(visible);
            source.Reply("World visible: " + visible);
        }

        [RestrictedCommand(Group.Moderator, 1, "hidelobby", Usage = "hidden")]
        void HideLobbyCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireWorldOptions();

            bool hide;
            try
            {
                hide = bool.Parse(request.Args[1]);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse parameter: hidden", ex);
            }

            Room.Of(this.BotBits).SetHideLobby(hide);
            source.Reply("Hidden from lobby: " + hide);
        }
    }
}
