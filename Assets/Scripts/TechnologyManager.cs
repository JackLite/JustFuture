using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyManager : MonoBehaviour {

    private Text technologyText;
    private float techPoints = 0f;
	void Start () {
        technologyText = GameObject.Find("TechnologyPoints").GetComponent<Text>();

    }
	
	public void addTechnology(float points)
    {
        if (technologyText.IsDestroyed()) return;
        techPoints += points;
        technologyText.text = techPoints.ToString();
    }
}
