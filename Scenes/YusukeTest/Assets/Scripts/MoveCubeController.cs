using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCubeController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 pos;

    public Text lenText;    // 距離を表示

    public float speed;     // 回転の速さ
    public float radius;    // 回転の半径

    public float spdX, spdY, spdZ;  // それぞれの速さ
    public int state;       // 状態

    private float len;      // 距離
    public float MaxLen;    // どこまで行くか

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbodyを付けてたら検出して返す
        rb = GetComponent<Rigidbody>();
        pos = transform.position;
    }

    void Update()
    {
        // 動きの種類
        Type();
        SetCountText();
    }

    void Len()
    {
        //len = transform.position.x - pos.x;
        //if (len > 6 || len < -6)
        //{
        //    spdX *= -1;
        //}

        //// 移動する値
        //float x = (spdX * Time.deltaTime);

        //move(x, 0, 0);
    }

    // テキストを何回も書かずにまとめるためのもの
    void SetCountText()
    {
        lenText.text = len.ToString();
    }


    // 物理演算を実行する直前に呼び出される
    void FixedUpdate()
    {
    }

    void Type()
    {
        switch (state)
        {
            // 円状
            case 0:
                Circle();
                break;
            // 左右
            case 1:
                LeftRight();
                break;
            // 上下
            case 2:
                UpDown();
                break;
            // 前後
            case 3:
                GoBack();
                break;
        }
    }

    void Circle()
    {
        // 円運動
        rb.MovePosition(
            new Vector3(
                radius * Mathf.Sin(Time.time * speed) + pos.x,
                pos.y,
                radius * Mathf.Cos(Time.time * speed) + pos.z)
        );
    }

    void LeftRight()
    {
        // 距離を初期位置からとる
        len = transform.position.x - pos.x;

        // 一定の距離になったら速度を反転させる
        if (len > MaxLen || len < -MaxLen)
        {
            spdX *= -1;
        }

        // 移動する値
        float x = (spdX * Time.deltaTime);

        // 移動設定
        move(x, 0, 0);
    }

    void UpDown()
    {
        // 距離を初期位置からとる
        len = transform.position.y - pos.y;

        // 一定の距離になったら速度を反転させる
        if (len > MaxLen || len < -MaxLen)
        {
            spdY *= -1;
        }

        // 移動する値
        float y = (spdY * Time.deltaTime);

        // 移動設定
        move(0, y, 0);
    }

    void GoBack()
    {
        // 距離を初期位置からとる
        len = transform.position.z - pos.z;

        // 一定の距離になったら速度を反転させる
        if (len > MaxLen || len < -MaxLen)
        {
            spdZ *= -1;
        }

        // 移動する値
        float z = (spdZ * Time.deltaTime);

        // 移動設定
        move(0, 0, z);
    }

    void move(float x, float y, float z)
    {
        // 移動を設定
        transform.position += new Vector3(x, y, z);
    }

}
