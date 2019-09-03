using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSetController : MonoBehaviour
{
    public GameObject effect;

    public int num;             // いっきに出すエフェクトの数

    // どこからどこまでの範囲内に出すか
    public float range_max_x;
    public float range_min_x;
    public float range_max_y;
    public float range_min_y;
    public float range_max_z;
    public float range_min_z;

    // Start is called before the first frame update
    void Start()
    {
        // エフェクト
        Apper_Effect();
    }

    void Update()
    {
    }

    // アイテム取ったときのエフェクト
    void Apper_Effect()
    {
        // いっきにnum個のeffectを出す
        for (int i = 0; i < num; ++i)
        {
            // 生成する物体、生成場所、回転軸の設定
            Instantiate(effect,
                new Vector3(
                    transform.position.x + Random.Range(range_min_x, range_max_x),
                    transform.position.y + Random.Range(range_min_y, range_max_y),
                    transform.position.z + Random.Range(range_min_z, range_max_z)),
                effect.transform.rotation);
        }
    }

}
