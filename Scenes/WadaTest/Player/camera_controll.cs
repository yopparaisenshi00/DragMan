using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scenes.WadaTest.Player
{
    class camera_controll : MonoBehaviour
    {


        public  GameObject target;   // 参照物
        
        //public  GameObject target2;   // 参照物2 (test)

        public GameObject current_target;   // 現参照


        //private Vector3 target_pos;
        private Rigidbody rb;
        private Camera camera;

        //private Quaternion direction;
        //private Vector3 direction;

        Quaternion offset_direction;

        public float move_side_speed = 70.0f;
        public float min_distance = 0.2f;
        public float max_distance = 8.0f;
        //[SerializeField]
        //private Vector3 offset = new Vector3(0,1,0);

        // Start is called before the first frame update

        //void set_target(GameObject target) {
        //    if (target == null)
        //    {
        //        Debug.Log(name + ": don't have target object");
        //        return;
        //    }
        //    else {
        //        Debug.Log(name + ": set target\" " + target.name+ " \"");
        //    }
        //    offset_direction = target.transform.rotation;
        //}
        void Start()
        {
            camera = GetComponent<Camera>();
            if (camera != null) {
                Debug.Log(name + ":Get Component Camera");
            }

            rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log(name + ":Get Component Rigidbody");
            }


            current_target = target;

            //if (target1) {
            //    set_target(target1);
            //}


        }

        // Update is called once per frame
        void Update()
        {
        }

        // 全てのモノがUpdate()で処理された後に実行される
        bool chenge_target;
        void LateUpdate()
        {



            Vector3 move_rotete;
            Vector3 move_position;

            float side_move = get_input_side();
            // 自身
            Vector3 m_pos = transform.position;
            // ターゲット
            Vector3 t_pos = current_target.transform.position;

            Vector3 target_dir = (t_pos - transform.position);

            // 回転
            {
                get_input_side();
                move_rotete = Vector3.RotateTowards(transform.forward, target_dir, 12 * Time.deltaTime, 0f);

                //ターゲットの少し上を見る
                if (Input.GetKey(KeyCode.O))
                {
                    t_pos.y += 4.0f;
                }
            }


            // 移動
            {

                move_position = transform.position;

                float dir = Vector3.Distance(t_pos, m_pos);
                Vector3 normal = Vector3.Normalize(m_pos - t_pos);
                move_position.y = t_pos.y + 1.5f;
                


                //rb.
                //move_position = Vector3.MoveTowards(m_pos,normal,6);
                //Vector3 = transform.TransformDirection(Vector3.up);
            }


            //// TEST //
            ////move();
            //// TEST //
            //Vector3 axis = transform.TransformDirection(Vector3.up);
            //transform.RotateAround(t_pos, axis, side_move * Time.deltaTime);
            


            //rb.MoveRotation(Quaternion.LookRotation(move_rotete));
            //rb.MovePosition(move_position);

            transform.LookAt(target.transform);
            Vector3 axis = Vector3.up;
            transform.RotateAround(t_pos, axis, side_move * Time.deltaTime);

        }

        // 横移動 //
        public float get_input_side() {

            float dir = 0;
            if (Input.GetKey(KeyCode.L))
            {
                dir += move_side_speed;
            }
            if (Input.GetKey(KeyCode.J))
            {
                dir += -move_side_speed;
            }
            return dir;
        }

        

        /// <移動> /////////////////////////////////////////
        public void move() {
            Vector3 move_direction = get_input_translation_direction();

            //move_direction = move_direction * transform;
            rb.MovePosition(transform.position + move_direction);
        }
        Vector3 get_input_translation_direction()
        {
            Vector3 direction = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                direction += transform.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += -transform.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += -transform.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += transform.right;
            }
            if (Input.GetKey(KeyCode.E))
            {
                direction += -transform.up;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                direction += transform.up;
            }
            return direction;
        }



        //void RotationZXY(float x, float y, float z)
        //{
        //    float sx, sy, sz, cx, cy, cz;

        //    sx = Mathf.Sin(x); sy = Mathf.Sin(y); sz = Mathf.Sin(z);
        //    cx = Mathf.Cos(x); cy = Mathf.Cos(y); cz = Mathf.Cos(z);

        //    _11 = cz * cy + sz * sx * sy;
        //    _12 = sz * cx;
        //    _13 = -cz * sy + sz * sx * cy;
        //    _14 = 0;

        //    _21 = -sz * cy + cz * sx * sy;
        //    _22 = cz * cx;
        //    _23 = sz * sy + cz * sx * cy;
        //    _24 = 0;

        //    _31 = cx * sy;
        //    _32 = -sx;
        //    _33 = cx * cy;
        //    _34 = 0;

        //    _41 = 0;
        //    _42 = 0;
        //    _43 = 0;
        //    _44 = 1;
        //}
    }

 
}