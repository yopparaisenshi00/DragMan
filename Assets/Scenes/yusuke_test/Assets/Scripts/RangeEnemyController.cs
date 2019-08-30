using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeEnemyController : MonoBehaviour
{
    private GameObject player;

    private Vector3 apper;  // 出現した場所
    private Vector3 pos;    // 今の自分の場所

    public float BackLen;   // どこまで戻るか
    public float MaxLen;    // どれだけの範囲動くか
    public float FollowSpd; // 追うスピード
    public float BackSpd;   // 戻るスピード

    public Text FlagText;

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置保存
        apper = transform.position;

        // プレイヤーみつける
        player = GameObject.Find("Player");

        FlagText.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    // 敵の動きまとめ
    void MoveEnemy()
    {
        if (OnRange())  FollowPlayer();
        else            BackEnemy();
    }

    // プレイヤーを追う
    void FollowPlayer()
    {
        // 方向出す
        Vector3 vec = (player.transform.position - transform.position).normalized;
        vec.y = 0;
        move(vec, FollowSpd);
    }

    // もとの位置に戻る
    void BackEnemy()
    {
        // 最初にいた地点に近づいたら止まる
        if (Length(apper, transform.position) > BackLen)
        {
            // 方向出す
            Vector3 vec = (apper - transform.position).normalized;
            vec.y = 0;
            move(vec, BackSpd);
        }
    }

    // 範囲に入ったかどうか
    bool OnRange()
    {
        if (Length(apper, player.transform.position) < MaxLen)
        {
            FlagText.text = "true";
            return true;
        }
        else
        {
            FlagText.text = "false";
            return false;
        }
    }

    // 距離はかる
    float Length(Vector3 v1, Vector3 v2)
    {
        float l = Vector3.Distance(v1, v2);
        return l;
    }

    // 追従する
    void move(Vector3 vec, float spd)
    {
        vec.y = 0;
        // 移動を設定
        transform.position += vec * (spd * Time.deltaTime);
    }

}
