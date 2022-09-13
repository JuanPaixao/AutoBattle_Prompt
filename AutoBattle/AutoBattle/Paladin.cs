using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Paladin : Types.CharacterClassSpecific
    {
        Types.CharacterClassSpecific _paladinClass = new Types.CharacterClassSpecific();

        public Types.CharacterClassSpecific PaladinClass
        {
            get => _paladinClass;
            set => _paladinClass = value;
        }

        public Types.CharacterClassSpecific GetClassSpecific()
        {
            PaladinClass = new Types.CharacterClassSpecific();
            PaladinClass.CharacterClass = Types.CharacterClass.Paladin;
            PaladinClass.HpModifier = 10f;
            PaladinClass.AtkModifier = 15f;
            SetCharacterSkills();

            return PaladinClass;
        }

        public void SetCharacterSkills()
        {
            Types.CharacterSkills characterSkill01 = new Types.CharacterSkills();
            Types.CharacterSkills characterSkill02 = new Types.CharacterSkills();

            characterSkill01.Name = "Holy Sword";
            characterSkill01.SkillValueBase = 20f;
            characterSkill01.SkillValueMultiplier = 1.5f;
            characterSkill01.SkillEffects = Types.SkillEffects.None;

            characterSkill02.Name = "Cross Slash"; // TODO IMPLEMENT DOUBLE ATTACK CHANCE
            characterSkill02.SkillValueBase = 15f;
            characterSkill02.SkillValueMultiplier = 1f;
            characterSkill02.SkillEffects = Types.SkillEffects.DoubleAttack;
            
            PaladinClass.Skills = new Types.CharacterSkills[2];
            PaladinClass.Skills[0] = characterSkill01;
            PaladinClass.Skills[1] = characterSkill02;
        }
    }
}