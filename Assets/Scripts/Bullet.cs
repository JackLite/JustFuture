using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {

        Camera cam;
        private Rigidbody2D rigid;
        private bool active = false;

        public float angle;
        public float speed = 100f;
        public Vector3 force = new Vector3(0, 0);
        public float damage = 14f;

        // Use this for initialization
        void Start()
        {
            cam = GameObject.Find("MainCamera").GetComponent<Camera>();
            angle = transform.rotation.eulerAngles.z;
            rigid = GetComponent<Rigidbody2D>();
            rigid.AddForce(force.normalized * speed);
            active = true;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 viewPos = cam.WorldToScreenPoint(transform.position);
            bool isVisibleX = Screen.width >= viewPos.x;
            bool isVisibleY = Screen.height >= viewPos.y;

            if (!isVisibleX || !isVisibleY)
                Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy" && active)
            {
                active = false;
                collision.gameObject.GetComponent<Enemy>().SetDamage(this);
                Destroy(gameObject);
            }
        }
    }
}