namespace SnakeGame.Models
{
    /// <summary>
    /// Структура, представляющая один пиксель на экране консоли. 
    /// Хранит координаты, цвет и символ пикселя.
    /// </summary>
    public readonly struct Pixel
    {
        /// <summary>
        /// Символы, используемые для отображения различных элементов.
        /// </summary>
        private const char BorderChar = '█'; // Граница
        private const char FoodChar = 'o';   // Еда
        private const char BombChar = '*';   // Бомба

        /// <summary>
        /// Конструктор для создания пикселя с заданными координатами, цветом и символом.
        /// </summary>
        /// <param name="x">Координата X пикселя.</param>
        /// <param name="y">Координата Y пикселя.</param>
        /// <param name="color">Цвет пикселя.</param>
        /// <param name="pixelChar">Символ для отрисовки пикселя. По умолчанию — символ границы.</param>
        public Pixel(int x, int y, ConsoleColor color, char pixelChar = BorderChar)
        {
            X = x;
            Y = y;
            Color = color;
            PixelChar = pixelChar;
        }

        /// <summary> Координата X пикселя.</summary>
        public int X { get; }

        /// <summary> Координата Y пикселя.</summary>
        public int Y { get; }

        /// <summary> Цвет пикселя.</summary>
        public ConsoleColor Color { get; }

        /// <summary> Символ для отрисовки пикселя.</summary>
        private char PixelChar { get; }

        /// <summary> Метод для отрисовки пикселя на экране.</summary>
        public void Draw()
        {
            Console.ForegroundColor = Color;      // Устанавливаем цвет пикселя
            Console.SetCursorPosition(X, Y);      // Перемещаем курсор на заданные координаты
            Console.Write(PixelChar);             // Рисуем пиксель
            Console.ResetColor();                 // Сбрасываем цвет
        }

        /// <summary> Метод для очистки пикселя с экрана.</summary>
        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');                   // Очищаем пиксель, заполняя пробелом
        }
    }
}
