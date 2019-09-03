using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomApperEnemyController : MonoBehaviour
{
    public GameObject enemy;      // 出す敵
    private GameObject[] tags;

    private bool apper_enemy;     // trueにしたら出現
    public int num;             // いっきに出す数

    public int enemy_min;   // 敵の下限

    // どの範囲に出すか
    public float range_max_x, range_min_x, range_max_z, range_min_z;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Apper_Enemy();
        Apper();
    }

    void Apper()
    {
        if (apper_enemy)
        {
            // いっきにnum個のeffectを出す
            for (int i = 0; i < num; ++i)
            {
                Instantiate(enemy,
                    new Vector3(
                        transform.position.x + Random.Range(range_min_x, range_max_x),
                        transform.position.y,
                        transform.position.z + Random.Range(range_min_z, range_max_z)),
                    enemy.transform.rotation);
            }
            apper_enemy = false;
        }

    }

    // 敵が少なくなって新しい敵を出現させる関数
    void Apper_Enemy()
    {
        // 敵の数をチェック
        Check("ApperEnemy");

        // 敵の数が少なくなったら新しい敵を出す
        if (tags.Length < enemy_min)
        {
            apper_enemy = true;
        }

    }

    // 敵が何体おるか調べる
    void Check(string name)
    {
        tags = GameObject.FindGameObjectsWithTag(name);
    }

    // 当たり判定
    //void OnTriggerEnter(Collider col)
    //{
    //    if(col.gameObject.tag == "PLAYER")
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //何かに当たった時
    //void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.gameObject.tag == "PLAYER")
    //    {
    //        Destroy(gameObject);
    //    }
    //}


}
