using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    public class UtilityCommands : Package<UtilityCommands>
    {
        public UtilityCommands()
        {
            this.InitializeFinish += DefaultCommandsManager_InitializeFinish;
        }

        private void DefaultCommandsManager_InitializeFinish(object sender, EventArgs e)
        {
            CommandLoader.Of(this.BotBits).Load(this);
        }

        [Command(0, "access", Usage = "[key]")]
        void AccessCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);

            string key = String.Empty;

            if (request.Count >= 1)
                key = request.GetTrail(0);

            Actions.Of(this.BotBits).Access(key);

            source.Reply("Access sent.");
        }

        [Command(1, "autosay", Usage = "text")]
        private void AutosayCommand(IInvokeSource source, ParsedRequest request)
        {
            AutoText text;
            try
            {
                text = (AutoText)Enum.Parse(typeof(AutoText), request.Args[0], true);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse parameter: text", ex);
            }

            Actions.Of(this.BotBits).AutoSay(text);
        }
    }
}
