using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Canvas PauseCanvas;
    [SerializeField] private RectTransform MainPanel;
    [SerializeField] private RectTransform SettingsPanel;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        //AudioManager.instance.PlaySFX("ButtonClick");
        Time.timeScale = 0f;
        //set any player/input script as false
        PauseCanvas.gameObject.SetActive(true);
    }

    public void ContinueBttn()
    {
        //AudioManager.instance.PlaySFX("ButtonClick");
        Time.timeScale = 1.0f;
        PauseCanvas.gameObject.SetActive(false);
    }

    public void MainMenuBttn()
    {
        //AudioManager.instance.PlaySFX("ButtonClick");
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }

    public void SettingsBttn()
    {
        MainPanel.gameObject.SetActive(false);
        SettingsPanel.gameObject.SetActive(true);
    }

    public void ExitBttn()
    {
        
        //in built ver
        Application.Quit();
    }

    //////////////

    public void ReturnToMainBttn()
    {
        //AudioManager.instance.PlaySFX("ButtonClick");
        MainPanel.gameObject.SetActive(true);
        SettingsPanel.gameObject.SetActive(false);
    }
}