using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelProgressSlider : MonoBehaviour
    {

        private void Start()
        {
            GetComponent<Slider>().maxValue = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().enemyCount;
            GetComponent<Slider>().value = GetComponent<Slider>().maxValue;
        }
    }
}