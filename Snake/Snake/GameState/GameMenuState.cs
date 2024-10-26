using SnakeGame.Menu;
using static System.Console;

namespace SnakeGame.GameState
{
    /// <summary>
    /// Представляет состояние игрового меню.
    /// </summary>
    public class GameMenuState : IGameState
    {
        /// <summary>
        /// Обрабатывает текущее состояние меню игры.
        /// </summary>
        /// <param name="context">Контекст игры, содержащий текущее состояние.</param>
        public void Handle(GameContext context)
        {
            Run(); // Выполнение метода Run для отображения меню
        }

        /// <summary>
        /// Запускает процесс отображения игрового меню.
        /// </summary>
        public void Run()
        {
            MenuManager.ShowMenu(); // Показать меню через MenuManager
        }
    }
}