using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 选择游戏的面板，通过这个面板进入不同的游戏，一共有6种游戏
/// </summary>
public class GameFish : MonoBehaviour
{
    // Start is called before the first frame update
  
    public int GameIndex;//通过游戏的索引加载不同游戏的场景
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnClick()
    {
        switch (GameIndex)
        {
            case 0:
                SceneManager.LoadScene("game01");
                break;
            case 1:
                SceneManager.LoadScene("game02");
                break;
            case 2:
                SceneManager.LoadScene("game03");
                break;
            case 3:
                SceneManager.LoadScene("game04");
                break;
            case 4:
                SceneManager.LoadScene("game05");
                break;
            case 5:
                SceneManager.LoadScene("game06");
                break;
        }
    }
}
