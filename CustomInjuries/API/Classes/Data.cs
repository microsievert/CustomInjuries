using System.Collections.Generic;

using Exiled.API.Features;

namespace CustomInjuries.API.Classes
{
    public class Data
    {
        public List<Player> ImmunityPlayers { get; } = new ();
        
        public void SwitchImmunity(Player player)
        {
            if (ImmunityPlayers.Contains(player))
                ImmunityPlayers.Remove(player);
            else
                ImmunityPlayers.Add(player);
        }
    }
}
