namespace SnakeGame.Models
{
    /// <summary>
    /// Класс, представляющий еду для змейки.
    /// </summary>
    internal class Food
    {
        /// <summary>
        /// Координаты X еды на игровом поле.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координаты Y еды на игровом поле.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Цвет еды.
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// Символ, используемый для отображения еды.
        /// </summary>
        public char Symbol { get; set; }

        /// <summary>
        /// Конструктор класса <see cref="Food"/>.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <param name="color">Цвет еды.</param>
        /// <param name="symbol">Символ для отображения еды.</param>
        public Food(int x, int y, ConsoleColor color, char symbol)
        {
            X = x;
            Y = y;
            Color = color;
            Symbol = symbol;
        }

        /// <summary>
        /// Метод для отрисовки еды на консоли.
        /// </summary>
        public void Draw()
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol);
            Console.ResetColor();
        }

        /// <summary>
        /// Метод для очистки еды с консоли.
        /// </summary>
        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' '); // Очищаем символ
        }
    }
}
