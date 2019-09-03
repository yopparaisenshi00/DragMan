using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class wada_test_player : MonoBehaviour
    {

        public GameObject camera_tr;
        class player_config
        {
            public float x;
            public float y;
            public float z;

            public float yaw;
            public float pitch;
            public float roll;


            public void SetFromTransform(Transform t)
            {
                pitch = t.eulerAngles.x;
                yaw = t.eulerAngles.y;
                roll = t.eulerAngles.z;
                x = t.position.x;
                y = t.position.y;
                z = t.position.z;
            }


            public void Translate(Vector3 translation)
            {
                Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;

                x += rotatedTranslation.x;
                y += rotatedTranslation.y;
                z += rotatedTranslation.z;
            }

            public void LerpTowards(player_config target, float positionLerpPct, float rotationLerpPct)
            {
                yaw   = Mathf.Lerp(yaw,   target.yaw,   rotationLerpPct);
                pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
                roll  = Mathf.Lerp(roll,  target.roll,  rotationLerpPct);

                x = Mathf.Lerp(x, target.x, positionLerpPct);
                y = Mathf.Lerp(y, target.y, positionLerpPct);
                z = Mathf.Lerp(z, target.z, positionLerpPct);
            }

            public void UpdateTransform(Transform t)
            {
                t.eulerAngles = new Vector3(pitch, yaw, roll);
                t.position = new Vector3(x, y, z);
            }

        }

        private player_config Config = new player_config();
        private Rigidbody RigidbodyCom;

        // Start is called before the first frame update
        void Start()
        {
            RigidbodyCom = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            // Translation
            var translation = GetInputTranslationDirection() * Time.deltaTime;

            // Speed up movement when shift key held
            if (Input.GetKey(KeyCode.LeftShift))
            {
                translation *= 10.0f;
            }

            Config.Translate(translation);

            // Speed up movement when shift key held
            if (Input.GetKey(KeyCode.LeftShift))
            {
                translation *= 10.0f;
            }


            translation *= 10;
            Config.UpdateTransform(transform);
        }


        Vector3 GetInputTranslationDirection()
        {

        Transform tr = camera_tr.transform;
            Vector3 direction = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += -Vector3.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += -Vector3.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                direction += -Vector3.up;
            }
            if (Input.GetKey(KeyCode.E))
            {
                direction += Vector3.up;
            }
            return direction;
        }

    //Vector3 GetInputTranslationDirection()
    //{

    //    Transform tr = camera_tr.transform;
    //    Vector3 direction = new Vector3();
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        direction += tr.forward;
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        direction += -tr.forward;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        direction += -tr.right;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        direction += tr.right;
    //    }
    //    if (Input.GetKey(KeyCode.Q))
    //    {
    //        direction += -tr.up;
    //    }
    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        direction += tr.up;
    //    }
    //    return direction;
    //}

}
