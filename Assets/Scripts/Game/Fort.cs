using UnityEngine;

namespace Game
{
	public class Fort : MonoBehaviour {

		// Use this for initialization
		private void Start () {
			var camera = Camera.main;
			var x = camera.ScreenToWorldPoint(new Vector3(0, 0)).x;
			transform.position = new Vector3(x, transform.position.y);
		}
	
		// Update is called once per frame
		private void Update () {
			var camera = Camera.main;
			var x = camera.ScreenToWorldPoint(new Vector3(0, 0)).x + 1.5f;
			transform.position = new Vector3(x, transform.position.y);
		}
	}
}