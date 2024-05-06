using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public static MainMenu instance;
    public string levelToLoad;
    public string newGameScene;
    public string mainMenuScene;
    public GameObject deathScreen;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
