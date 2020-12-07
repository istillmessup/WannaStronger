using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoginPanel : MonoBehaviour
{
    void Start()
    {
        
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("main");
    }
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
