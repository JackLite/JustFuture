using System.Collections;
using UnityEngine;

namespace Game.Companions
{
    abstract public class Companion : MonoBehaviour
    {
        private GameObject nearestEnemy;
        private bool isBulletFire = false;
        private float realFireSpeed;
        private float maxAccuracy = 100f;
        private float minAccuracy = 60f;
        private float realAccuracy;
        private AudioSource mainAudioSource;
        protected Info info;

        public BulletPool bulletPool;
        public float bulletSpeed = 100f;
        public float fireSpeed = 100f;
        public float damage = 10f;
        public float accuracy = 100f;
        public AudioClip shootSound;

        public enum Type
        {
            Attack,
            Defense,
            Help,
            Resource
        }

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

        abstract public Info GetInfo();

        private void FindNearestEnemy()
        {
            nearestEnemy = null;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float nearestDistance = 0;
            foreach (GameObject enemyObject in enemies)
            {
                Vector3 enemyPosition = enemyObject.GetComponent<Transform>().position;
                float distance = Vector3.Distance(transform.position, enemyPosition);
                if (nearestDistance == 0 || distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemyObject;
                }
            }
        }

        private IEnumerator Fire()
        {
            realFireSpeed = 100f / fireSpeed;
            realAccuracy = (maxAccuracy - Mathf.Clamp(accuracy, minAccuracy, maxAccuracy)) / maxAccuracy;

            isBulletFire = true;
            var bullet = bulletPool.GetBullet();
            Vector3 enemyPos = nearestEnemy.GetComponent<Transform>().position;

            Vector3 spawnPointPosWorld = transform.Find("Gun/BulletSpawnPoint").transform.position;

            float distanceY = enemyPos.y - spawnPointPosWorld.y;
            float distanceX = enemyPos.x - spawnPointPosWorld.x;
            distanceY = Random.Range(distanceY * (1f - realAccuracy), distanceY * (1f + realAccuracy));

            float angle = Mathf.Atan(distanceY / distanceX);
            angle = Mathf.Rad2Deg * angle;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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