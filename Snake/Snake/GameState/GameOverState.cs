namespace SnakeGame.GameState;
using static System.Console;
using SnakeGame.Menu;

/// <summary>
/// Представляет состояние игры после проигрыша.
/// </summary>
public class GameOverState : IGameState
{
    private readonly int _finalScore; // Финальный счёт игрока

    /// <summary>
    /// Инициализирует новое состояние игры с заданным финальным счётом.
    /// </summary>
    /// <param name="finalScore">Финальный счёт игрока.</param>
    public GameOverState(int finalScore)
    {
        _finalScore = finalScore; // Установка финального счёта
    }

    /// <summary>
    /// Обрабатывает текущее состояние игры после проигрыша.
    /// </summary>
    /// <param name="context">Контекст игры, содержащий текущее состояние.</param>
    public void Handle(GameContext context)
    {
        Run(); // Выполнение метода Run для отображения состояния проигрыша
        context.CurrentState = new GameMenuState(); // Переключение на состояние игрового меню
    }

    /// <summary>
    /// Запускает процесс отображения состояния проигрыша.
    /// </summary>
    public void Run()
    {
        LoseGame(_finalScore); // Вызов метода проигрыша с финальным счётом
    }

    /// <summary>
    /// Отображает сообщение о проигрыше и финальный счёт.
    /// </summary>
    /// <param name="score">Финальный счёт игрока.</param>
    private void LoseGame(int score)
    {
        SetCursorPosition(WindowWidth / 3, WindowHeight / 2); // Установка курсора в центр
        WriteLine($"Game over, score: {score}"); // Отображение сообщения о проигрыше
        Task.Run(() => Beep(200, 600)); // Звук проигрыша
        ShowGameOverMenu(); // Показать меню окончания игры
    }

    /// <summary>
    /// Отображает меню окончания игры и ожидает нажатия клавиши для возврата в меню.
    /// </summary>
    static void ShowGameOverMenu()
    {
        WriteLine("Press any key to return to the menu..."); // Сообщение для игрока
        ReadKey(); // Ожидание нажатия клавиши
        MenuManager.ShowMenu(); // Возврат в меню
    }
}