using System;
using System.Collections.Generic;
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
        public float damageMultiplier { get; set; }
        public int range;
        public GridBox currentBox;
        public int playerIndex;
        public bool isDead;
        public Character Target { get; set; }

        public Character(CharacterClass characterClass)
        {
        }


        public bool TakeDamage(float amount, Character attacker)
        {
            Console.WriteLine(
                $"Player {attacker.playerIndex} is attacking the player {this.playerIndex} and did {amount} damage." +
                $" Now the Player {this.playerIndex} is with {this.health - amount} HP!\n");

            if ((health -= amount) <= 0)
            {
                Die(attacker);
                return true;
            }

            return false;
        }

        public void Die(Character attacker)
        {
            Console.WriteLine($"The Player {playerIndex} is dead :(\n");
            Console.WriteLine($"The Winner is: {attacker.playerIndex}!!!\n");
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
                Attack(Target);
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
                        battlefield.drawBattlefield(5, 5);

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
                    battlefield.drawBattlefield(5, 5);

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
                    battlefield.drawBattlefield(5, 5);
                }
                else if (this.currentBox.yIndex < Target.currentBox.yIndex)
                {
                    this.currentBox.occupied = false;
                    battlefield.grids[currentBox.Index] = this.currentBox;
                    this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.yLength));
                    this.currentBox.occupied = true;

                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {playerIndex} walked down\n");
                    battlefield.drawBattlefield(5, 5);
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

        public void Attack(Character target)
        {
            if (isDead) return;
            var rand = new Random();
            int calculatedDamage = (int)(baseDamage + damageMultiplier);
            target.TakeDamage(rand.Next(0, calculatedDamage), this);
        }
    }
}