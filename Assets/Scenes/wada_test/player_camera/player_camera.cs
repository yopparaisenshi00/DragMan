using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_camera : MonoBehaviour
{

    public GameObject follow_target;
    public Vector3    follow_offset;

    public GameObject look_target;
    public Vector3    look_offset;

    public float zoom_max = 1;
    public float zoom_min = 30;
    public float horizontally_roll_min;
    public float horizontally_roll_max;


    public GameObject camera;


    public float distance = 8;
    public float vertically_roll_speed = 100;
    public float horizontally_roll_speed = 80;
    public float zoom_speed = 1;

    public float distance_min;
    public float distance_max;

    //private Quaternion rotete;


    private float yaw;  //横
    private float pitch;//高さ
    private float roll; //
    

    //private Quaternion rotate = new Quaternion(0,0,0,0);
    public LayerMask mask;
    //private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
       // rb = GetComponent<Rigidbody>();
    }

    void LateUpdate() {

        //free();
        //return;
        //direction update // 
        //Vector3 dir = get_imput_direction();

        //dir.x *= Mathf.Deg2Rad;
        //dir.y *= Mathf.Deg2Rad;
        //dir.z *= Mathf.Deg2Rad;

        //direction.x += dir.x;
        //direction.y += dir.y;
        //direction.z += dir.z;
        //direction.Normalize();

        //float c1 = Mathf.Cos(direction.x * 0.5f);
        //float c2 = Mathf.Cos(direction.y * 0.5f);
        //float c3 = Mathf.Cos(direction.z * 0.5f);

        //float s1 = Mathf.Sin(direction.x * 0.5f);
        //float s2 = Mathf.Sin(direction.y * 0.5f);
        //float s3 = Mathf.Sin(direction.z * 0.5f);

        //// Unity's order is 'YXZ'
        //float qx = s1 * c2 * c3 + c1 * s2 * s3;
        //float qy = c1 * s2 * c3 - s1 * c2 * s3;
        //float qz = c1 * c2 * s3 - s1 * s2 * c3;
        //float qw = c1 * c2 * c3 + s1 * s2 * s3;

        //Quaternion q = new Quaternion(qx, qy, qz, qw);
        //Vector3 t = q.eulerAngles;
        ////Debug.Log(q.eulerAngles);
        //Debug.Log(t);

        // ターゲット //
        Vector3 pos = follow_target.transform.position + follow_offset;


        // 回転作成 //
        Vector3 dir = get_imput_direction();
        //dir.x *= Mathf.Deg2Rad;
        //dir.y *= Mathf.Deg2Rad;
        //dir.z *= Mathf.Deg2Rad;

        pitch   += dir.x * vertically_roll_speed * Time.deltaTime;
        yaw     += dir.y * horizontally_roll_speed * Time.deltaTime;
        //roll  += dir.z * 100 * Time.deltaTime;
        transform.rotation = new Quaternion(0, 0, 0, 1);
        transform.Rotate(pitch, yaw, roll);

        // 距離     //
        zoom(dir.z);


        // camera座標算出
        Transform t = camera.transform;
        {
            Ray ray = new Ray(pos,transform.forward*distance);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance, mask))
            {
                t.transform.position = hit.point;
                //distance = hit.distance;
                Debug.DrawLine(pos, hit.point, Color.red);
            }
            else
            {
                t.transform.position = pos + (transform.forward * distance);
                Debug.DrawLine(pos, transform.position, Color.red);
                Debug.DrawRay(pos ,transform.forward , Color.red);
            }
        }

        // camera_update
        //transform.position = pos;
        //direction + dir;
        //Debug.DrawRay(target.transform.position, direction, Color.red);
        transform.position = pos;
        camera.transform.LookAt(look_target.transform.position + look_offset);
    }

    void OnGUI() {
        string text = 
                      "yaw : " + yaw + "\n" +
                      "pitch : " + pitch + "\n" +
                      "roll : " + roll + "\n";
        GUI.TextArea(new Rect(0, 0, 200, 200), text);

    }

    //void free() {
    //    Vector3 dir = get_imput_direction();

    //    dir.x *= Mathf.Deg2Rad;
    //    dir.y *= Mathf.Deg2Rad;
    //    dir.z *= Mathf.Deg2Rad;

    //    direction.x += dir.x;
    //    direction.y += dir.y;
    //    direction.z += dir.z;
    //    //direction.Normalize();

    //    float c1 = Mathf.Cos(direction.x * 0.5f);
    //    float c2 = Mathf.Cos(direction.y * 0.5f);
    //    float c3 = Mathf.Cos(direction.z * 0.5f);

    //    float s1 = Mathf.Sin(direction.x * 0.5f);
    //    float s2 = Mathf.Sin(direction.y * 0.5f);
    //    float s3 = Mathf.Sin(direction.z * 0.5f);

    //    // Unity's order is 'YXZ'
    //    float qx = s1 * c2 * c3 + c1 * s2 * s3;
    //    float qy = c1 * s2 * c3 - s1 * c2 * s3;
    //    float qz = c1 * c2 * s3 - s1 * s2 * c3;
    //    float qw = c1 * c2 * c3 + s1 * s2 * s3;

    //    transform.rotation = new Quaternion(qx, qy, qz, qw);

    //}

    private Vector3 get_imput_direction()
    {
        Vector3 dir = new Vector3(0,0,0);
        if (Input.GetKey(KeyCode.J)) {
            dir += Vector3.up;
        }
        if (Input.GetKey(KeyCode.L))
        {
            dir -= Vector3.up;
        }
        if (Input.GetKey(KeyCode.I))
        {
            dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.K))
        {
            dir -= Vector3.left;
        }
        if (Input.GetKey(KeyCode.O))
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.U))
        {
            dir -= Vector3.forward;
        }

        return dir;
    }

    void zoom(float val)
    {
        distance += val * zoom_speed * Time.deltaTime;
    }


    // Update is called once per frame
    void Update()
    {
    
    }


    
}
