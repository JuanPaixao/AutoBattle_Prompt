using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Character
    {
        public string name { get; set; }
        public float health;
        public float baseDamage;
        public int chanceToUseSkill = 50;
        public float damageMultiplier { get; set; }
        public int range;
        public GridBox currentBox;
        public int playerIndex;
        public bool isDead;

        public CharacterClassSpecific classSpecific;

        public Character Target { get; set; }

        public Character(CharacterClass characterClass)
        {
        }


        public void TakeDamage(float amount, Character attacker, ConsoleColor previousColor)
        {
            Program.WriteColor(
                $"[Player {attacker.playerIndex}] is attacking the [Player {this.playerIndex}] and did [{amount} damage.]" +
                $" Now the [Player {this.playerIndex}] is with [{this.health - amount} HP!]\n", ConsoleColor.Yellow,
                previousColor, true);

            if ((health -= amount) <= 0)
            {
                Die(attacker);
            }
        }

        public void Die(Character attacker)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Program.WriteColor($"The [Player {playerIndex}] is [dead] :(\n", ConsoleColor.Yellow, ConsoleColor.Green,
                true);
            Program.WriteColor($"The Winner is: [{attacker.playerIndex}]!!!\n", ConsoleColor.Yellow,
                ConsoleColor.Green, true);
            currentBox.occupied = false;
            isDead = true;
        }

        public void WalkTO(bool CanWalk)
        {
        }

        public void StartTurn(Grid battlefield)
        {
            if (CheckCloseTargets(battlefield))
            {
                bool willUseSkill = false;

                Random skillChance = new Random();
                int number = skillChance.Next(0, 100);
                willUseSkill = number <= chanceToUseSkill;
                CharacterSkills skill = new CharacterSkills();

                ConsoleColor colorToUse = ConsoleColor.White;

                if (playerIndex == 0) colorToUse = ConsoleColor.Blue;
                else colorToUse = ConsoleColor.Red;

                Console.ForegroundColor = colorToUse;

                if (willUseSkill)
                {
                    Random skillType = new Random();
                    skill = this.classSpecific.Skills[skillType.Next(0, this.classSpecific.Skills.Length)];
                    Program.WriteColor($"The [Player {playerIndex}] will use the skill [{skill.Name}]",
                        ConsoleColor.Yellow,
                        colorToUse, true);
                }
                else
                    Program.WriteColor($"The [Player {playerIndex}] will attack with [basic attack]",
                        ConsoleColor.Yellow,
                        colorToUse, true);

                Attack(Target, willUseSkill, skill);
            }
            else
            {
                // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if (this.currentBox.xIndex > Target.currentBox.xIndex)
                {
                    if ((battlefield.grids.Exists(x => x.Index == currentBox.Index - 1)))
                    {
                        currentBox.occupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
                        currentBox.occupied = true;

                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {playerIndex} walked left\n");
                        battlefield.DrawBattlefield(5, 5);

                        return;
                    }
                }
                else if (currentBox.xIndex < Target.currentBox.xIndex)
                {
                    currentBox.occupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                    currentBox.occupied = true;

                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {playerIndex} walked right\n");
                    battlefield.DrawBattlefield(5, 5);

                    return;
                }

                if (this.currentBox.yIndex > Target.currentBox.yIndex)
                {
                    this.currentBox.occupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.yLength));
                    this.currentBox.occupied = true;

                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {playerIndex} walked up\n");
                    battlefield.DrawBattlefield(5, 5);
                }
                else if (this.currentBox.yIndex < Target.currentBox.yIndex)
                {
                    this.currentBox.occupied = false;
                    battlefield.grids[currentBox.Index] = this.currentBox;
                    this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.yLength));
                    this.currentBox.occupied = true;

                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {playerIndex} walked down\n");
                    battlefield.DrawBattlefield(5, 5);
                }
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            bool left = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1).occupied);
            bool right = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1).occupied);
            bool up = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.yLength).occupied);
            bool down = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.yLength).occupied);

            if (left || right || up || down)
            {
                return true;
            }

            return false;
        }

        public void Attack(Character target, bool skillAttack, CharacterSkills skill)
        {
            if (isDead) return;
            var rand = new Random();

            int calculatedDamage = 0;

            if (!skillAttack)
                calculatedDamage = (int)(baseDamage + damageMultiplier);

            else
            {
                calculatedDamage =
                    (int)(baseDamage + skill.SkillValueBase + skill.SkillValueMultiplier);
            }

            ConsoleColor color = ConsoleColor.White;
            if (this.playerIndex == 0) color = ConsoleColor.Blue;
            else color = ConsoleColor.Red;

            target.TakeDamage(rand.Next(0, calculatedDamage), this, color);
        }
    }
}