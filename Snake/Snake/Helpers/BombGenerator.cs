using SnakeGame.Models;

namespace SnakeGame.Helpers
{
    /// <summary>
    /// Класс, отвечающий за генерацию бомб в игре.
    /// </summary>
    public static class BombGenerator
    {
        // Экземпляр генератора случайных чисел.
        private static readonly Random Random = new Random();

        /// <summary>
        /// Генерирует новую бомбу на игровой карте, гарантируя, что она не будет пересекаться с телом змейки или едой.
        /// </summary>
        /// <param name="snake">Объект змейки, чтобы проверить отсутствие пересечения.</param>
        /// <param name="food">Пиксель, представляющий еду, чтобы избежать совпадения позиций.</param>
        /// <param name="mapWidth">Ширина карты, чтобы ограничить координаты бомбы.</param>
        /// <param name="mapHeight">Высота карты, чтобы ограничить координаты бомбы.</param>
        /// <param name="bombColor">Цвет бомбы для отображения.</param>
        /// <returns>Пиксель, представляющий бомбу, с случайными координатами.</returns>
        public static Pixel GenBomb(Snake snake, Pixel food, int mapWidth, int mapHeight, ConsoleColor bombColor)
        {
            Pixel bomb;
            do
            {
                // Генерируем координаты бомбы, ограниченные размерами карты.
                bomb = new Pixel(Random.Next(2, mapWidth - 2), Random.Next(1, mapHeight - 1), bombColor, '*');
            }
            // Повторяем генерацию, если координаты бомбы совпадают с позицией змейки или еды.
            while (snake.Body.Any(b => b.X == bomb.X && b.Y == bomb.Y ||
                                       (food.X == bomb.X && food.Y == bomb.Y)));

            return bomb; // Возвращаем корректно сгенерированную бомбу.
        }
    }
}
