using System;
using System.Linq;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    internal abstract class CommandsBase<T> : Package<T> where T : Package<T>, new()
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
            if (source.GetGroup() <= this.GetGroup(username))
                throw new AccessDeniedCommandException("The target player is higher or equally ranked.");
        }

        protected void RequireEqualRank(IInvokeSource source, string username)
        {
            if (source.GetGroup() < this.GetGroup(username))
                throw new AccessDeniedCommandException("The target player is higher ranked.");
        }
        
        private Group GetGroup(string username)
        {
            var player = Players.Of(this.BotBits).FromUsername(username).FirstOrDefault();
            if (player == null)
                throw new CommandException("Unknown user.");
            return player.GetGroup();
        }

        protected void RequireOwner()
        {
            if (Actions.Of(this.BotBits).AccessRight < AccessRight.Owner)
                throw new CommandException("Bot must be world owner to run that command.");
        }

        protected void RequireWorldOptions()
        {
            if (Actions.Of(this.BotBits).AccessRight < AccessRight.WorldOptions)
                throw new CommandException("Bot must be world owner or crew member to run that command.");
        }

        protected void RequireEdit()
        {
            if (!Actions.Of(this.BotBits).CanEdit)
                throw new CommandException("Bot must have edit to run that command.");
        }
    }
}
