using UnityEngine;

public class Fort : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera camera = Camera.main;
        float x = camera.ScreenToWorldPoint(new Vector3(0, 0)).x;
        transform.position = new Vector3(x, transform.position.y);
    }
	
	// Update is called once per frame
	void Update () {
        Camera camera = Camera.main;
        float x = camera.ScreenToWorldPoint(new Vector3(0, 0)).x + 1.5f;
        transform.position = new Vector3(x, transform.position.y);
    }
}
