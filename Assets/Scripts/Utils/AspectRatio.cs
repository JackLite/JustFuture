using UnityEngine;

public class AspectRatio : MonoBehaviour {

    public float targetAspect;

    private void Start()
    {
        var windowAspect = Screen.width / Screen.height;
        var scaleHeight = windowAspect / targetAspect;
        var camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            camera.orthographicSize = camera.orthographicSize / scaleHeight;
        }
    }
}
