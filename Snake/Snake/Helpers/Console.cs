using System.Runtime.InteropServices;

namespace SnakeGame.Helpers
{
    /// <summary>
    /// Класс для управления положением и размером окна консоли.
    /// </summary>
    internal class ConsoleHelper
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern nint GetConsoleWindow(); // Импортирование функции для получения дескриптора окна консоли.

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(nint hWnd, out RECT lpRect); // Импортирование функции для получения размера окна.

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(nint hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint); // Импортирование функции для перемещения окна.

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;   // Левая граница окна.
            public int Top;    // Верхняя граница окна.
            public int Right;  // Правая граница окна.
            public int Bottom; // Нижняя граница окна.
        }

        private const int ScreenWidth = 1920;  // Ширина экрана.
        private const int ScreenHeight = 1080; // Высота экрана.

        /// <summary>
        /// Центрирует окно консоли на экране.
        /// </summary>
        public void CenterConsoleWindow()
        {
            nint consoleWindow = GetConsoleWindow(); // Получение дескриптора окна консоли.
            if (consoleWindow == nint.Zero) return; // Проверка на успешное получение дескриптора.

            GetWindowRect(consoleWindow, out RECT rect); // Получение размеров окна консоли.
            int windowWidth = rect.Right - rect.Left; // Вычисление ширины окна.
            int windowHeight = rect.Bottom - rect.Top; // Вычисление высоты окна.

            int posX = (ScreenWidth - windowWidth) / 2; // Вычисление позиции X для центрирования.
            int posY = (ScreenHeight - windowHeight) / 2; // Вычисление позиции Y для центрирования.

            MoveWindow(consoleWindow, posX, posY, windowWidth, windowHeight, true); // Перемещение окна консоли.
        }

        /// <summary>
        /// Устанавливает размер окна консоли и скрывает курсор.
        /// </summary>
        /// <param name="width">Ширина окна консоли.</param>
        /// <param name="height">Высота окна консоли.</param>
        public void SetConsoleSize(int width, int height)
        {
            Console.SetWindowSize(width, height); // Установка размера окна консоли.
            Console.SetBufferSize(width, height); // Установка размера буфера консоли.
            Console.CursorVisible = false; // Скрытие курсора.
        }
    }
}