using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void EasyGame() 
    {
        GameManager.Instance.StartGame(LevelDifficulty.Easy);
    }
    public void MediumGame() 
    {
        GameManager.Instance.StartGame(LevelDifficulty.Medium);
    }
    public void HardGame() 
    {
        GameManager.Instance.StartGame(LevelDifficulty.Hard);
    }
    public void ExitGame()
    {

    }
}
