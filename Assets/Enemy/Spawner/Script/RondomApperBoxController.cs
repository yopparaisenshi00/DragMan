using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RondomApperBoxController : MonoBehaviour
{
    public GameObject box;      // 出す箱
    private bool apper_box;     // trueにしたら出現
    public int num;             // いっきに出す数
    private int timer;          // 待機時間
    public int timer_max;       // 何秒待つか指定

    // どの範囲に出すか
    public float range_max_x, range_min_x, range_max_z, range_min_z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Apper();
    }

    void Apper()
    {
        if (apper_box)
        {
            // いっきにnum個のeffectを出す
            for (int i = 0; i < num; ++i)
            {
                Instantiate(box,
                    new Vector3(
                        transform.position.x + Random.Range(range_min_x, range_max_x),
                        transform.position.y,
                        transform.position.z + Random.Range(range_min_z, range_max_z)),
                    transform.rotation);
            }
            apper_box = false;
        }

        if (timer++ > timer_max)
        {
            apper_box = true;
            timer = 0;
        }
    }

}
