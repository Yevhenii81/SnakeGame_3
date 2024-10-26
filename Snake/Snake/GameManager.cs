using SnakeGame.GameState;
using SnakeGame.Helpers;

namespace SnakeGame
{
    /// <summary>
    /// Класс для управления состояниями игры.
    /// </summary>
    public class GameManager
    {
        private IGameState _currentState; // Текущее состояние игры

        /// <summary>
        /// Конструктор, инициализирующий начальное состояние игры.
        /// </summary>
        public GameManager()
        {
            _currentState = new GameStartState(); // Установка начального состояния
        }

        /// <summary>
        /// Запускает игру, управляя её состояниями и отображением.
        /// </summary>
        public void StartGame()
        {
            ConsoleHelper consoleHelper = new ConsoleHelper();
            consoleHelper.CenterConsoleWindow(); // Центрирование окна консоли

            GamePlayState gamePlayState = new GamePlayState();

            // Подписка на события победы и поражения
            gamePlayState.OnGameWin += HandleGameWin;
            gamePlayState.OnGameLoss += HandleGameLoss;

            _currentState = gamePlayState; // Установка текущего состояния в gamePlayState

            // Основной цикл игры
            while (true)
            {
                _currentState.Run(); // Запуск текущего состояния
                SwitchState(); // Переключение состояния игры
            }
        }

        /// <summary>
        /// Метод для обработки события победы.
        /// </summary>
        /// <param name="score">Счет игрока.</param>
        private void HandleGameWin(int score)
        {
            _currentState = new GameWinState(score); // Переключение в состояние победы
        }

        /// <summary>
        /// Метод для обработки события поражения.
        /// </summary>
        /// <param name="score">Счет игрока.</param>
        private void HandleGameLoss(int score)
        {
            _currentState = new GameOverState(score); // Переключение в состояние проигрыша
        }

        /// <summary>
        /// Переключает состояние игры в зависимости от текущего состояния.
        /// </summary>
        private void SwitchState()
        {
            if (_currentState is GameStartState)
                _currentState = new GamePlayState(); // Переход от начального состояния к игровому
            else if (_currentState is GamePlayState)
                _currentState = new GameOverState(GetScore()); // Переход к состоянию проигрыша
            else if (_currentState is GameOverState)
                _currentState = new GameStartState(); // Возврат к начальному состоянию
        }

        /// <summary>
        /// Получает текущий счет игрока.
        /// </summary>
        /// <returns>Текущий счет.</returns>
        private int GetScore()
        {
            // Логика получения счета
            return 0; // Замените на реальный счет
        }
    }
}