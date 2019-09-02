using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private Vector3 apper;  // 自分の出現した位置
    private Vector3 pos;    // プレイヤーの位置
    private Vector3 vec;    // プレイヤーとの方向

    public GameObject player;   // プレイヤー

    public float spd;       // 速度

    private int timer;
    public int MaxTimer;

    // Start is called before the first frame update
    void Start()
    {
        // 出現した位置を保存
        apper = transform.position;

        // プレイヤーの位置保存
        pos = player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        //Destroy();
    }

    void MoveEnemy()
    {
        // 出現した位置を保存
        apper = transform.position;

        // プレイヤーの位置保存
        pos = player.transform.position;

        // プレイヤーがいた方向
        vec = pos - apper;

        // vecの方向にspd進む
        move(vec.x, vec.y, vec.z);
    }

    void Destroy()
    {
        timer += (int)(1.0f * Time.deltaTime);
        if(timer++ > MaxTimer)
        {
            Destroy(gameObject);
        }
    }

    // 追従する
    void move(float x, float y, float z)
    {
        // 移動を設定
        transform.position += new Vector3(x, y, z);
    }

}
