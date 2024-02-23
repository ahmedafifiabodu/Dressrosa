using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string resumeKey = "resumeSceneIndex";
    private int defaultSceneIndex = 1;
    public void PlayGame()
    {

        SceneManager.LoadSceneAsync(1);
    }


    public void ResumeGame()
    {
      
        if (PlayerPrefs.HasKey(resumeKey))
        {
            int resumeSceneIndex = PlayerPrefs.GetInt(resumeKey);
            SceneManager.LoadSceneAsync(resumeSceneIndex);
        }
        else
        {
           
            SceneManager.LoadSceneAsync(defaultSceneIndex);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}