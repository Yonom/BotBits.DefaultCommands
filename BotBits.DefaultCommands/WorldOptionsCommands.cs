using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
