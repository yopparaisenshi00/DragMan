﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimitController : MonoBehaviour
{
    public Text time_text;

    public int min;     // 分
    public float sec;   // 秒

    public bool stop;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 制限時間計算
        Time_Limit();

        // 文字設定
        Set_Text();
    }

    void Time_Limit()
    {
        // 一秒づつ減らす
        if (!stop) sec -= Time.deltaTime;

        // 時間をとめる条件
        if (min <= 0 && sec <= 0)
        {
            stop = true;
            sec = 0;
        }

        // 秒が減ったら分減らす
        if (sec <= 0)
        {
            if (!stop) sec = 60;
            if (min > 0) min--;
        }
    }

    // 文字設定
    void Set_Text()
    {
        time_text.text = min + "分" + (int)sec + "秒";
    }

}
