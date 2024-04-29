using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas MenuPanel;
    [SerializeField] private Canvas SettingsPanel;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private MonoBehaviour[] componentsToDisableOnPause;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        foreach (var component in componentsToDisableOnPause)
        {
            component.enabled = true;
        }
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        foreach (var component in componentsToDisableOnPause)
        {
            component.enabled = false;
        }
    }

    public void Settings()
    {
        MenuPanel.gameObject.SetActive(false);
        SettingsPanel.gameObject.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit() => Application.Quit();
}