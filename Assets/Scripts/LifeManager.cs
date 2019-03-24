using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{

    private Slider DPSlider;
    private Slider HPSlider;

    private void Start()
    {
        DPSlider = GameObject.Find("DPSlider").GetComponent<Slider>();
        HPSlider = GameObject.Find("HPSlider").GetComponent<Slider>();
    }

    public void applyDamage(float damage)
    {
        if (DPSlider.value > 0)
        {
            DPSlider.value -= damage;
        }
        else
        {
            HPSlider.value -= damage;
        }
    }
}
