using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroyer : MonoBehaviour
{
    private int timer;
    public int timer_max;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy();
    }

    void Destroy()
    {
        if(timer++ > timer_max)
        {
            Destroy(gameObject);
            timer = 0;
        }
    }

}
