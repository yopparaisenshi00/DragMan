using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShotController : MonoBehaviour
{
    private Vector3 apper;  // 自分の出現した位置
    private Vector3 pos;    // プレイヤーの位置
    private Vector3 vec;    // プレイヤーとの方向

    //public GameObject player;   // プレイヤー
    private GameObject player;

    public float spd;       // 速度

    private int timer;
    public int MaxTimer;    // 何秒たったら消すか
    public float MaxLen;    // どれだけ離れたら消すか

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        // 出現した位置を保存
        apper = transform.position;

        // プレイヤーの位置保存
        pos = player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        Destroy();
    }

    void MoveEnemy()
    {
        // プレイヤーがいた方向
        vec = (pos - apper).normalized;

        // vecの方向にspd進む
        move(vec, spd);
    }

    // ショット消す処理
    void Destroy()
    {
        timer += (int)(1.0f * Time.deltaTime);

        // 時間たったら壊す
        if (timer++ > MaxTimer)
        {
            Destroy(gameObject);
        }

        // 距離はなれたら壊す
        if (Vector3.Distance(apper, transform.position) > MaxLen)
        {
            Destroy(gameObject);
        }
    }

    // 追従する
    void move(Vector3 vec, float spd)
    {
        vec.y = 0;
        // 移動を設定
        transform.position += vec * (spd * Time.deltaTime);
    }

    // 当たった瞬間に呼び出される
    void OnTriggerEnter(Collider other)
    {
        // 当たったものの名前がPickUp
        if (other.gameObject.CompareTag("PLAYER"))
        {
            Destroy(gameObject);
        }
    }


}
