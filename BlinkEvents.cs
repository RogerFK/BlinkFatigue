using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkFatigue
{
    public class BlinkEvents
    {
        public void OnWaitingForPlayers()
        {
            if (!BlinkStuff.alreadyRan)
            {
                BlinkStuff.InitializeAndFetchAllValues(PlayerManager.localPlayer.GetComponent<Scp173PlayerScript>());
            }
        }
    }
}
