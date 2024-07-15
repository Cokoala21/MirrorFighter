using System.Collections;
using UnityEngine;

public class DisplayCanvas : MonoBehaviour
{
    public GameObject canvasToDisplay; // Assign your canvas in the inspector

    void Start()
    {
        StartCoroutine(ShowCanvasForTime(20f)); // Show the canvas for 20 seconds
    }

    private IEnumerator ShowCanvasForTime(float duration)
    {
        canvasToDisplay.SetActive(true); // Show the canvas
        yield return new WaitForSeconds(duration); // Wait for the specified duration
        canvasToDisplay.SetActive(false); // Hide the canvas after the wait
    }
}
