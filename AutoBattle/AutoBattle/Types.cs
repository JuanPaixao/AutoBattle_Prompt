using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Types
    {
        public class CharacterClassSpecific
        {
            private CharacterClass _characterClass;
            private float _hpModifier;
            private float _atkModifier;
            private CharacterSkills[] _skills;

            public CharacterClassSpecific GetClassBundle(CharacterClass characterClass)
            {
                if (characterClass == CharacterClass.Paladin)
                {
                    Paladin paladin = new Paladin();
                    var loadedValues = paladin.GetClassSpecific();
                    return loadedValues;
                }

                if (characterClass == CharacterClass.Warrior)
                {
                    Warrior warrior = new Warrior();
                    var loadedValues = warrior.GetClassSpecific();
                    return loadedValues;
                }

                if (characterClass == CharacterClass.Cleric)
                {
                    Cleric cleric = new Cleric();
                    var loadedValues = cleric.GetClassSpecific();
                    return loadedValues;
                }

                if (characterClass == CharacterClass.Archer)
                {
                    Archer archer = new Archer();
                    var loadedValues = archer.GetClassSpecific();
                    return loadedValues;
                }

                return null;
            }

            #region Get/Set Area

            public CharacterClass CharacterClass
            {
                get => _characterClass;
                set => _characterClass = value;
            }

            public float HpModifier
            {
                get => _hpModifier;
                set => _hpModifier = value;
            }

            public float AtkModifier
            {
                get => _atkModifier;
                set => _atkModifier = value;
            }
            
            public CharacterSkills[] Skills
            {
                get => _skills;
                set => _skills = value;
            }
        }

        #endregion

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
            string _name;
            float _skillValueBase;
            float _skillValueMultiplier;

            SkillEffects _skillEffects;

            #region Get/Set Area

            public string Name
            {
                get => _name;
                set => _name = value;
            }

            public float SkillValueBase
            {
                get => _skillValueBase;
                set => _skillValueBase = value;
            }

            public float SkillValueMultiplier
            {
                get => _skillValueMultiplier;
                set => _skillValueMultiplier = value;
            }

            public SkillEffects SkillEffects
            {
                get => _skillEffects;
                set => _skillEffects = value;
            }

            #endregion
        }

        public enum CharacterClass : uint
        {
            Paladin = 1,
            Warrior = 2,
            Cleric = 3,
            Archer = 4
        }

        public enum SkillEffects : uint
        {
            None = 0,
            Heal = 1,
            Stun = 2,
            DoubleAttack = 3,
            Knockback = 4
        }
    }
}