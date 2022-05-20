using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private AudioSource footstep;
    [SerializeField]
    private AudioClip grass, wood;
    [SerializeField]
    private AudioSource voice;
    [SerializeField]
    private AudioClip jump1, jump2, defeat;

    public void Footstep()
    {
        footstep.volume = GameManager.sfxVolume;
        footstep.Play();
    }

    public void Stop()
    {
        footstep.Stop();
    }
    
    public void SetOnGrass(bool onGrass)
    {
        if (footstep.isPlaying) return;

        if (onGrass)
        {
            footstep.clip = grass;
        }
        else
        {
            footstep.clip = wood;
        }
    }

    public void Defeat()
    {
        voice.volume = GameManager.sfxVolume;
        voice.PlayOneShot(defeat);
    }

    public void Jump()
    {
        voice.volume = GameManager.sfxVolume;
        if (!voice.isPlaying) {
            int rand = Random.Range(0, 2);
            voice.PlayOneShot(rand == 0 ? jump1 : jump2);
        }
    }
    
    public void Retry()
    {
        GameManager.instance.Retry();
    }

}
