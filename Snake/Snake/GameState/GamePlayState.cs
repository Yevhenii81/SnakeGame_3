using SnakeGame.Models;
using SnakeGame.Helpers;
using SnakeGame.Menu;
using SnakeGame.Utilities; 
using static System.Console;

namespace SnakeGame.GameState
{
    /// <summary>
    /// Представляет состояние игрового процесса.
    /// </summary>
    public class GamePlayState : IGameState
    {
        // Делегат для обработки событий победы и поражения
        public delegate void GameEventHandler(int score);

        // События, которые вызываются при победе или поражении
        public event GameEventHandler? OnGameWin;
        public event GameEventHandler? OnGameLoss;

        /// <summary>
        /// Запускает игровой процесс.
        /// </summary>
        public void Run()
        {
            Title = "Snake Game"; // Установка заголовка консольного окна

            CursorVisible = false; // Скрытие курсора

            var consoleHelper = new ConsoleHelper();
            consoleHelper.CenterConsoleWindow(); // Центрирование окна консоли

            while (true)
            {
                GameStartState.ShowLogo(); // Показ логотипа игры
                MenuManager.ShowMenu(); // Показ главного меню
                ReadKey(); // Ожидание нажатия клавиши
            }
        }

        /// <summary>
        /// Начинает игру с заданными параметрами.
        /// </summary>
        /// <param name="mapWidth">Ширина игровой карты.</param>
        /// <param name="mapHeight">Высота игровой карты.</param>
        /// <param name="withBomb">Флаг, указывающий на наличие бомбы.</param>
        public void StartGame(int mapWidth, int mapHeight, bool withBomb)
        {
            // Установка размеров окна и буфера консоли
            SetWindowSize(mapWidth + 1, mapHeight + 1);
            SetBufferSize(mapWidth + 1, mapHeight + 1);

            Clear(); // Очистка экрана
            GameBorderDrawer.DrawBorder(mapWidth, mapHeight); // Отрисовка границ игрового поля

            Direction currentMovement = Direction.Right; // Начальное направление движения змейки
            var snake = new Snake(10, 5, Configurations.HeadColor, Configurations.BodyColor); // Создание змейки

            Pixel food = FoodGenerator.GenFood(snake, mapWidth, mapHeight, Configurations.FoodColor); // Генерация еды
            food.Draw(); // Отрисовка еды

            Pixel? bomb = withBomb ? BombGenerator.GenBomb(snake, food, mapWidth, mapHeight, Configurations.BombColor) : (Pixel?)null; // Генерация бомбы
            bomb?.Draw(); // Отрисовка бомбы

            int score = 0; // Текущий счёт
            int maxScore = (mapWidth - 2) * (mapHeight - 2) - snake.Body.Count - 1; // Максимальный возможный счёт
            Stopwatch sw = new Stopwatch(); // Создание таймера

            while (true)
            {
                sw.Restart(); // Перезапуск таймера
                Direction oldMovement = currentMovement; // Сохранение старого направления движения

                // Цикл, продолжающийся до истечения времени кадра
                while (sw.ElapsedMilliseconds <= Configurations.FrameMs)
                {
                    if (currentMovement == oldMovement)
                    {
                        currentMovement = ReadMovement(currentMovement); // Чтение движения
                    }
                }

                // Проверка на столкновение с едой
                if (snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    snake.Move(currentMovement, true); // Змейка съедает еду
                    food = FoodGenerator.GenFood(snake, mapWidth, mapHeight, Configurations.FoodColor); // Генерация новой еды
                    food.Draw(); // Отрисовка новой еды

                    if (withBomb)
                    {
                        bomb?.Clear(); // Очистка старой бомбы
                        bomb = BombGenerator.GenBomb(snake, food, mapWidth, mapHeight, Configurations.BombColor); // Генерация новой бомбы
                        bomb?.Draw(); // Отрисовка новой бомбы
                    }

                    score++; // Увеличение счёта

                    Task.Run(() => Beep(1200, 200)); // Звук при съедании еды

                    // Проверка на победу
                    if (score == maxScore)
                    {
                        OnGameWin?.Invoke(score); // Вызов события победы
                        break;
                    }
                }
                // Проверка на столкновение с бомбой
                else if (withBomb && bomb.HasValue && snake.Head.X == bomb.Value.X && snake.Head.Y == bomb.Value.Y)
                {
                    OnGameLoss?.Invoke(score); // Вызов события поражения
                    HandleLoss(score); // Обработка поражения
                    break;
                }
                else
                {
                    snake.Move(currentMovement); // Движение змейки
                }

                // Проверка на столкновение с границей или своим телом
                if (snake.Head.X == mapWidth - 1
                   || snake.Head.X == 0
                   || snake.Head.Y == mapHeight - 1
                   || snake.Head.Y == 0
                   || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                {
                    OnGameLoss?.Invoke(score); // Вызов события поражения
                    HandleLoss(score); // Обработка поражения
                    break;
                }
            }

            snake.Clear(); // Очистка змейки
            food.Clear(); // Очистка еды
            bomb?.Clear(); // Очистка бомбы, если она есть
        }

        /// <summary>
        /// Обрабатывает текущее состояние игры.
        /// </summary>
        /// <param name="context">Контекст игры.</param>
        public void Handle(GameContext context)
        {
            Run(); // Запуск игрового процесса
        }

        /// <summary>
        /// Обрабатывает событие поражения.
        /// </summary>
        /// <param name="score">Текущий счёт игрока.</param>
        private void HandleLoss(int score)
        {
            // Переключение в состояние GameOver при проигрыше
            var context = new GameContext(new GameOverState(score));
            context.Request(); // Запрос на выполнение состояния GameOver
        }

        /// <summary>
        /// Обрабатывает событие победы.
        /// </summary>
        /// <param name="score">Текущий счёт игрока.</param>
        private void HandleWin(int score)
        {
            var context = new GameContext(new GameOverState(score));
            context.Request(); // Запрос на выполнение состояния GameOver
        }

        /// <summary>
        /// Читает движение змейки с клавиатуры.
        /// </summary>
        /// <param name="currentDirection">Текущее направление движения.</param>
        /// <returns>Новое направление движения.</returns>
        public Direction ReadMovement(Direction currentDirection)
        {
            if (!Console.KeyAvailable)
                return currentDirection; // Если клавиша не нажата, возвращаем текущее направление

            ConsoleKey key = Console.ReadKey(true).Key; // Чтение нажатой клавиши

            currentDirection = key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up, // Переключение на вверх
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down, // Переключение на вниз
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left, // Переключение на влево
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right, // Переключение на вправо
                _ => currentDirection // Если клавиша не соответствует, возвращаем текущее направление
            };

            return currentDirection; // Возвращаем новое направление
        }
    }
}