using SnakeGame.Helpers;
using static System.Console;

namespace SnakeGame.GameState
{
    /// <summary>
    /// Представляет состояние начала игры.
    /// </summary>
    public class GameStartState : IGameState
    {
        private static ConsoleHelper _consoleHelper = new ConsoleHelper();


        /// <summary>
        /// Обрабатывает текущее состояние, запускает отображение логотипа и переключает состояние на GamePlayState.
        /// </summary>
        /// <param name="context">Контекст игры, содержащий текущее состояние.</param>
        public void Handle(GameContext context)
        {
            Run(); // Запуск метода Run для отображения логотипа
            context.CurrentState = new GamePlayState(); // Переключение на состояние игры
        }

        /// <summary>
        /// Запускает отображение логотипа.
        /// </summary>
        public void Run()
        {
            ShowLogo(); // Вызов метода для отображения логотипа
        }

        /// <summary>
        /// Отображает логотип игры с задержкой между столбцами.
        /// </summary>
        public static void ShowLogo()
        {
            _consoleHelper.SetConsoleSize(120, 30);
            _consoleHelper.CenterConsoleWindow();

            Clear(); // Очистка консоли

            // Логотип в виде массива строк
            var logoLines = new[]
            {
                "  SSSSS  N   N   AAAAA   K   K  EEEEE ",
                " S       NN  N   A   A   K  K   E     ",
                "  SSS    N N N   AAAAA   KKK    EEEEE ",
                "     S   N  NN   A   A   K  K   E     ",
                " SSSSS   N   N   A   A   K   K  EEEEE "
            };

            int maxLength = logoLines[0].Length; // Длина самой длинной строки логотипа

            // Центрирование
            int startX = (WindowWidth - maxLength) / 2; // Координата X для центрирования логотипа
            int startY = (WindowHeight - logoLines.Length) / 2; // Координата Y для центрирования логотипа

            // Рисуем логотип по буквам вертикально
            for (int col = 0; col < maxLength; col++)
            {
                for (int row = 0; row < logoLines.Length; row++)
                {
                    if (col < logoLines[row].Length) // Проверка, чтобы не выйти за пределы строки
                    {
                        SetCursorPosition(startX + col, startY + row); // Установка позиции курсора
                        Write(logoLines[row][col]); // Вывод буквы логотипа
                    }
                }
                System.Threading.Thread.Sleep(100); // Задержка между столбцами
            }

            // Показ сообщения о начале игры
            SetCursorPosition(startX, startY + logoLines.Length + 1);
            Write("Press Enter to start..."); // Инструкция для пользователя

            // Ожидание нажатия клавиши Enter
            while (true)
            {
                if (KeyAvailable && ReadKey(true).Key == ConsoleKey.Enter) // Проверка нажатия клавиши Enter
                    break; // Выход из цикла, если Enter нажат
            }
        }
    }
}