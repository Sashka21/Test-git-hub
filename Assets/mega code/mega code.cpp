using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Метод для кнопки "Start"
    public void PlayGame()
    {
        // Завантажує сцену з грою
        SceneManager.LoadScene("GameScene");
    }

    // Метод для кнопки "Settings"
    public void OpenSettings()
    {
        // Тут можна відкрити панель налаштувань або іншу сцену
        Debug.Log("Settings button pressed!");
    }

    // Метод для кнопки "Quit" (опціонально)
    public void QuitGame()
    {
        Debug.Log("Quit button pressed!");
        Application.Quit();
    }
}
