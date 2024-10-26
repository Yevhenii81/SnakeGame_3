using static System.Console;

namespace SnakeGame.GameState
{
    /// <summary>
    /// Представляет состояние победы в игре.
    /// </summary>
    public class GameWinState : IGameState
    {
        private readonly int _score; // Итоговый счёт игрока

        /// <summary>
        /// Конструктор, который принимает текущий счёт.
        /// </summary>
        /// <param name="score">Текущий счёт игрока.</param>
        public GameWinState(int score)
        {
            _score = score; // Сохранение итогового счёта
        }

        /// <summary>
        /// Обрабатывает состояние победы, отображая экран победы.
        /// </summary>
        /// <param name="context">Контекст игры, содержащий текущее состояние.</param>
        public void Handle(GameContext context)
        {
            Run(); // Вызов метода для отображения победного экрана
        }

        /// <summary>
        /// Отображает экран победы с итоговым счётом.
        /// </summary>
        public void Run()
        {
            Clear(); // Очистка экрана
            SetCursorPosition(WindowWidth / 3, WindowHeight / 2); // Установка курсора на центр экрана
            WriteLine($"You Win! Final Score: {_score}"); // Отображение сообщения о победе

            Task.Run(() => Beep(1500, 600)); // Звук победы
            ShowGameOverMenu(); // Показать меню после игры
        }

        /// <summary>
        /// Отображает меню окончания игры и ожидает нажатия клавиши для возврата в главное меню.
        /// </summary>
        private void ShowGameOverMenu()
        {
            WriteLine("\nPress any key to return to the main menu..."); // Сообщение о возвращении в меню
            ReadKey(true); // Ожидание нажатия любой клавиши
            var context = new GameContext(new GameStartState()); // Вернуться к начальному состоянию
            context.Request(); // Вызвать начальное состояние
        }
    }
}