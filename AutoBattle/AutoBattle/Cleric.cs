using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Cleric : Types.CharacterClassSpecific
    {
        Types.CharacterClassSpecific clericClass = new Types.CharacterClassSpecific();

        public Types.CharacterClassSpecific GetClassSpecific()
        {
            clericClass = new Types.CharacterClassSpecific();
            clericClass.CharacterClass = Types.CharacterClass.Cleric;
            clericClass.HpModifier = 30f;
            clericClass.AtkModifier = 10f;
            clericClass.RangeModifier = 0;
            SetCharacterSkills();

            return clericClass;
        }

        public void SetCharacterSkills()
        {
            Types.CharacterSkills characterSkill01 = new Types.CharacterSkills();
            Types.CharacterSkills characterSkill02 = new Types.CharacterSkills();

            characterSkill01.Name = "Holy Light";
            characterSkill01.SkillValueBase = 10f;
            characterSkill01.SkillValueMultiplier = 1.75f;

            characterSkill02.Name = "Heal"; // TODO IMPLEMENT HEAL AS UNIQUE FEATURE
            characterSkill02.SkillValueBase = 10f;
            characterSkill02.SkillValueMultiplier = 1f;

            clericClass.Skills = new Types.CharacterSkills[2];
            clericClass.Skills[0] = characterSkill01;
            clericClass.Skills[1] = characterSkill02;
        }
    }
}