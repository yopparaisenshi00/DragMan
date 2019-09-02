using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCubeController : MonoBehaviour
{
    public float angle;     // 回転の速さ
    public float MaxSpd;    // 回転の最大速度
    public float InitSpd;   // 初期回転の速度
    public int timer;       // どれだけの時間回すか
    public int MaxTimer;    // 待ち時間
    public bool OnRotate;   // まわってるかどうか
    public float ConstantAngle;

    public int state;       // 状態

    public int add;

    // Start is called before the first frame update
    void Start()
    {
        //state = 0;
        angle = InitSpd;
    }

    // Update is called once per frame
    void Update()
    {
        Type();
    }

    void Type()
    {
        switch (state)
        {
                // 常にまわる
            case 0:
                AlwaysRot();
                break;
                // 一定の時間たったら何度か回転
            case 1:
                Constant();
                break;
                // 徐々に早くなっていく
            case 2:
                Gradually();
                break;
        }
    }

    // 常にまわる
    void AlwaysRot()
    {
        Rotator(angle);
    }

    // 一定の時間たったら何度か回転
    void Constant()
    {

        // フラグがonになったら回す
        if (OnRotate)
        {
            //Rotator(ConstantAngle);
            // ConstantAngle分かくかくに動く
            transform.Rotate(new Vector3(0, ConstantAngle, 0));
            OnRotate = false;
            //if (timer++ > ConstantAngle)
            //{
            //    timer = 0;
            //    OnRotate = false;
            //}
        }
        // 待ち時間
        else if (timer++ > MaxTimer)
        {
            OnRotate = true;
            timer = 0;
        }

    }

    // 徐々に早くなっていく
    void Gradually()
    {
        angle += add;
        // 回転速くなったら初期に
        if (angle > MaxSpd)
        {
            angle = MaxSpd;
            // 何秒最大速度で回すか
            if (timer++ > MaxTimer)
            {
                angle = InitSpd;
                timer = 0;
            }
        }
        Rotator(angle);

    }

    void Rotator(float angle)
    {
        transform.Rotate(new Vector3(0, angle, 0) * Time.deltaTime);
    }



}
