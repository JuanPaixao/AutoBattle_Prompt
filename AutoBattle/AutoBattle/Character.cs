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
        public float damageMultiplier = 1f;
        public int range;
        public GridBox currentBox;
        public int playerIndex;
        public float damageOverTimeValue;
        public bool isWithDamageOverTime;
        public bool isDead;
        public bool canAttack;
        private Grid _battlefield;

        public CharacterClassSpecific classSpecific;

        public Character Target { get; set; }

        public Character(CharacterClass characterClass)
        {
        }


        public void TakeDamage(float amount, Character attacker, ConsoleColor previousColor)
        {
            if ((health -= amount) <= 0)
            {
                Program.WriteColor(
                    $"[Player {attacker.playerIndex}] unleash the final blow on [Player {this.playerIndex}] and did [{amount} damage.]\n",
                    ConsoleColor.Yellow,
                    previousColor, true);
                Die();
            }
            else
                Program.WriteColor(
                    $"[Player {attacker.playerIndex}] is attacking the [Player {this.playerIndex}] and did [{amount} damage.]" +
                    $" Now the [Player {this.playerIndex}] is with [{this.health} HP!]\n", ConsoleColor.Yellow,
                    previousColor, true);
        }

        public void Heal(float amount, ConsoleColor previousColor)
        {
            health += amount;
            Program.WriteColor(
                $"[Player {this.playerIndex}] [heals {amount}]. Now it's HP is [{this.health}!]",
                ConsoleColor.Yellow,
                previousColor, true);
        }

        public void SkipTurn(ConsoleColor previousColor)
        {
            canAttack = false;
            Program.WriteColor(
                $"[Player {this.playerIndex}] can't attack due to an [abnormal status!]",
                ConsoleColor.Yellow,
                previousColor, true);
        }

        public void DoubleAttack(Character target, ConsoleColor previousColor)
        {
            Program.WriteColor(
                $"[Player {this.playerIndex}] will perform a [double attack!]",
                ConsoleColor.Yellow,
                previousColor, true);

            if (!target.isDead) Attack(target, false, new CharacterSkills(), SkillEffects.None);
            //
        }

        public void DamageOverTime(Character target, float damage, ConsoleColor previousColor)
        {
            health -= damage;
            if (health <= 0)
            {
                isDead = true;
                health = 0;
            }

            Program.WriteColor(
                $"[Player {this.playerIndex}] is [poisoned] and will receive [{damage}] of damage and is with [{health} HP]",
                ConsoleColor.Yellow,
                previousColor, true);
            target.isWithDamageOverTime = true;
            target.damageOverTimeValue = damage;
        }


        public void Die()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Program.WriteColor($"The [Player {playerIndex}] is [dead] :(", ConsoleColor.Yellow, ConsoleColor.Green,
                false);
            //   Program.WriteColor($"The Winner is: [{attacker.playerIndex}]!!!\n", ConsoleColor.Yellow,
            //         ConsoleColor.Green, true);
            currentBox.occupied = false;
            isDead = true;
        }

        public void StartTurn(Grid battlefield)
        {
            _battlefield = battlefield;

            ConsoleColor colorToUse = ConsoleColor.White;

            if (playerIndex == 0) colorToUse = ConsoleColor.Blue;
            else colorToUse = ConsoleColor.Red;

            Console.ForegroundColor = colorToUse;

            if (isWithDamageOverTime)
            {
                DamageOverTime(this, damageOverTimeValue, colorToUse);
                if (isDead)
                {
                    Die();
                    return;
                }
            }

            if (CheckCloseTargets(battlefield) != Direction.None)
            {
                bool willUseSkill = false;

                Random skillChance = new Random();
                int number = skillChance.Next(0, 100);
                willUseSkill = number <= chanceToUseSkill;
                CharacterSkills skill = new CharacterSkills();


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


                Attack(Target, willUseSkill, skill, skill.SkillEffects);
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
        Direction CheckCloseTargets(Grid battlefield)
        {
            bool left = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1).occupied);
            bool right = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1).occupied);
            bool up = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.yLength).occupied);
            bool down = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.yLength).occupied);
            Direction direction = Direction.None;
            if (up) return Direction.Up;
            if (left) return Direction.Left;
            if (right) return Direction.Right;
            if (down) return Direction.Down;

            return direction;
        }

        public void Attack(Character target, bool skillAttack, CharacterSkills skill, SkillEffects skillEffects)
        {
            if (isDead) return;

            SkillEffects skillEffect = skillEffects;

            ConsoleColor color = ConsoleColor.White;
            if (this.playerIndex == 0) color = ConsoleColor.Blue;
            else color = ConsoleColor.Red;

            if (canAttack)
            {
                var rand = new Random();
                int calculatedDamage = 0;

                if (!skillAttack)
                    calculatedDamage = (int)(baseDamage * damageMultiplier);

                else
                {
                    calculatedDamage =
                        (int)(baseDamage + skill.SkillValueBase * skill.SkillValueMultiplier);
                }

                AttackEffect(target, color, skill);

                if (skill.SkillEffects != SkillEffects.Heal && skill.SkillEffects != SkillEffects.DamageOverTime)
                    target.TakeDamage(rand.Next(0, calculatedDamage), this, color);
            }
            else
            {
                Program.WriteColor($"The [Player {playerIndex}] tried to attack but couldn't move!",
                    ConsoleColor.Yellow,
                    color, true);
                canAttack = true;
            }
        }

        public void AttackEffect(Character target, ConsoleColor color, CharacterSkills skill)
        {
            if (skill.SkillEffects == SkillEffects.None) return;
            if (skill.SkillEffects == SkillEffects.Stun) target.SkipTurn(color);
            if (skill.SkillEffects == SkillEffects.DamageOverTime)
                target.DamageOverTime(target, skill.SkillValueBase * skill.SkillValueMultiplier, color);
            if (skill.SkillEffects == SkillEffects.DoubleAttack) DoubleAttack(target, color);
            if (skill.SkillEffects == SkillEffects.Heal)
                Heal(skill.SkillValueBase * skill.SkillValueMultiplier, color);
        }
    }

    public enum Direction
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }
}