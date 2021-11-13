using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // 单例模式
    public static UIController Instance;
    // 收集物UI 文本组件
    private Text cherryText;
    //private Text diamondText;
    // 当前收集物数量
    private int cherryNum;
    //private int diamondNum;
    // 樱桃数量的属性，修改时候自动更新UI
    public int CherryNum
    {
        get => cherryNum;
        set
        {
            // 修改当前金币数量
            cherryNum = value;
            // 更新UI的文本显示
            cherryText.text = "x " + cherryNum;
        }
    }

    //钻石数量的属性，修改时自动更新UI
    /*
    public int DiamondNum
    {
        get => diamondNum;
        set
        {
            diamondNum = value;
            diamondText.text = "x " + diamondNum;
        }
    }
    */

    // 游戏一开始就执行
    private void Awake()
    {
        Instance = this;
        // 樱桃的Text组件查找
        cherryText = transform.Find("cherry/cherryText").GetComponent<Text>();
        //diamondText = transform.Find("diamond/diamondText").GetComponent<Text>();
    }

}
