using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    上上がって降りる
*/

public class StepEnemyController : MonoBehaviour
{
    private Vector3 pos;    // 初期位置保存
    public Text len_text;   // 距離
    private Rigidbody rb;   // 物理演算使う

    private float spd;      // 設定速度
    public float add_spd;   // 慣性速度
    public float init_spd;  // 初期速度
    public float max_up;    // 空中
    public float max_down;  // 地面

    public int state;       // 状態

    private int timer;
    public int max_timer;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;       // 初期位置保存
        len_text.text = "";             // テキスト空
        rb = GetComponent<Rigidbody>(); // 物理演算用
    }

    // 物理演算を実行する直前に呼び出される
    void FixedUpdate()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        Force();
    }

    void Force()
    {
        // 距離を初期位置からとる
        float len = transform.position.y - pos.y;
        len_text.text = "距離 : " + len.ToString();

        /*      
            距離が地面についたらちょっと待機して上に上る
            上まで上りきったらちょっと待機して下がる
        */

        switch (state)
        {
            // 下に下がる
            case 0:
                spd += add_spd;                 // 慣性
                move(0, -Speed(), 0);           // 動く
                if (len < max_down) state = 1;  // 地面ついたら
                break;
            // 待機
            case 1:
                if (timer++ > max_timer)
                {
                    timer = 0;
                    state = 2;
                }
                break;
            // 上に上がる
            case 2:
                spd = init_spd;                 // 速度を設定
                move(0, Speed(), 0);            // 動く
                if (len > max_up) state = 3;    // 初期位置戻ったら
                break;
            // 待機
            case 3:
                if (timer++ > max_timer)
                {
                    timer = 0;
                    state = 0;
                }
                break;
        }
    }


    float Speed()
    {
        float s = (spd * Time.deltaTime);
        return s;
    }

    void move(float x, float y, float z)
    {
        // 移動を設定
        transform.position += new Vector3(x, y, z);
    }

}
