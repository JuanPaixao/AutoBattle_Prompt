using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Archer : Types.CharacterClassSpecific
    {
        Types.CharacterClassSpecific archerClass = new Types.CharacterClassSpecific();

        public Types.CharacterClassSpecific GetClassSpecific()
        {
            archerClass = new Types.CharacterClassSpecific();
            archerClass.CharacterClass = Types.CharacterClass.Archer;
            archerClass.HpModifier = 30f;
            archerClass.AtkModifier = 10f;
            archerClass.RangeModifier = 2;
            SetCharacterSkills();

            return archerClass;
        }

        public void SetCharacterSkills()
        {
            Types.CharacterSkills characterSkill01 = new Types.CharacterSkills();
            Types.CharacterSkills characterSkill02 = new Types.CharacterSkills();

            characterSkill01.Name = "Arrow Shower";
            characterSkill01.SkillValueBase = 25f;
            characterSkill01.SkillValueMultiplayer = 1f;

            characterSkill02.Name = "Arrow Repel"; // TODO IMPLEMENT KNOCKBACK
            characterSkill02.SkillValueBase = 15f;
            characterSkill02.SkillValueMultiplayer = 1f;

            archerClass.Skills = new Types.CharacterSkills[2];
            archerClass.Skills[0] = characterSkill01;
            archerClass.Skills[1] = characterSkill02;
        }
    }
}