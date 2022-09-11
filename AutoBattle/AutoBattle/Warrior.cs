using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Warrior : Types.CharacterClassSpecific
    {
        Types.CharacterClassSpecific warriorClass = new Types.CharacterClassSpecific();

        public Types.CharacterClassSpecific GetClassSpecific()
        {
            warriorClass = new Types.CharacterClassSpecific();
            warriorClass.CharacterClass = Types.CharacterClass.Warrior;
            warriorClass.HpModifier = 5f;
            warriorClass.AtkModifier = 20f;
            warriorClass.RangeModifier = 0;
            SetCharacterSkills();

            return warriorClass;
        }

        public void SetCharacterSkills()
        {
            Types.CharacterSkills characterSkill01 = new Types.CharacterSkills();
            Types.CharacterSkills characterSkill02 = new Types.CharacterSkills();

            characterSkill01.Name = "Berserker Slash";
            characterSkill01.Damage = 30f; 
            characterSkill01.DamageMultiplier = 1.5f;

            characterSkill02.Name = "Roar"; // TODO IMPLEMENT FEAR (STUN)
            characterSkill02.Damage = 10f;
            characterSkill02.DamageMultiplier = 1f;

            warriorClass.Skills = new Types.CharacterSkills[2];
            warriorClass.Skills[0] = characterSkill01;
            warriorClass.Skills[1] = characterSkill02;
        }
    }
}