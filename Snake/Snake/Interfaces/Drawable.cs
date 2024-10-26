namespace SnakeGame.Interfaces
{
    /// <summary>
    /// Интерфейс для объектов, которые можно рисовать на экране.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Рисует объект на экране.
        /// </summary>
        void Draw();

        /// <summary>
        /// Очищает объект с экрана.
        /// </summary>
        void Clear();
    }
}