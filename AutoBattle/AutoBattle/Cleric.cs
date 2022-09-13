using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Cleric : Types.CharacterClassSpecific
    {
        Types.CharacterClassSpecific _clericClass = new Types.CharacterClassSpecific();

        public Types.CharacterClassSpecific ClericClass
        {
            get => _clericClass;
            set => _clericClass = value;
        }

        public Types.CharacterClassSpecific GetClassSpecific()
        {
            ClericClass = new Types.CharacterClassSpecific();
            ClericClass.CharacterClass = Types.CharacterClass.Cleric;
            ClericClass.HpModifier = 30f;
            ClericClass.AtkModifier = 10f;
            SetCharacterSkills();

            return ClericClass;
        }

        public void SetCharacterSkills()
        {
            Types.CharacterSkills characterSkill01 = new Types.CharacterSkills();
            Types.CharacterSkills characterSkill02 = new Types.CharacterSkills();

            characterSkill01.Name = "Holy Light";
            characterSkill01.SkillValueBase = 10f;
            characterSkill01.SkillValueMultiplier = 1.75f;
            characterSkill01.SkillEffects = Types.SkillEffects.None;

            characterSkill02.Name = "Heal"; // TODO IMPLEMENT HEAL AS UNIQUE FEATURE
            characterSkill02.SkillValueBase = 20f;
            characterSkill02.SkillValueMultiplier = 1f;
            characterSkill02.SkillEffects = Types.SkillEffects.Heal;

            ClericClass.Skills = new Types.CharacterSkills[2];
            ClericClass.Skills[0] = characterSkill01;
            ClericClass.Skills[1] = characterSkill02;
        }
    }
}