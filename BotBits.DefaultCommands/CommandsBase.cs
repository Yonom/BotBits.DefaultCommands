using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    internal class CommandsBase<T> : Package<T> where T : Package<T>, new()
    {
        protected CommandsBase()
        {
            this.InitializeFinish += this.DefaultCommandsManager_InitializeFinish;
        }

        private void DefaultCommandsManager_InitializeFinish(object sender, EventArgs e)
        {
            CommandLoader.Of(this.BotBits).Load(this);
        }

        protected void RequireLowerRank(IInvokeSource source, string username)
        {
            if (source.GetGroup() <= this.GetRank(username))
                throw new AccessDeniedCommandException("The target player is higher or equally ranked.");
        }

        protected void RequireEqualRank(IInvokeSource source, string username)
        {
            if (source.GetGroup() < this.GetRank(username))
                throw new AccessDeniedCommandException("The target player is higher ranked.");
        }

        private Group GetRank(string username)
        {
            var player = Players.Of(this.BotBits).FromUsername(username).FirstOrDefault();
            if (player == null)
                throw new CommandException("Unknown user.");
            return player.GetGroup();
        }

        protected void RequireOwner()
        {
            if (Room.Of(this.BotBits).AccessRight < AccessRight.Owner)
                throw new CommandException("Bot must be world owner to run that command.");
        }
    }
}
