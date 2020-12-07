using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoBehaviour
{
    public static UIMgr inst;

    public MainPanel mainPanel; //主界面面板


    private void Awake()
    {
        inst = this;
    }


}
