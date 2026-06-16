using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale=0;

    }
    public void home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale=1;

    }
    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale=1;

    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale=1;

    }

}
