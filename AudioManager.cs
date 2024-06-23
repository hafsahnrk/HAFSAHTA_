using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioSource backgroundMusic;

    public Button buttonOn;
    public Button buttonOff;

    private void Start()
    {
        if (backgroundMusic == null)
        {
            GameObject musicObject = GameObject.FindGameObjectWithTag("Music");
            if (musicObject != null)
            {
                backgroundMusic = musicObject.GetComponent<AudioSource>();
            }
            else
            {
                Debug.LogError("No GameObject with tag 'Music' found in the scene.");
            }
        }

        if (buttonOn == null || buttonOff == null)
        {
            Debug.LogError("Button On or Button Off is not assigned in the Inspector.");
        }
        else
        {
            // Debug log to check button assignments
            Debug.Log("Button On and Button Off are assigned.");
        }

        UpdateButtonStatus();
    }

    // untuk mengaktifkan/menonaktifkan backsound
    public void ToggleSound(bool isOn)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.mute = !isOn;
        }
        UpdateButtonStatus();
    }

    // untuk mengatur volume backsound
    public void AdjustVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = Mathf.Clamp(volume, 0f, 1f);
            Debug.Log("Volume set to: " + backgroundMusic.volume);
        }
    }

    // memperbarui status button sesuai backsound
    private void UpdateButtonStatus()
    {
        if (backgroundMusic != null)
        {
            bool isMuted = backgroundMusic.mute;
            if (buttonOn != null)
            {
                buttonOn.gameObject.SetActive(isMuted);
            }
            else
            {
                Debug.LogError("Button On is not assigned.");
            }

            if (buttonOff != null)
            {
                buttonOff.gameObject.SetActive(!isMuted);
            }
            else
            {
                Debug.LogError("Button Off is not assigned.");
            }
        }
        else
        {
            Debug.LogError("Background music is not assigned.");
        }
    }

    // memastikan status button diperbarui
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // memperbarui referensi backgroundMusic dan status button setiap ganti scene
        GameObject musicObject = GameObject.FindGameObjectWithTag("Music");
        if (musicObject != null)
        {
            backgroundMusic = musicObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Music' found in the scene after scene load.");
        }

        UpdateButtonStatus();
    }
}
