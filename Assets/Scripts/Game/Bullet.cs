using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private Camera cam;
        private Rigidbody2D rigid;
        private BulletPool bulletPool;

        public float speed = 100f;
        public float damage = 14f;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        // Use this for initialization
        private void Start()
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

        private void Update()
        {
            var viewPos = cam.WorldToScreenPoint(transform.position);
            var isVisibleX = Screen.width >= viewPos.x;
            var isVisibleY = Screen.height >= viewPos.y;

            if (gameObject.activeSelf && (!isVisibleX || !isVisibleY))
            {
                bulletPool.RemoveBullet(gameObject);

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Enemy") || !gameObject.activeSelf) return;
            
            collision.gameObject.GetComponent<Enemy>().SetDamage(this);
            bulletPool.RemoveBullet(gameObject);
        }
    }
}