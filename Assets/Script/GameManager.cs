using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float worldSpeed;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Check for pause input
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire3"))
        {
            Pause();
        }
    }


    public void Pause()
    {
        if (UIController.instance.pausePanel.activeSelf == false)
        {
            UIController.instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
            AudioManager.instance.PlaySound(AudioManager.instance.pause);
        }
        else
        {
            UIController.instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.instance.ExitBoost();
            AudioManager.instance.PlaySound(AudioManager.instance.unpause);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application is quitting.");
    }

    public void GoToMainMenu()
    { 
        SceneManager.LoadScene("Main_Menu");
    }

    public void GameOver()
    {
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game_Over");
    }

    public void MissionComplete()
    {
        StartCoroutine(ShowMissionCompleteScreen());
    }

    IEnumerator ShowMissionCompleteScreen()
    {
        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene("Game_Complete");
        Time.timeScale = 0f;
    }

}
