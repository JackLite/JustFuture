using System.Collections;
using UnityEngine;

namespace Game.Companions
{
    public class Companion : MonoBehaviour
    {
        private GameObject nearestEnemy;
        private bool isBulletFire;
        private float realFireSpeed;
        private float realAccuracy;
        private AudioSource mainAudioSource;
        protected Info info;

        public BulletPool bulletPool;
        public float bulletSpeed = 100f;
        public float fireSpeed = 100f;
        public float accuracy = 100f;
        public AudioClip shootSound;
        public float maxAccuracy = 100f;
        public float minAccuracy = 60f;

        private void Start()
        {
            realFireSpeed = 100f / fireSpeed;
            mainAudioSource = GameObject.Find("MainAudio").GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!LevelManager.Instance.IsGamePaused())
            {
                if (nearestEnemy != null && !isBulletFire)
                {
                    StartCoroutine(Fire());
                }
                else
                {
                    FindNearestEnemy();
                }
            }
        }

        private void FindNearestEnemy()
        {
            nearestEnemy = null;
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float nearestDistance = 0;
            foreach (var enemyObject in enemies)
            {
                var enemyPosition = enemyObject.GetComponent<Transform>().position;
                var distance = Vector3.Distance(transform.position, enemyPosition);
                
                if (nearestDistance != 0 && !(distance < nearestDistance)) continue;
                
                nearestDistance = distance;
                nearestEnemy = enemyObject;
            }
        }

        private IEnumerator Fire()
        {
            realFireSpeed = 100f / fireSpeed;
            realAccuracy = (maxAccuracy - Mathf.Clamp(accuracy, minAccuracy, maxAccuracy)) / maxAccuracy;

            isBulletFire = true;
            var bullet = bulletPool.GetBullet();
            var enemyPos = nearestEnemy.GetComponent<Transform>().position;

            var spawnPointPosWorld = transform.Find("Gun/BulletSpawnPoint").transform.position;

            var distanceY = enemyPos.y - spawnPointPosWorld.y;
            var distanceX = enemyPos.x - spawnPointPosWorld.x;
            distanceY = Random.Range(distanceY * (1f - realAccuracy), distanceY * (1f + realAccuracy));

            var angle = Mathf.Atan(distanceY / distanceX);
            angle = Mathf.Rad2Deg * angle;

            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 bulletForceVector = new Vector2(distanceX, distanceY);
            bullet.GetComponent<Transform>().position = spawnPointPosWorld;
            bullet.GetComponent<Transform>().rotation = rotation;
            bullet.GetComponent<Bullet>().speed = bulletSpeed;
            bullet.GetComponent<Bullet>().Fire(bulletForceVector);
            mainAudioSource.PlayOneShot(shootSound);
            yield return new WaitForSeconds(realFireSpeed);
            isBulletFire = false;
        }
    }
}