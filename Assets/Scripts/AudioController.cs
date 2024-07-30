using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource defaultClickAudioSource;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button down detected.");
            HandleMouseClick();
        }
    }

    void HandleMouseClick()
    {
        // Getting the mouse click position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Mouse position: " + mousePosition);

        // Performing a raycast at the mouse position
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        Debug.Log("Raycast hit: " + (hit.collider != null));

        // if (hit.collider != null)
        // {
            PlayDefaultClickSound();
        // }
    }

    public void PlayDefaultClickSound()
    {
        if (defaultClickAudioSource != null)
        {
            if (defaultClickAudioSource.clip != null)
            {
                Debug.Log("Playing click sound.");
                defaultClickAudioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource has no AudioClip assigned.");
            }
        }
        else
        {
            Debug.LogWarning("defaultClickAudioSource is not assigned.");
        }

    }
}
