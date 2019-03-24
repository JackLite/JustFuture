using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Gun : MonoBehaviour
    {

        public GameObject bullet;
        public float bulletSpeed = 100;
        public float fireSpeed = 100;
        public Vector2 targetPoint;
        public AudioClip shootSound;

        private bool isBulletFire = false;
        private float realFireSpeed;
        private bool isNeedFire = false;
        private AudioSource mainAudioSource;

        // Use this for initialization
        void Start()
        {
            realFireSpeed = 100f / fireSpeed;
            mainAudioSource = GameObject.Find("MainAudio").GetComponent<AudioSource>();
        }

        public void startFire()
        {
            isNeedFire = true;
        }

        public void stopFire()
        {
            isNeedFire = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isNeedFire && !isBulletFire)
            {
                StartCoroutine(fire());
            }
        }

        IEnumerator fire()
        {
            isBulletFire = true;

            Vector3 mousePosScreen = Input.mousePosition;

            Vector3 spawnPointPosWorld = transform.Find("BulletSpawnPoint").transform.position;
            Vector3 spawnPointPosScreen = Camera.main.WorldToScreenPoint(spawnPointPosWorld);

            float distanceY = mousePosScreen.y - spawnPointPosScreen.y;
            float distanceX = mousePosScreen.x - spawnPointPosScreen.x;

            float angle = Mathf.Atan(distanceY / distanceX);
            angle = Mathf.Rad2Deg * angle;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 bulletForceVector = new Vector2(distanceX, distanceY);

            bullet.GetComponent<Bullet>().speed = bulletSpeed;
            bullet.GetComponent<Bullet>().force = bulletForceVector;

            Instantiate(bullet, transform.position, rotation, null);
            mainAudioSource.PlayOneShot(shootSound);
            yield return new WaitForSeconds(realFireSpeed);
            isBulletFire = false;
        }
    }
}