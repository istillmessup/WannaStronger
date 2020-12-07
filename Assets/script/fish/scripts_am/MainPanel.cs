using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 控制主面板的函数，挂靠在Oder90Canvers
/// </summary>
public class MainPanel : MonoBehaviour
{
    public Text coinLabel;          //金币显示数量
    public Toggle muteToogle;       //音乐播放的控件

    public Transform fishHolder;    //购买的鱼生成的位置
    public Transform[] genPositions;//生成鱼的位置
    public GameObject[] fishPrefabs;//存储鱼的预制体

    public AudioManager audioManager;//控制音乐的控件


    public Transform no_money;  //没有钱的面板
    private GameData gameData;  //需要存储的数据


    
    private List<FishCtrl> fishCtrls = new List<FishCtrl>(); //已经购买的鱼
    private void OnEnable() //awake前调用
    {
        GetData();
        SetUI();
    }
    private void OnDisable()
    {
        SaveData();
    }
    /// <summary>
    /// 获取数据
    /// </summary>
    private void GetData()
    {
        //获取信息
        if (PlayerPrefs.HasKey("gameData"))
        {
            string data = PlayerPrefs.GetString("gameData");
            gameData = JsonMapper.ToObject<GameData>(data);
        }
        else
        {
            //如果还无信息 进行初始化
            gameData = new GameData();
            gameData.coinAmount = 200; //初始值
            //没有购买过鱼
            gameData.fishInfos = new List<FishInfo>();//fish
            gameData.mute = false;

        }
    }
    /// <summary>
    /// 存储数据
    /// </summary>
    private void SaveData()
    {
        
        gameData.fishInfos = new List<FishInfo>();//fish 已经购买的鱼

        //鱼
        for (int i = 0; i < fishCtrls.Count; i++)
        {
            FishInfo info = fishCtrls[i].info; //将已购买的食物添加进存储的列表
            gameData.fishInfos.Add(info);
        }

        gameData.mute = !muteToogle.isOn;
        string data = JsonMapper.ToJson(gameData);
        PlayerPrefs.SetString("gameData", data);
    }
    /// <summary>
    /// 初始化UI
    /// </summary>
    private void SetUI()
    {
        //设置所拥有钱币数目
        coinLabel.text = gameData.coinAmount.ToString();
        //设置购买的fish
        for (int i = 0; i < gameData.fishInfos.Count; i++)
        {
            FishInfo info = gameData.fishInfos[i];
            AddFish(info);
        }
        audioManager.isMute = gameData.mute;
        muteToogle.isOn = !audioManager.isMute;

    }


    /// <summary>
    /// 增加鱼
    /// </summary>
    /// <param name="_info"></param>
    ///
    private void AddFish(FishInfo _info)
    {
        
        //在不同的位置随机的生成鱼
        int genPosIndex = Random.Range(0, genPositions.Length);//随机生成鱼的位置
        GameObject fish = Instantiate(fishPrefabs[_info.fishID]);
        fish.transform.SetParent(fishHolder, false);
        fish.transform.localPosition = genPositions[genPosIndex].localPosition;
        fish.transform.localRotation = genPositions[genPosIndex].localRotation;
        FishCtrl fishCtrl = fish.GetComponent<FishCtrl>();
       
        fishCtrl.SetFish(_info);
        fishCtrls.Add(fishCtrl);
    }

    /// <summary>
    /// 当金钱发生变化
    /// </summary>
    /// <param name="_coin"></param>
    /// 金钱变化时 
    public void OnCoinChanged(int _coin)
    {
        gameData.coinAmount += _coin;
        coinLabel.text = gameData.coinAmount.ToString(); //同时更改显示面板

    }

  
    /// <summary>
    /// 购买鱼
    /// </summary>
    /// <param name="_coin"></param>
    /// <param name="_info"></param>
    public void OnBuyFish(int _coin, FishInfo _info)
    {
        if (gameData.coinAmount > _coin) //如果可以购买
        {
            OnCoinChanged(-_coin);      //拥有的钱减去价格
            AddFish(_info);             //增加鱼
        }
        else
        {
            no_money.gameObject.SetActive(true); //否则显示没钱的面板
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isOn"></param>
    public void SwitchMute(bool isOn)
    {
        audioManager.SwitchMuteState(isOn);
    }


    public void BackStart()
    {
        SceneManager.LoadScene("start");
    }

    private void Update()
    {
        if (Status.stat)
        {
            OnCoinChanged(100);
        }
        Status.stat = false;
    }
}
