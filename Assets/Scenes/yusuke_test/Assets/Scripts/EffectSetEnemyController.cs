using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSetEnemyController : MonoBehaviour
{
    private Rigidbody rb;

    // 出現させるエフェクト
    public GameObject effect;
    private int timer;          // エフェクトが出るまでの待機時間
    private bool on_effect;     // エフェクトを出す判定
    private bool apper_effect;  // エフェクトを出すときに実際にエフェクトが出る判定
    public float apper_time;    // エフェクトが出るまでの時間
    public int num;             // いっきに出すエフェクトの数

    public float spd;

    // どこからどこまでの範囲内に出すか
    public float range_max_x;
    public float range_min_x;
    public float range_max_y;
    public float range_min_y;
    public float range_max_z;
    public float range_min_z;

    void Start()
    {
        // Rigidbodyを付けてたら検出して返す
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            on_effect = true;
        }

        if (on_effect)
        {
            Effect();
        }

        on_effect = false;

    }

    void Effect()
    {
        if (apper_effect && on_effect)
        {
            // いっきにnum個のeffectを出す
            for (int i = 0; i < num; ++i)
            {
                Instantiate(effect,
                    new Vector3(
                        transform.position.x + Random.Range(range_min_x, range_max_x),
                        transform.position.y + Random.Range(range_min_y, range_max_y),
                        transform.position.z + Random.Range(range_min_z, range_max_z)) +
                        (transform.forward * -1),
                    effect.transform.rotation);
            }
            apper_effect = false;
        }

        if (timer++ > apper_time && !apper_effect)
        {
            apper_effect = true;
            timer = 0;
        }

    }

    // 物理演算を実行する直前に呼び出される
    void FixedUpdate()
    {
        // 水平軸と垂直軸からの入力を記録
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 移動する値を設定
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // 物理演算を使って動かす
        rb.AddForce(movement * spd);

    }
}
