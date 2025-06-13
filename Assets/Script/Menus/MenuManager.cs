using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void NewGame()
    {    
        SceneManager.LoadScene("_Scene_0");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application is quitting.");
    }
}
