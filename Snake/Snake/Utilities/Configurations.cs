namespace SnakeGame.Utilities
{
    /// <summary>
    /// Класс, содержащий конфигурации и настройки игры.
    /// </summary>
    internal class Configurations
    {
        /// <summary>
        /// Время кадра в миллисекундах.
        /// </summary>
        public const int FrameMs = 200;

        /// <summary>
        /// Цвета для границ, головы и тела змеи, еды и бомбы.
        /// </summary>
        public static readonly ConsoleColor BorderColor = ConsoleColor.DarkRed; // Цвет границы
        public static readonly ConsoleColor HeadColor = ConsoleColor.DarkGreen; // Цвет головы змеи
        public static readonly ConsoleColor BodyColor = ConsoleColor.Green; // Цвет тела змеи
        public static readonly ConsoleColor FoodColor = ConsoleColor.Yellow; // Цвет еды
        public static readonly ConsoleColor BombColor = ConsoleColor.Red; // Цвет бомбы
    }
}