using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartTheGame()
    {
        SaveManager.LoadGame();
        SceneManager.LoadScene("Game");
    }
    
    public void SaveTheGame()
    {
        SaveManager.SaveGame();
    }
}
