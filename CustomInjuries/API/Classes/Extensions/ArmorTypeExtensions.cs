using Exiled.API.Features;

using CustomInjuries.API.Enums;

using InventorySystem.Items.Armor;

namespace CustomInjuries.API.Classes.Extensions
{
    public static class ArmorTypeExtensions
    {
        public static ArmorType GetHeaviestArmor(Player player)
        {
            if (player.Inventory.TryGetBodyArmor(out BodyArmor armor))
            {
                switch (armor.ItemTypeId)
                {
                    case ItemType.ArmorHeavy:
                        return ArmorType.HeavyArmor;
                    case ItemType.ArmorCombat:
                        return ArmorType.CombatArmor;
                    case ItemType.ArmorLight:
                        return ArmorType.LightArmor;
                    default:
                        return ArmorType.NoArmor;
                }
            }

            return ArmorType.NoArmor;
        }
    }
}