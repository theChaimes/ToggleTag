using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace ToggleTag
{

    public class ToggleTag : Plugin<Config>
    {
        public UserManager TagManager;
        public ToggleTagEventHandler Handler;
        public override string Name => "ToggleTag";
        public override string Author => "KoukoCocoa (Updated by Chaimes)";

        public override void OnEnabled()
        {
            TagManager = new UserManager();
            Handler = new ToggleTagEventHandler(this);
            Exiled.Events.Handlers.Player.Joined += Handler.RunWhenPlayerJoins;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Joined -= Handler.RunWhenPlayerJoins;
            Handler = null;
            TagManager = null;
        }

        public override void OnReloaded() { }
    }



    // SHOW TAG COMMAND
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class ShowTagCommand : ICommand
    {
        string ICommand.Command => "showtag";

        public string[] Aliases { get; } = { "show" };

        public string Description => "Shows your tag.";

        public ToggleTag Plugin;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            ((Player)sender).BadgeHidden = false;
            Plugin.TagManager.AddPlayer(((Player)sender).UserId, 0, ToggleTagEventHandler.PropertyType.Tag);
            response = "Your tag (if you have one) will permanently be shown on this server now";
            return true;
        }
    }

    // HIDE TAG COMMAND
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class HideTagCommand : ICommand
    {
        string ICommand.Command => "hidetag";

        public string[] Aliases { get; } = { "hide" };

        public string Description => "Hides your tag.";

        public ToggleTag Plugin;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            ((Player)sender).BadgeHidden = true;
            Plugin.TagManager.AddPlayer(((Player)sender).UserId, 1, ToggleTagEventHandler.PropertyType.Tag);
            response = "Your tag (if you have one) will permanently be hidden on this server now";
            return true;
        }
    }
}
