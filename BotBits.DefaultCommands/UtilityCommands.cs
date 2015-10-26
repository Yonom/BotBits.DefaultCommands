using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BotBits.ChatExtras;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    internal class UtilityCommands : CommandsBase<UtilityCommands>
    {
        [Command(0, "ping", Usage = "")]
        void PingCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);
            source.Reply("Pong.");
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
            Group.Moderator.RequireFor(source);

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

        [Command(1, "say", Usage = "text")]
        private void SayCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);

            Chat.Of(this.BotBits).Say(request.GetTrail(0));
        }

        [Command(1, "sayraw", Usage = "text")]
        private void SayrawCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Admin.RequireFor(source);

            if (ChatFormatsExtension.IsLoadedInto(this.BotBits))
            {
                Chat.Of(this.BotBits).Send(request.GetTrail(0));
            }
            else
            {
                Chat.Of(this.BotBits).Say(request.GetTrail(0));
            }
        }

        [Command(0, "die", Usage = "")]
        private void DieCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);

            Actions.Of(this.BotBits).Die();
        }

        [Command(1, "setsmiley", Usage = "smiley")]
        private void SetSmileyCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);

            Smiley smiley;
            try
            {
                smiley = (Smiley)Enum.Parse(typeof(Smiley), request.Args[0], true);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse parameter: smiley", ex);
            }

            Actions.Of(this.BotBits).ChangeSmiley(smiley);
            source.Reply("Smiley set to {0}.", smiley);
        }

        [Command(1, "setaura", Usage = "aura")]
        void AuraCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Moderator.RequireFor(source);

            Aura aura;
            try
            {
                aura = (Aura)Enum.Parse(typeof(Aura), request.Args[0], true);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse parameter: aura", ex);
            }

            Actions.Of(this.BotBits).ChangeAura(aura);
            source.Reply("Aura set to {0}.", aura);
        }

        [Command(0, "exit")]
        private void ExitCommand(IInvokeSource source, ParsedRequest request)
        {
            Group.Admin.RequireFor(source);
            source.Reply("Exiting...");

            this.BotBits.Dispose();
        }
    }
}
