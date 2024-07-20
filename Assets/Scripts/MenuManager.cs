using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public void StartGame()
    {
        SceneManager.LoadScene("Main Scene");
        Debug.Log("Start Game");
    }
}
