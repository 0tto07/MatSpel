using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{


    [SerializeField] Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.GetAudioClip();

            s.source.volume=s.GetVolume();
            s.source.pitch=s.GetPitch();
            s.source.loop=s.IsLoop();
        }
        
        
    }


    void Start()
    {
        Play("Test");
    }



    public void Play(string name)
    {
       Sound s= Array.Find(sounds, sound => sound.GetName() == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }


    
}


