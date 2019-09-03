using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 pos;

    private int up;

    public float radius;

    public float spd, angle;
    public int state;

    // Start is called before the first frame update
    void Start()
    {
        up = 3;
        rb = GetComponent<Rigidbody>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Type();
    }

    void Type()
    {
        switch (state)
        {
            case 0:
                Circle();
                break;
            case 1:
                CircleRot();
                break;
        }
    }

    void Circle()
    {
        // 円運動
        pos.x = radius * Mathf.Sin(Time.time * spd);
        pos.y = radius * Mathf.Cos(Time.time * spd);

        transform.position = new Vector3(pos.x, pos.y + up, pos.z);
    }

    void CircleRot()
    {
        // 円運動
        pos.x = radius * Mathf.Sin(Time.time * spd);
        pos.y = radius * Mathf.Cos(Time.time * spd);

        transform.position = new Vector3(pos.x, pos.y + up, pos.z);
        Rotator(-angle);
    }

    void Rotator(float angle)
    {
        transform.Rotate(new Vector3(0, 0, angle) * Time.deltaTime);
    }


}
