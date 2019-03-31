using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{

    private Slider dpSlider;
    private Slider hpSlider;

    private void Start()
    {
        dpSlider = GameObject.Find("DPSlider").GetComponent<Slider>();
        hpSlider = GameObject.Find("HPSlider").GetComponent<Slider>();
    }

    public void ApplyDamage(float damage)
    {
        if (dpSlider.value > 0)
        {
            dpSlider.value -= damage;
        }
        else
        {
            hpSlider.value -= damage;
        }
    }
}
