namespace SnakeGame.Models
{
    /// <summary>
    /// Класс, представляющий змею в игре.
    /// </summary>
    public class Snake
    {
        /// <summary>
        /// Цвет головы змеи.
        /// </summary>
        private readonly ConsoleColor _headColor;

        /// <summary>
        /// Цвет тела змеи.
        /// </summary>
        private readonly ConsoleColor _bodyColor;

        /// <summary>
        /// Конструктор, создающий змею с заданными начальными координатами,
        /// цветом головы и тела, а также длиной тела.
        /// </summary>
        /// <param name="initialX">Начальная координата X головы змеи.</param>
        /// <param name="initialY">Начальная координата Y головы змеи.</param>
        /// <param name="headColor">Цвет головы змеи.</param>
        /// <param name="bodyColor">Цвет тела змеи.</param>
        /// <param name="bodyLength">Длина тела змеи (по умолчанию 3).</param>
        public Snake(int initialX,
            int initialY,
            ConsoleColor headColor,
            ConsoleColor bodyColor,
            int bodyLength = 3)
        {
            _headColor = headColor; // Задаём цвет головы
            _bodyColor = bodyColor; // Задаём цвет тела

            Head = new Pixel(initialX, initialY, headColor); // Создаём голову

            // Создаём начальное тело змеи
            for (int i = bodyLength; i >= 0; i--)
            {
                Body.Enqueue(new Pixel(Head.X - i - 1, initialY, _bodyColor));
            }

            Draw(); // Отрисовываем змею
        }

        /// <summary>
        /// Свойство для головы змеи.
        /// </summary>
        public Pixel Head { get; private set; }

        /// <summary>
        /// Очередь для хранения пикселей, составляющих тело змеи.
        /// </summary>
        public Queue<Pixel> Body { get; } = new Queue<Pixel>();

        /// <summary>
        /// Метод для перемещения змеи в заданном направлении.
        /// </summary>
        /// <param name="direction">Направление движения змеи.</param>
        /// <param name="eat">Флаг, указывающий, ест ли змея (по умолчанию false).</param>
        public void Move(Direction direction, bool eat = false)
        {
            Clear(); // Очистка текущей позиции

            // Добавляем текущую голову в тело
            Body.Enqueue(new Pixel(Head.X, Head.Y, _bodyColor));
            if (!eat)
                Body.Dequeue(); // Убираем последний пиксель, если змея не ест

            // Обновляем позицию головы в зависимости от направления движения
            Head = direction switch
            {
                Direction.Right => new Pixel(Head.X + 1, Head.Y, _headColor),
                Direction.Left => new Pixel(Head.X - 1, Head.Y, _headColor),
                Direction.Up => new Pixel(Head.X, Head.Y - 1, _headColor),
                Direction.Down => new Pixel(Head.X, Head.Y + 1, _headColor),
                _ => Head
            };

            Draw(); // Отрисовка змеи на новом месте
        }

        /// <summary>
        /// Метод для отрисовки змеи на экране.
        /// </summary>
        public void Draw()
        {
            Head.Draw(); // Рисуем голову

            foreach (Pixel pixel in Body) // Рисуем тело
            {
                pixel.Draw();
            }
        }

        /// <summary>
        /// Метод для очистки змеи с экрана.
        /// </summary>
        public void Clear()
        {
            Head.Clear(); // Очистка головы

            foreach (Pixel pixel in Body) // Очистка тела
            {
                pixel.Clear();
            }
        }
    }
}