using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Warrior : Types.CharacterClassSpecific
    {
        Types.CharacterClassSpecific _warriorClass = new Types.CharacterClassSpecific();

        public Types.CharacterClassSpecific WarriorClass
        {
            get => _warriorClass;
            set => _warriorClass = value;
        }

        public Types.CharacterClassSpecific GetClassSpecific()
        {
            WarriorClass = new Types.CharacterClassSpecific();
            WarriorClass.CharacterClass = Types.CharacterClass.Warrior;
            WarriorClass.HpModifier = 5f;
            WarriorClass.AtkModifier = 20f;
            SetCharacterSkills();

            return WarriorClass;
        }

        public void SetCharacterSkills()
        {
            Types.CharacterSkills characterSkill01 = new Types.CharacterSkills();
            Types.CharacterSkills characterSkill02 = new Types.CharacterSkills();

            characterSkill01.Name = "Berserker Slash";
            characterSkill01.SkillValueBase = 30f;
            characterSkill01.SkillValueMultiplier = 1.5f;
            characterSkill01.SkillEffects = Types.SkillEffects.None;

            characterSkill02.Name = "Roar";
            characterSkill02.SkillValueBase = 10f;
            characterSkill02.SkillValueMultiplier = 1f;
            characterSkill02.SkillEffects = Types.SkillEffects.Stun;

            WarriorClass.Skills = new Types.CharacterSkills[2];
            WarriorClass.Skills[0] = characterSkill01;
            WarriorClass.Skills[1] = characterSkill02;
        }
    }
}