using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 游戏的商品界面，包括购买鱼需要的金币、鱼的id，在商品选择界面的每一个商品上
/// </summary>
public class ShopFish : MonoBehaviour
{
    public int coin;    //价格
    public FishInfo info;//鱼参数
    public ifBuy ifbuy;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);

    }

    //选择购买鱼后 就将鱼信息传给确认购买窗口
    private void OnClick()
    {
        ifbuy.coin = coin;
        ifbuy.fishinfo = info;

    }
}
