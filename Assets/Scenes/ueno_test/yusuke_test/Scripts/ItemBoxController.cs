using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxController : MonoBehaviour
{
    public Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotator();
        
    }

    void Rotator()
    {
        // 一定の角度で回転させる
        transform.Rotate(new Vector3(rot.x, rot.y, rot.z) * Time.deltaTime);
    }


}
