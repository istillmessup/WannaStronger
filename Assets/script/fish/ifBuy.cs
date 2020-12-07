using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 确认购买的的函数，yes按钮，将需要花的钱和买的鱼的种类传过去
/// </summary>
public class ifBuy : MonoBehaviour
{
    public int coin;    //价格
   
    public FishInfo fishinfo; //鱼的参数
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        
        UIMgr.inst.mainPanel.OnBuyFish(coin, fishinfo); 
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
