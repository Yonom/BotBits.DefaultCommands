﻿using System;
using BotBits.ChatExtras;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    internal sealed class UtilityCommands : CommandsBase<UtilityCommands>
    {
        [RestrictedCommand(Group.Moderator, 0, "ping", Usage = "")]
        private void PingCommand(IInvokeSource source, ParsedRequest request)
        {
            source.Reply("Pong.");
        }

        [RestrictedCommand(Group.Moderator, 0, "access", Usage = "[key]")]
        private void AccessCommand(IInvokeSource source, ParsedRequest request)
        {
            var key = string.Empty;

            if (request.Count >= 1) key = request.GetTrail(0);

            Actions.Of(this.BotBits).Access(key);

            source.Reply("Access sent.");
        }

        [RestrictedCommand(Group.Moderator, 1, "autosay", Usage = "text")]
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

        [RestrictedCommand(Group.Moderator, 1, "say", Usage = "text")]
        private void SayCommand(IInvokeSource source, ParsedRequest request)
        {
            Chat.Of(this.BotBits).Say(request.GetTrail(0));
        }

        [RestrictedCommand(Group.Admin, 1, "sayraw", Usage = "text")]
        private void SayrawCommand(IInvokeSource source, ParsedRequest request)
        {
            if (ChatFormatsExtension.IsLoadedInto(this.BotBits))
            {
                Chat.Of(this.BotBits).Send(request.GetTrail(0));
            }
            else
            {
                Chat.Of(this.BotBits).Say(request.GetTrail(0));
            }
        }

        [RestrictedCommand(Group.Moderator, 0, "die", Usage = "")]
        private void DieCommand(IInvokeSource source, ParsedRequest request)
        {
            Actions.Of(this.BotBits).Die();
        }

        [RestrictedCommand(Group.Moderator, 1, "setsmiley", Usage = "smiley")]
        private void SetSmileyCommand(IInvokeSource source, ParsedRequest request)
        {
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

        [RestrictedCommand(Group.Moderator, 1, "setaura", Usage = "color [shape]")]
        private void AuraCommand(IInvokeSource source, ParsedRequest request)
        {
            AuraColor color;
            try
            {
                color = (AuraColor)Enum.Parse(typeof(AuraColor), request.Args[0], true);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse parameter: color", ex);
            }

            var shape = Players.Of(this.BotBits).OwnPlayer.AuraShape;
            if (request.Count >= 2)
            {
                try
                {
                    shape = (AuraShape)Enum.Parse(typeof(AuraShape), request.Args[0], true);
                }
                catch (Exception ex)
                {
                    throw new CommandException("Unable to parse parameter: shape", ex);
                }
            }

            Actions.Of(this.BotBits).ChangeAura(shape, color);
            source.Reply("Aura set to {0}/{1}.", color, shape);
        }

        [RestrictedCommand(Group.Moderator, 1, "team", "setteam", Usage = "username [color]")]
        private void TeamCommand(IInvokeSource source, ParsedRequest request)
        {
            this.RequireOwner();

            var player = request.GetPlayerIn(this.BotBits, 0);
            var color = Team.None;
            if (request.Args.Length > 1)
            {
                try
                {
                    color = (Team)Enum.Parse(typeof(Team), request.Args[1], true);
                }
                catch (Exception ex)
                {
                    throw new CommandException("Unable to parse parameter: color", ex);
                }
            }
            player.SetTeam(color);

            source.Reply("Team of {0} set to {1}.", player.Username, color);
        }

        [RestrictedCommand(Group.Admin, 0, "exit")]
        private void ExitCommand(IInvokeSource source, ParsedRequest request)
        {
            source.Reply("Exiting...");

            this.BotBits.Dispose();
        }
    }
}