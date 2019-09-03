using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectController : MonoBehaviour
{
    public GameObject item_effect;

    // どこからどこまでの範囲内に出すか
    public float range_max_x;
    public float range_min_x;
    public float range_max_y;
    public float range_min_y;
    public float range_max_z;
    public float range_min_z;

    private bool item_hit;  // アイテムに当たった判定
    public int item_effect_num = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (item_hit) Item_Put_Effect();
    }

    void Item_Put_Effect()
    {
        // いっきにnum個のeffectを出す
        for (int i = 0; i < item_effect_num; ++i)
        {
            // 生成する物体、生成場所、回転軸の設定
            Instantiate(item_effect,
                new Vector3(
                    transform.position.x + Random.Range(range_min_x, range_max_x),
                    transform.position.y + Random.Range(range_min_y, range_max_y),
                    transform.position.z + Random.Range(range_min_z, range_max_z)),
                item_effect.transform.rotation);
        }
        item_hit = false;
    }

    //何かに当たった時
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Item_Hit(hit); // アイテムと
    }

    // アイテムと当たった時
    void Item_Hit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Item")
        {
            item_hit = true;
            Destroy(hit.gameObject);
        }
    }




}
