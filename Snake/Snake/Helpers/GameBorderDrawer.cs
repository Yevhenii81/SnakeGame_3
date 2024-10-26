using SnakeGame.Models;
using SnakeGame.Helpers;
using SnakeGame.Utilities;

namespace SnakeGame.Helpers
{
    /// <summary>
    /// Класс для отрисовки границ игрового поля.
    /// </summary>
    public static class GameBorderDrawer
    {
        /// <summary>
        /// Рисует границы игрового поля.
        /// </summary>
        /// <param name="mapWidth">Ширина игрового поля.</param>
        /// <param name="mapHeight">Высота игрового поля.</param>
        public static void DrawBorder(int mapWidth, int mapHeight)
        {
            // Отрисовка верхней границы
            for (int i = 0; i <= mapWidth; i++)
            {
                new Pixel(i, 0, Configurations.BorderColor).Draw(); // Рисуем верхний ряд
            }

            // Отрисовка боковых границ
            for (int i = 1; i <= mapHeight; i++)
            {
                new Pixel(0, i, Configurations.BorderColor).Draw(); // Левая граница
                new Pixel(1, i, Configurations.BorderColor).Draw(); // Вторая левая граница
                new Pixel(mapWidth, i, Configurations.BorderColor).Draw(); // Правая граница
                new Pixel(mapWidth - 1, i, Configurations.BorderColor).Draw(); // Вторая правая граница
            }

            // Отрисовка нижней границы
            for (int i = 0; i <= mapWidth; i++)
            {
                new Pixel(i, mapHeight, Configurations.BorderColor).Draw(); // Рисуем нижний ряд
            }
        }
    }
}