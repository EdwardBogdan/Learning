using System;

namespace MyProject.Data
{
    [Serializable]
    public class PlayerData
    {
        public int HP = 25;
        public int MaxHP = 30;
        public bool IsArmed()
        {
            int count = GameSession.CurrentSession.Inventory.GetCount("Sword");
            return count > 0;
        }
        public PlayerData Clone()
        {
            return new PlayerData(this);
        }

        public PlayerData()
        {
            
        }

        public PlayerData(PlayerData data)
        {
            HP = data.HP;
            MaxHP = data.MaxHP;
        }
    }
}
