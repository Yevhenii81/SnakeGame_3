using SnakeGame.Models;
using SnakeGame.GameState;

namespace SnakeGame.Helpers
{
    /// <summary>
    /// Класс, отвечающий за генерацию еды в игре.
    /// </summary>
    public static class FoodGenerator
    {
        // Экземпляр генератора случайных чисел.
        private static readonly Random Random = new Random();

        /// <summary>
        /// Генерирует новую еду на игровой карте, гарантируя, что она не будет пересекаться с головой и телом змейки.
        /// </summary>
        /// <param name="snake">Объект змейки, чтобы проверить отсутствие пересечения.</param>
        /// <param name="mapWidth">Ширина карты, чтобы ограничить координаты еды.</param>
        /// <param name="mapHeight">Высота карты, чтобы ограничить координаты еды.</param>
        /// <param name="foodColor">Цвет еды для отображения.</param>
        /// <returns>Пиксель, представляющий еду, с случайными координатами.</returns>
        public static Pixel GenFood(Snake snake, int mapWidth, int mapHeight, ConsoleColor foodColor)
        {
            Pixel food;
            do
            {
                // Генерируем координаты еды, ограниченные размерами карты.
                food = new Pixel(Random.Next(2, mapWidth - 2), Random.Next(1, mapHeight - 1), foodColor, 'o');
            }
            // Проверяем, чтобы еда не появилась на месте головы или тела змейки.
            while (snake.Head.X == food.X && snake.Head.Y == food.Y ||
                   snake.Body.Any(b => b.X == food.X && b.Y == food.Y));

            return food; // Возвращаем корректно сгенерированную еду.
        }
    }
}
