﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemyController : MonoBehaviour
{

    public GameObject shot;
    public int num;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        for (int i = 0; i < num; ++i)
        {
            Instantiate(shot, transform.position, transform.rotation);
            yield return new WaitForSeconds(1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}