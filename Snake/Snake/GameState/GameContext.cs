namespace SnakeGame.GameState
{
    /// <summary>
    /// Класс для управления состоянием игры.
    /// </summary>
    public class GameContext
    {
        /// <summary>
        /// Получает или задает текущее состояние игры.
        /// </summary>
        public IGameState CurrentState { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GameContext"/> с заданным начальным состоянием.
        /// </summary>
        /// <param name="initialState">Начальное состояние игры.</param>
        public GameContext(IGameState initialState)
        {
            CurrentState = initialState;
        }

        /// <summary>
        /// Выполняет обработку текущего состояния игры.
        /// </summary>
        public void Request()
        {
            CurrentState.Handle(this); // Обработка текущего состояния
        }
    }
}