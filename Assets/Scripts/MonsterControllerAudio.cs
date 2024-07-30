using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControllerAudio : MonoBehaviour
{
    public AudioSource alchemySuccess;
    public AudioSource alchemyFailed;

    public void PlaySuccessSound()
    {
        if (alchemySuccess != null)
        {
            alchemySuccess.Play();
        }
    }

    public void PlayFailedSound()
    {
        if (alchemyFailed != null)
        {
            alchemyFailed.Play();
        }
    }
}
