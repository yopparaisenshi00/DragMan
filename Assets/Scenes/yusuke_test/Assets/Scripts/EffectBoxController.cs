using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBoxController : MonoBehaviour
{
    // エフェクトの大きさ
    public float scale_spd; // エフェクトが小さくなる速さ    
    public float scale_min; // エフェクトの下限

    // 大きさの範囲
    public float init_scale_max;
    public float init_scale_min;

    // 角度の範囲
    public float init_rot_max;
    public float init_rot_min;

    // 待機時間
    private int timer;
    public int timer_max;

    private int state;
    private int timer_scale_up;
    public int timer_scale_up_max;

    void Start()
    {
        state = 0;
        float rand_scale = Random.Range(init_scale_min, init_scale_max);
        float rand_rot = Random.Range(init_rot_min, init_rot_max);

        transform.localScale = new Vector3(rand_scale, rand_scale, rand_scale);
        transform.Rotate(new Vector3(rand_rot, rand_rot, rand_rot));
    }


    void Update()
    {
        Scale_Change();
        Destroy();
    }

    // 大きさを変える
    void Scale_Change()
    {
        switch(state)
        {
            case 0:
                // 徐々に大きく
                transform.localScale -= new Vector3(scale_spd, scale_spd, scale_spd);

                if (timer_scale_up++ > timer_scale_up_max)
                {
                    state = 1;
                }
                break;
            case 1:
                // 徐々に小さく
                transform.localScale += new Vector3(scale_spd, scale_spd, scale_spd);
                break;
        }

    }

    // 壊れる条件
    void Destroy()
    {
        if (timer++ > timer_max)
        {
            Destroy(gameObject);
        }

        // 全部同じ比率やからどれかが一定の大きさになったら壊す
        if (transform.localScale.x < scale_min)
        {
            Destroy(gameObject);
        }
        if (transform.localScale.x > 3.1f)
        {
            Destroy(gameObject);
        }
    }

}
