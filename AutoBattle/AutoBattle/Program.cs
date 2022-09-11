using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid(5, 5);
            CharacterClass playerCharacterClass;
            GridBox PlayerCurrentLocation;
            GridBox EnemyCurrentLocation;
            Character PlayerCharacter;
            Character EnemyCharacter;
            List<Character> AllPlayers = new List<Character>();
            int currentTurn = 0;
            int numberOfPossibleTiles = grid.grids.Count;
            Setup();


            void Setup()
            {
                GetPlayerChoice();
            }

            void GetPlayerChoice()
            {
                //asks for the player to choose between for possible classes via console.
                Console.WriteLine("Choose Between One of this Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                //store the player choice in a variable
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "2":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "3":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "4":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    default:
                        GetPlayerChoice();
                        break;
                }
            }

            void CreatePlayerCharacter(int classIndex)
            {
                CharacterClass characterClass = (CharacterClass)classIndex;
                CharacterClassSpecific characterClassSpecific = new CharacterClassSpecific();

                Console.WriteLine($"Player Class Choice: {characterClass}");
                PlayerCharacter = new Character(characterClass);
                PlayerCharacter.health = 100;
                PlayerCharacter.baseDamage = 20;
                PlayerCharacter.playerIndex = 0;


                var loadedClass = characterClassSpecific.GetClassBundle(characterClass);
                characterClassSpecific = loadedClass;

                Console.WriteLine(
                    $"You selected {characterClassSpecific.CharacterClass} Class! This class have {characterClassSpecific.AtkModifier} of Atk. Modifier, " +
                    $"{characterClassSpecific.HpModifier} of HP Modifier {characterClassSpecific.RangeModifier} of Range Modifier and finally this class skills are " +
                    $"{characterClassSpecific.Skills[0].Name} and {characterClassSpecific.Skills[1].Name}!");
                Console.ReadLine();


                CreateEnemyCharacter();
            }

            void CreateEnemyCharacter()
            {
                //randomly choose the enemy class and set up vital variables
                var rand = new Random();
                int randomInteger = rand.Next(1, 4);
                CharacterClass enemyClass = (CharacterClass)randomInteger;
                CharacterClassSpecific characterClassSpecific = new CharacterClassSpecific();
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");
                EnemyCharacter = new Character(enemyClass);
                EnemyCharacter.health = 100;
                EnemyCharacter.baseDamage = 20;
                EnemyCharacter.playerIndex = 1;

                var loadedClass = characterClassSpecific.GetClassBundle(enemyClass);
                characterClassSpecific = loadedClass;

                Console.WriteLine(
                    $"You selected {characterClassSpecific.CharacterClass} Class! This class have {characterClassSpecific.AtkModifier} of Atk. Modifier, " +
                    $"{characterClassSpecific.HpModifier} of HP Modifier {characterClassSpecific.RangeModifier} of Range Modifier and finally this class skills are " +
                    $"{characterClassSpecific.Skills[0].Name} and {characterClassSpecific.Skills[1].Name}!");
                Console.ReadLine();

                StartGame();
            }

            void StartGame()
            {
                //populates the character variables and targets
                EnemyCharacter.Target = PlayerCharacter;
                PlayerCharacter.Target = EnemyCharacter;
                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);
                AlocatePlayers();
                StartTurn();
            }

            void StartTurn()
            {
                if (currentTurn == 0)
                {
                    //  AllPlayers.Sort();  
                }

                foreach (Character character in AllPlayers)
                {
                    character.StartTurn(grid);
                }

                currentTurn++;
                HandleTurn();
            }

            bool CheckIfAnyCharacterIsDead()
            {
                return PlayerCharacter.isDead || EnemyCharacter.isDead;
            }

            void HandleTurn()
            {
                if (CheckIfAnyCharacterIsDead())
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Game Finsihed!\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    ConsoleKeyInfo key = Console.ReadKey();
                }
                else
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key to start the next turn...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    ConsoleKeyInfo key = Console.ReadKey();
                    StartTurn();
                }
            }

            int GetRandomInt(int min, int max)
            {
                var rand = new Random();
                int index = rand.Next(min, max);
                return index;
            }

            void AlocatePlayers()
            {
                AlocatePlayerCharacter();
            }

            void AlocatePlayerCharacter()
            {
                int random = 0;
                GridBox RandomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!RandomLocation.occupied)
                {
                    GridBox PlayerCurrentLocation = RandomLocation;
                    RandomLocation.occupied = true;
                    grid.grids[random] = RandomLocation;
                    PlayerCharacter.currentBox = grid.grids[random];
                    AlocateEnemyCharacter();
                }
                else
                {
                    AlocatePlayerCharacter();
                }
            }

            void AlocateEnemyCharacter()
            {
                int random = 24;
                GridBox RandomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!RandomLocation.occupied)
                {
                    EnemyCurrentLocation = RandomLocation;
                    RandomLocation.occupied = true;
                    grid.grids[random] = RandomLocation;
                    EnemyCharacter.currentBox = grid.grids[random];
                    grid.drawBattlefield(5, 5);
                }
                else
                {
                    AlocateEnemyCharacter();
                }
            }
        }
    }
}