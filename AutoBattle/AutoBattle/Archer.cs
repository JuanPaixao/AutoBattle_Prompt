using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Archer : Types.CharacterClassSpecific
    {
        Types.CharacterClassSpecific _archerClass = new Types.CharacterClassSpecific();

        public Types.CharacterClassSpecific ArcherClass
        {
            get => _archerClass;
            set => _archerClass = value;
        }

        public Types.CharacterClassSpecific GetClassSpecific()
        {
            ArcherClass = new Types.CharacterClassSpecific();
            ArcherClass.CharacterClass = Types.CharacterClass.Archer;
            ArcherClass.HpModifier = 30f;
            ArcherClass.AtkModifier = 10f;
            SetCharacterSkills();

            return ArcherClass;
        }

        public void SetCharacterSkills()
        {
            Types.CharacterSkills characterSkill01 = new Types.CharacterSkills();
            Types.CharacterSkills characterSkill02 = new Types.CharacterSkills();

            characterSkill01.Name = "Arrow Shower";
            characterSkill01.SkillValueBase = 25f;
            characterSkill01.SkillValueMultiplier = 1f;
            characterSkill01.SkillEffects = Types.SkillEffects.None;


            characterSkill02.Name = "Poison Arrow";
            characterSkill02.SkillValueBase = 20f;
            characterSkill02.SkillValueMultiplier = 1f;
            characterSkill02.SkillEffects = Types.SkillEffects.DamageOverTime;

            ArcherClass.Skills = new Types.CharacterSkills[2];
            ArcherClass.Skills[0] = characterSkill01;
            ArcherClass.Skills[1] = characterSkill02;
        }
    }
}