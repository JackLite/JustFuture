using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelProgressManager : MonoBehaviour
    {

        private Slider levelProgressSlider;

        private void Start()
        {
            levelProgressSlider = GameObject.Find("LevelProgressSlider").GetComponent<Slider>();
            int enemyCount = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().enemyCount;
            levelProgressSlider.maxValue = enemyCount;
            levelProgressSlider.value = enemyCount;
        }

        public void Decrease()
        {
            levelProgressSlider.value--;
        }
    }
}