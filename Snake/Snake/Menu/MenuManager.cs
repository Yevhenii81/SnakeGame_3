﻿using SnakeGame.GameState;
using SnakeGame.Helpers;
using static System.Console;

namespace SnakeGame.Menu
{
    internal class MenuManager
    {
        private static GamePlayState _gamePlayState = new GamePlayState();
        private static ConsoleHelper _consoleHelper = new ConsoleHelper();

        /// <summary>
        /// Отображает главное меню игры.
        /// </summary>
        public static void ShowMenu()
        {
            _consoleHelper.SetConsoleSize(120, 30); // Устанавливаем размер 
            _consoleHelper.CenterConsoleWindow(); // Центрируем окно на экране

            Clear();
            string[] options = {
                "1. Правила",
                "2. Играть (Легкий уровень)",
                "3. Выбор уровня сложности",
                "4. Выйти"
            };

            int selectedOption = 0;

            while (true)
            {
                Clear();

                int menuWidth = options.Max(option => option.Length);
                int startX = (WindowWidth - menuWidth) / 2;
                int startY = (WindowHeight - options.Length) / 2;

                // Отрисовка меню с подсветкой
                for (int i = 0; i < options.Length; i++)
                {
                    SetCursorPosition(startX, startY + i);
                    if (i == selectedOption)
                    {
                        ForegroundColor = ConsoleColor.Blue; // Подсветка
                        WriteLine(" " + options[i]);
                        ResetColor();
                    }
                    else
                    {
                        WriteLine(options[i]);
                    }
                }

                ConsoleKey key = ReadKey(true).Key;

                // Обработка нажатия клавиш
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption == 0) ? options.Length - 1 : selectedOption - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption == options.Length - 1) ? 0 : selectedOption + 1;
                        break;
                    case ConsoleKey.Enter:
                        ExecuteMenuChoice(selectedOption);
                        return;
                }
            }
        }

        /// <summary>
        /// Выполняет выбранный пункт меню.
        /// </summary>
        /// <param name="selectedOption">Индекс выбранного пункта меню.</param>
        public static void ExecuteMenuChoice(int selectedOption)
        {
            switch (selectedOption)
            {
                case 0:
                    ShowRules(); // Показать правила
                    break;
                case 1:
                    _gamePlayState.StartGame(40, 20, withBomb: false); // Начать игру на легком уровне
                    break;
                case 2:
                    ChooseDifficulty(); // Выбор уровня сложности
                    break;
                case 3:
                    CloseConsole(); // Закрыть консоль
                    break;
            }
        }

        /// <summary>
        /// Отображает правила игры.
        /// </summary>
        public static void ShowRules()
        {
            Clear();
            WriteLine("Правила игры:");
            WriteLine("1. Управляйте змейкой с помощью стрелок.");
            WriteLine("2. Съедайте еду, чтобы расти и набирать очки.");
            WriteLine("3. Если съедите бомбу (в сложных уровнях), игра завершится.");
            WriteLine("\nНажмите любую клавишу для возврата в меню...");
            ReadKey();
            ShowMenu(); // Возврат в меню
        }

        /// <summary>
        /// Метод для выбора уровня сложности.
        /// </summary>
        public static void ChooseDifficulty()
        {
            Clear();
            string[] difficultyOptions = {
                "1. Легкий (без бомб, поле 40x20)",
                "2. Средний (с бомбами, поле 60x30)",
                "3. Сложный (с бомбами, поле 80x40)",
                "4. Назад"
            };

            int selectedDifficulty = 0;

            while (true)
            {
                Clear();

                int menuWidth = difficultyOptions.Max(option => option.Length);
                int startX = (WindowWidth - menuWidth) / 2;
                int startY = (WindowHeight - difficultyOptions.Length) / 2;

                // Отрисовка выбора уровня сложности
                for (int i = 0; i < difficultyOptions.Length; i++)
                {
                    SetCursorPosition(startX, startY + i);
                    if (i == selectedDifficulty)
                    {
                        ForegroundColor = ConsoleColor.Blue; // Подсветка выбора
                        WriteLine(" " + difficultyOptions[i]);
                        ResetColor();
                    }
                    else
                    {
                        WriteLine(difficultyOptions[i]);
                    }
                }

                ConsoleKey key = ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedDifficulty = (selectedDifficulty == 0) ? difficultyOptions.Length - 1 : selectedDifficulty - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedDifficulty = (selectedDifficulty == difficultyOptions.Length - 1) ? 0 : selectedDifficulty + 1;
                        break;
                    case ConsoleKey.Enter:
                        switch (selectedDifficulty)
                        {
                            case 0:
                                _gamePlayState.StartGame(40, 20, withBomb: false); // Легкий уровень
                                break;
                            case 1:
                                _gamePlayState.StartGame(60, 30, withBomb: true); // Средний уровень
                                break;
                            case 2:
                                _gamePlayState.StartGame(80, 40, withBomb: true); // Сложный уровень
                                break;
                            case 3:
                                ShowMenu(); // Возврат в меню
                                return;
                        }
                        return;
                }
            }
        }

        /// <summary>
        /// Метод для закрытия консоли.
        /// </summary>
        static void CloseConsole()
        {
            Clear();
            WriteLine("Игра завершена. Нажмите любую клавишу, чтобы закрыть.");
            ReadKey(true);
            Environment.Exit(0); // Закрытие программы и консоли
        }
    }
}