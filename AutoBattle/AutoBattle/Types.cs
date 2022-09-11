using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Types
    {

        public struct CharacterClassSpecific
        {
            CharacterClass CharacterClass;
            float hpModifier;
            float ClassDamage;
            CharacterSkills[] skills;

        }

        public struct GridBox
        {
            public int xIndex;
            public int yIndex;
            public bool occupied;
            public int Index;

            public GridBox(int x, int y, bool occupied, int index)
            {
                xIndex = x;
                yIndex = y;
                this.occupied = occupied;
                this.Index = index;
            }

        }

        public struct CharacterSkills
        {
            string Name;
            float damage;
            float damageMultiplier;
        }

        public enum CharacterClass : uint
        {
            Paladin = 1,
            Warrior = 2,
            Cleric = 3,
            Archer = 4
        }

    }
}
