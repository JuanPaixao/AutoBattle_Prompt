using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Paladin : Types.CharacterClassSpecific
    {
        Types.CharacterClassSpecific paladinClass = new Types.CharacterClassSpecific();

        public Types.CharacterClassSpecific GetClassSpecific()
        {
            paladinClass = new Types.CharacterClassSpecific();
            paladinClass.CharacterClass = Types.CharacterClass.Paladin;
            paladinClass.HpModifier = 10f;
            paladinClass.AtkModifier = 15f;
            paladinClass.RangeModifier = 0;
            SetCharacterSkills();

            return paladinClass;
        }

        public void SetCharacterSkills()
        {
            Types.CharacterSkills characterSkill01 = new Types.CharacterSkills();
            Types.CharacterSkills characterSkill02 = new Types.CharacterSkills();

            characterSkill01.Name = "Holy Sword";
            characterSkill01.SkillValueBase = 20f;
            characterSkill01.SkillValueMultiplier = 1.5f;

            characterSkill02.Name = "Cross Slash"; // TODO IMPLEMENT DOUBLE ATTACK CHANCE
            characterSkill02.SkillValueBase = 15f;
            characterSkill02.SkillValueMultiplier = 1f;

            paladinClass.Skills = new Types.CharacterSkills[2];
            paladinClass.Skills[0] = characterSkill01;
            paladinClass.Skills[1] = characterSkill02;
        }
    }
}