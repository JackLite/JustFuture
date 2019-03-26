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
        private BulletPool bulletPool;

        public float speed = 100f;
        public float damage = 14f;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        // Use this for initialization
        void Start()
        {
            cam = Camera.main;
            bulletPool = GameObject.Find("BulletPool").GetComponent<BulletPool>();
            rigid = GetComponent<Rigidbody2D>();
        }

        public void Fire(Vector2 force)
        {
            gameObject.SetActive(true);
            rigid.velocity = force.normalized * speed;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 viewPos = cam.WorldToScreenPoint(transform.position);
            bool isVisibleX = Screen.width >= viewPos.x;
            bool isVisibleY = Screen.height >= viewPos.y;

            if (gameObject.activeSelf && (!isVisibleX || !isVisibleY))
            {
                bulletPool.RemoveBullet(gameObject);

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && gameObject.activeSelf)
            {
                collision.gameObject.GetComponent<Enemy>().SetDamage(this);
                bulletPool.RemoveBullet(gameObject);
            }
        }
    }
}