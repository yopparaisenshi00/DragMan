using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemyController : MonoBehaviour
{

    public GameObject shot;
    public int num;

    IEnumerator Start()
    {
        for (int i = 0; i < num; ++i)
        {
            Instantiate(shot, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
