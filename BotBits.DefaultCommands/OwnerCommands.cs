using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    public class OwnerCommands : Package<UtilityCommands>
    {
        public OwnerCommands()
        {
            this.InitializeFinish += DefaultCommandsManager_InitializeFinish;
        }

        private void DefaultCommandsManager_InitializeFinish(object sender, EventArgs e)
        {
            CommandLoader.Of(this.BotBits).Load(this);
        }

        [Command(0, "clear", Usage = "CLEAR")]
        private void AccessCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireOwner();

            if (request.Count == 0 || request.Args[0] != "CLEAR")
                throw new CommandException("To use clear, type !clear CLEAR");

            Room.Of(this.BotBits).Clear();
            source.Reply("Cleared level.");
        }

        private void RequireOwner()
        {
            if (Room.Of(this.BotBits).AccessRight < AccessRight.Owner)
                throw new CommandException("Bot must be world owner to run that command.");
        }
    }
}
