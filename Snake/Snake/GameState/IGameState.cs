namespace SnakeGame.GameState
{
    /// <summary>
    /// Интерфейс, представляющий состояние игры.
    /// </summary>
    public interface IGameState
    {
        /// <summary>
        /// Запускает выполнение состояния игры.
        /// </summary>
        void Run();

        /// <summary>
        /// Обрабатывает текущее состояние игры.
        /// </summary>
        /// <param name="context">Контекст игры, содержащий текущее состояние.</param>
        void Handle(GameContext context); // Метод для обработки текущего состояния
    }
}