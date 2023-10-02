using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private Text fpsText;
    private int fps;

    private void Start()
    {
        fpsText = GetComponent<Text>();
        StartCoroutine(CountFPS());
    }

    private void Update()
    {
        fps++;
    }

    private IEnumerator CountFPS()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            fpsText.text = $"FPS: {fps}";

            fps = 0;
        }
    }
}
