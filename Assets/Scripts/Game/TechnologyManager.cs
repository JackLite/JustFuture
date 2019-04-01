using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyManager : MonoBehaviour {

    private Text technologyText;
    private float techPoints;

    private void Start () {
        technologyText = GameObject.Find("TechnologyPoints").GetComponent<Text>();

    }
	
	public void AddTechnology(float points)
    {
        if (technologyText.IsDestroyed()) return;
        techPoints += points;
        technologyText.text = techPoints.ToString(CultureInfo.InvariantCulture);
    }
}
