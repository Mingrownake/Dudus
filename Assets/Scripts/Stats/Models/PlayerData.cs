using System;

namespace Stats.Models
{
    [Serializable]
    public class PlayerData
    {
        public int money;
        public int health;
        public bool isArmed;

        public void SetArmed(bool isArmed)
        {
            this.isArmed = isArmed;
        }
    }
}