using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollisionManager : MonoBehaviour
{
    public AudioClip soundClip; // Assign your audio clip in the inspector
    private AudioSource audioSource;

    [Range(0f, 1f)] // Range attribute to limit volume between 0 and 1
    public float volume = 1f;

    public TextMeshProUGUI hitCountText; 
    public GameObject loseCanvas; // Assign your "You Lose" canvas in the inspector

    private int hitCount = 10;

    void Start()
    {
        // Add an AudioSource component if not already attached
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;
        audioSource.volume = volume;

        // Initialize the hit count text
        UpdateHitCountText();
        loseCanvas.SetActive(false); // Hide the lose canvas initially
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("EnemyCollider"))
        {
           
            Debug.Log("Hit taken!");

            // Play the sound
            audioSource.Play();

            hitCount--;
            UpdateHitCountText();

            // Check if hit count has reached zero
            if (hitCount <= 0)
            {
                ShowLoseCanvas();
            }
        }
    }

    private void UpdateHitCountText()
    {
        hitCountText.text = $"Enemy hits: {hitCount}";
    }

    private void ShowLoseCanvas()
    {
        loseCanvas.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        // Optionally, you can disable other UI elements or handle game over logic here.
    }

    // Optional: Method to restart the game or quit
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        // Add your scene loading or reset logic here
    }
}
