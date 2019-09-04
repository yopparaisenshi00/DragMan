using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffectController : MonoBehaviour
{
    private Color init_col;

    private Vector3 front;

    // エフェクトが動く速さ
    public float spd;
    public float scale_min; // エフェクトの下限

    // 大きさの範囲
    public float init_scale_max;
    public float init_scale_min;

    // 角度の範囲
    public float init_rot_max;
    public float init_rot_min;

    private int state;              // 状態
    private int timer_scale_up;     // 拡大する時間
    public int timer_scale_up_max;  // 何秒拡大するか
    public float scale_spd;         // エフェクトが小さくなる速さ  

    // 透明になる速さ
    public float alpha_spd;  

    // 待機時間
    private int timer;
    public int timer_max;

    // Start is called before the first frame update
    void Start()
    {
        // モデルの色
        init_col = gameObject.GetComponent<MeshRenderer>().material.color;

        // 初期の大きさをランダムにする
        float rand_scale = Random.Range(init_scale_min, init_scale_max);
        transform.localScale = new Vector3(rand_scale, rand_scale, rand_scale);

        // 初期の角度
        float rand_rot = Random.Range(init_rot_min, init_rot_max);
        float rand_x_rot = Random.Range(0, 180);
        transform.Rotate(new Vector3(rand_x_rot, rand_rot, rand_rot));

        // 角度を保存
        front = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Scale_Change();        
        Destroy();
    }

    void Move()
    {
        // 飛ばす
        transform.position -= front * (spd * Time.deltaTime);
    }

    void Scale_Change()
    {
        switch (state)
        {
            case 0:
                // 徐々に大きく
                transform.localScale -= new Vector3(scale_spd, scale_spd, scale_spd);

                // 拡大終了
                if (timer_scale_up++ > timer_scale_up_max) state = 1;
                break;
            case 1:
                // 徐々に小さく
                transform.localScale += new Vector3(scale_spd, scale_spd, scale_spd);
                alpha_change();
                break;
        }
    }

    // 徐々に透明にする
    void alpha_change()
    {
        // 徐々に透明にする
        init_col.a -= alpha_spd;
        gameObject.GetComponent<MeshRenderer>().material.color = init_col;
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
        if (transform.localScale.x > 4.1f)
        {
            Destroy(gameObject);
        }

        // 透明になったら消す
        if(init_col.a < 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
