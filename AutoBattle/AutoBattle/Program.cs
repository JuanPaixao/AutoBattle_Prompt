using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static AutoBattle.Types;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid battlefield = new Grid(5, 5);
            GridBox _playerCurrentLocation;
            GridBox _enemyCurrentLocation;
            Character _playerCharacter;
            Character _enemyCharacter;
            List<Character> _allPlayers = new List<Character>();
            int _currentTurn = 0;
            int _numberOfPossibleTiles = battlefield.grids.Count;
            bool _gameFinished = false;
            Setup();


            void Setup()
            {
                GetPlayerChoice();
            }

            void GetPlayerChoice()
            {
                //asks for the player to choose between for possible classes via console.
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Choose Between One of this Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nPlayer Class Choice: {characterClass}");
                _playerCharacter = new Character(characterClass);
                _playerCharacter.health = 100;
                _playerCharacter.baseDamage = 20;
                _playerCharacter.playerIndex = 0;
                var loadedClass = characterClassSpecific.GetClassBundle(characterClass);
                characterClassSpecific = loadedClass;
                _playerCharacter.health += characterClassSpecific.HpModifier;
                _playerCharacter.baseDamage += characterClassSpecific.AtkModifier;
                _playerCharacter.classSpecific = characterClassSpecific;

                WriteColor(
                    $"You selected [{characterClassSpecific.CharacterClass}] Class! This class have [{characterClassSpecific.AtkModifier} of Atk. Modifier], " +
                    $"{characterClassSpecific.HpModifier} of HP Modifier, and the class [skills] are [" +
                    $"{characterClassSpecific.Skills[0].Name}] and [{characterClassSpecific.Skills[1].Name}!]",
                    ConsoleColor.Yellow, ConsoleColor.Blue, true);
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");
                _enemyCharacter = new Character(enemyClass);
                _enemyCharacter.health = 100;
                _enemyCharacter.baseDamage = 20;
                _enemyCharacter.playerIndex = 1;
                var loadedClass = characterClassSpecific.GetClassBundle(enemyClass);
                characterClassSpecific = loadedClass;
                _enemyCharacter.health += characterClassSpecific.HpModifier;
                _enemyCharacter.baseDamage += characterClassSpecific.AtkModifier;
                _enemyCharacter.classSpecific = characterClassSpecific;

                WriteColor(
                    $"You selected [{characterClassSpecific.CharacterClass}] Class! This class have [{characterClassSpecific.AtkModifier} of Atk. Modifier], " +
                    $"{characterClassSpecific.HpModifier} of HP Modifier, and the class [skills] are [" +
                    $"{characterClassSpecific.Skills[0].Name}] and [{characterClassSpecific.Skills[1].Name}!]",
                    ConsoleColor.Yellow, ConsoleColor.Red, true);
                Console.ReadLine();

                StartGame();
            }

            void StartGame()
            {
                //populates the character variables and targets
                _enemyCharacter.Target = _playerCharacter;
                _playerCharacter.Target = _enemyCharacter;
                _allPlayers.Add(_playerCharacter);
                _allPlayers.Add(_enemyCharacter);
                if (GetRandomInt(0, 100) <= 50) _allPlayers.Reverse();
                EnableAttack();
                AllocatePlayers();
                StartTurn();
            }

            void EnableAttack()
            {
                _playerCharacter.canAttack = true;
                _enemyCharacter.canAttack = true;
            }

            void StartTurn()
            {
                foreach (Character character in _allPlayers)
                {
                    if (CheckIfAnyCharacterIsDead()) break;
                    character.StartTurn(battlefield);
                    Console.ReadLine();
                }

                _currentTurn++;
                HandleTurn();
            }

            bool CheckIfAnyCharacterIsDead()
            {
                if (_gameFinished) return true;
                if (_playerCharacter.isDead) FinishGame(_enemyCharacter);
                if (_enemyCharacter.isDead) FinishGame(_playerCharacter);
                return _playerCharacter.isDead || _enemyCharacter.isDead;
            }

            void FinishGame(Character character)
            {
                WriteColor($"The [Player {character.playerIndex}] is the [Winner!]\n", ConsoleColor.Yellow,
                    ConsoleColor.Green,
                    true);
                _gameFinished = true;
            }

            void HandleTurn()
            {
                if (CheckIfAnyCharacterIsDead())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Game Finsihed!\n");
                    ConsoleKeyInfo key = Console.ReadKey();
                }
                else StartTurn();
            }

            int GetRandomInt(int min, int max)
            {
                var rand = new Random();
                int index = rand.Next(min, max);
                return index;
            }

            void AllocatePlayers()
            {
                AllocatePlayerCharacter();
            }

            void AllocatePlayerCharacter()
            {
                int random = GetRandomInt(0, _numberOfPossibleTiles);
                GridBox randomLocation = (battlefield.grids.ElementAt(random));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"The player will start on {random} position\n");
                if (!randomLocation.occupied)
                {
                    GridBox playerCurrentLocation = randomLocation;
                    randomLocation.occupied = true;
                    battlefield.grids[random] = randomLocation;
                    _playerCharacter.currentBox = battlefield.grids[random];
                    AllocateEnemyCharacter();
                }
                else
                {
                    AllocatePlayerCharacter();
                }
            }

            void AllocateEnemyCharacter()
            {
                int random = GetRandomInt(0, _numberOfPossibleTiles);
                GridBox RandomLocation = (battlefield.grids.ElementAt(random));
                Console.WriteLine($"The enemy will start on {random} position\n");
                if (!RandomLocation.occupied)
                {
                    _enemyCurrentLocation = RandomLocation;
                    RandomLocation.occupied = true;
                    battlefield.grids[random] = RandomLocation;
                    _enemyCharacter.currentBox = battlefield.grids[random];
                    battlefield.DrawBattlefield(5, 5);
                }
                else
                {
                    AllocateEnemyCharacter();
                }
            }
        }

        public static void WriteColor(string message, ConsoleColor color, ConsoleColor previousColor, bool lineSkip)
        {
            var pieces = Regex.Split(message, @"(\[[^\]]*\])");

            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];

                if (piece.StartsWith("[") && piece.EndsWith("]"))
                {
                    Console.ForegroundColor = color;
                    piece = piece.Substring(1, piece.Length - 2);
                }

                Console.Write(piece);
                Console.ForegroundColor = previousColor;
            }

            if (lineSkip) Console.WriteLine();
        }
    }
}