using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameObject pauseMenu;
    private GameObject gameOverScreen;

    private void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        gameOverScreen = GameObject.FindGameObjectWithTag("GameOver");
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = Mathf.Abs(Time.timeScale - 1);

            pauseMenu.SetActive(!pauseMenu.active);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(true);
    }
}
