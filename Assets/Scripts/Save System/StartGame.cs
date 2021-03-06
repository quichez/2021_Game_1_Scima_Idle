using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartTheGame()
    {
        Debug.Log(Application.persistentDataPath);
        SaveManager.LoadGame();
        SceneManager.LoadScene(2);
    }
    
    public void SaveTheGame()
    {
        SaveManager.SaveGame();        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
