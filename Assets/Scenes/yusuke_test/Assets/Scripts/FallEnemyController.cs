using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FallEnemyController : MonoBehaviour
{

    private Vector3 pos;
    private Vector3 pivot_rotation;
    private Vector3 pivot_reverse;
    private float ANGLE_MAX = 270;
    private float ANGLE_MIN = 359;
    private Vector3 localAngle;

    private float init_angle_rotation;  // 初期速度
    public float angle_rotation;        // 回転の速さ
    public float add_angle_rotation;    // 慣性
    public float angle_reverse;         // 反転の速さ

    public int timer;       // 待機時間
    public int timer_max;

    public int state;

    enum ROTATION_STATE
    {
        ROTATION = 0,
        WAIT01,
        REVERSE,
        WAIT02,
    }

    ROTATION_STATE rot_state;

    // Start is called before the first frame update
    void Start()
    {
        // 初期垂直時の位置
        pos = pivot_reverse = transform.position;

        // 水平時の位置
        pivot_rotation = new Vector3(transform.position.x, 0, transform.position.z - transform.position.y);

        // 初期回転速度設定
        init_angle_rotation = angle_rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // 角度を取得
        localAngle = transform.localEulerAngles;

        // 動きまとめ
        Switch_Rotation();
    }

    void Switch_Rotation()
    {
        switch (state)
        {
            case (int)ROTATION_STATE.ROTATION:
                // 回転
                Rotation((int)ROTATION_STATE.WAIT01, (angle_rotation += add_angle_rotation), ANGLE_MAX);
                break;
            case (int)ROTATION_STATE.WAIT01:
                // 待機
                if (timer++ > timer_max)
                {
                    timer = 0;
                    state++;
                }
                break;
            case (int)ROTATION_STATE.REVERSE:
                // 反転
                Reverse((int)ROTATION_STATE.WAIT02, angle_reverse, ANGLE_MIN);
                break;
            case (int)ROTATION_STATE.WAIT02:
                // 待機
                if (timer++ > timer_max)
                {
                    timer = 0;
                    state = (int)ROTATION_STATE.ROTATION;
                }
                break;
        }
    }


    // 90度回転する
    void Rotation(int no, float angle, float angle_max)
    {
        // 回転制限(angle_max + angle * Time.deltaTimeはangleの速さが変わっても制御できるように)
        if (localAngle.x >= angle_max + angle * Time.deltaTime)
        {
            // どこを軸にして回すか
            Around(-angle);
        }
        else
        {
            // 速さを元に戻す
            angle_rotation = init_angle_rotation;

            // 位置固定
            transform.position = pivot_rotation;

            // 回転終わったら水平に固定
            Angles(angle_max);
            state = no;
        }
    }

    // 反転する
    void Reverse(int no, float angle, float angle_min)
    {
        // 回転制限(angle_max - angle * Time.deltaTimeはangleの速さが変わっても制御できるように)
        if (localAngle.x <= angle_min - angle * Time.deltaTime)
        {
            // どこを軸にして回すか
            Around(angle);
        }
        else
        {
            // 位置固定
            transform.position = pivot_reverse;

            // 回転終わったら直角に固定
            Angles(angle_min);
            state = no;
        }
    }

    // 軸をあわしながら回転させる
    void Around(float angle)
    {
        // x,zはオブジェクトの軸の位置に固定、yは0で固定,x軸でangle * Time.deltaTime分回転
        transform.RotateAround(new Vector3(pos.x, 0, pos.z), transform.right, angle * Time.deltaTime);
    }

    // 位置と角度を設定
    Vector3 Angles(float angle_max_or_min)
    {        
        // 角度を綺麗に固定するために直角か水平のangleを設定
        transform.localEulerAngles = new Vector3(angle_max_or_min, localAngle.y, localAngle.z);

        return transform.localEulerAngles;
    }

}


