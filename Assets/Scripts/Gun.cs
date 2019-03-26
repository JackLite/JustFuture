using System.Collections;
using UnityEngine;

namespace Game
{
    public class Gun : MonoBehaviour
    {

        //public GameObject bullet;
        public float bulletSpeed = 100;
        public float fireSpeed = 100;
        public AudioClip shootSound;
        public BulletPool bulletPool;

        private bool isBulletFire;
        private float realFireSpeed;
        private bool isNeedFire;
        private AudioSource mainAudioSource;

        // Use this for initialization
        void Start()
        {
            realFireSpeed = 100f / fireSpeed;
            mainAudioSource = GameObject.Find("MainAudio").GetComponent<AudioSource>();
        }

        public void StartFire()
        {
            isNeedFire = true;
        }

        public void StopFire()
        {
            isNeedFire = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (isNeedFire && !isBulletFire)
            {
                StartCoroutine(Fire());
            }
        }

        private IEnumerator Fire()
        {
            isBulletFire = true;

            // получаем объект пули из пула
            var bullet = bulletPool.GetBullet();

            // угол относительно точки появления
            var angle = GetAngle();

            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // расстояние до точки клика
            Vector3 bulletForceVector = GetDistanceToPointer();

            bullet.GetComponent<Bullet>().speed = bulletSpeed;
            bullet.GetComponent<Transform>().position = transform.position;
            bullet.GetComponent<Transform>().rotation = rotation;
            
            bullet.GetComponent<Bullet>().Fire(bulletForceVector);
            
            mainAudioSource.PlayOneShot(shootSound);
            yield return new WaitForSeconds(realFireSpeed);
            isBulletFire = false;
        }

        private float GetAngle()
        {

            var distance = GetDistanceToPointer();
            var angle = Mathf.Atan(distance.y / distance.x);
            return Mathf.Rad2Deg * angle;
        }

        private Vector2 GetDistanceToPointer()
        {
            var mousePosScreen = Input.mousePosition;

            var spawnPointPosWorld = transform.Find("BulletSpawnPoint").transform.position;
            var spawnPointPosScreen = Camera.main.WorldToScreenPoint(spawnPointPosWorld);

            var distanceY = mousePosScreen.y - spawnPointPosScreen.y;
            var distanceX = mousePosScreen.x - spawnPointPosScreen.x;
            
            return new Vector2(distanceX, distanceY);
        }
    }
}