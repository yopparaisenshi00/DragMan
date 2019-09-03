using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject chara;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        chara.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime, Space.World);
    }
}
