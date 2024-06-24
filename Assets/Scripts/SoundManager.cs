using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; set; }

    public AudioSource CuttingDownTree;
    public AudioSource DroppingWood;
    public AudioSource Movement;
    public AudioSource Hit;
    public AudioSource Botdied;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Cutting()
    {
        if (!CuttingDownTree.isPlaying)
        {
            CuttingDownTree.Play();
        }
    }

    public void Dropping()
    {
        if (!DroppingWood.isPlaying)
        {
            DroppingWood.Play();
        }
    }

    public void Step()
    {
        if (!Movement.isPlaying)
        {
            Movement.Play();
        }
    }
    public void Running(AudioSource running)
    {
        Movement.pitch = running.pitch;
        if (!Movement.isPlaying)
        {
            Movement.Play();
        }
    }

    public void Hitting()
    {
        if (!Hit.isPlaying)
        {
            Hit.Play();
        }
    }

    public void botdied()
    {
        if (!Botdied.isPlaying)
        {
            Botdied.Play();
        }
    }


}
