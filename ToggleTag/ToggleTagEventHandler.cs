
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using RemoteAdmin;
using MEC;
using CommandSystem;

namespace ToggleTag
{
    public class ToggleTagEventHandler
    {


        public ToggleTag Plugin;

        public enum PropertyType
        {
            Tag,
            Overwatch
        };

        public ToggleTagEventHandler(ToggleTag Plugin) => this.Plugin = Plugin;



        public void RunWhenPlayerJoins(JoinedEventArgs JoinEv)
        {
            Timing.CallDelayed(0.1f, () => ShowOrHideFeatures(JoinEv.Player));
        }

        // im pretty sure the function below checks at the start of the round for if the player had their tag hidden or not and re-applies it

        public void ShowOrHideFeatures(Player Ply)
        {
            if (Plugin.TagManager.TagPlayers.ContainsKey(Ply.UserId))
            {
                switch (Plugin.TagManager.TagPlayers[Ply.UserId])
                {
                    case 0:
                        Ply.BadgeHidden = false;
                        break;
                    case 1:
                        Ply.BadgeHidden = true;
                        break;
                    default:
                        Ply.BadgeHidden = false;
                        break;
                }
            }
            if (Plugin.TagManager.OwPlayers.ContainsKey(Ply.UserId))
            {
                switch (Plugin.TagManager.OwPlayers[Ply.UserId])
                {
                    case 0:
                        Ply.IsOverwatchEnabled = false;
                        break;
                    case 1:
                        Ply.IsOverwatchEnabled = true;
                        break;
                    default:
                        Ply.IsOverwatchEnabled = false;
                        break;
                }
            }
        }
    }
}
