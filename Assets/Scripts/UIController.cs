using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // 单例模式
    public static UIController Instance;
    // 金币UI 文本组件
    private Text cherryText;
    // 当前金币数量
    private int cherryNum;

    // 金币数量的属性，修改时候自动更新UI
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

    // 游戏一开始就执行
    private void Awake()
    {
        Instance = this;
        // 金币的Text组件查找
        cherryText = transform.Find("cherry/cherryText").GetComponent<Text>();

    }

}
