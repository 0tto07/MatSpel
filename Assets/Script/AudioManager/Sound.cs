using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Rendering;
[System.Serializable]
public class Sound
{

    [SerializeField] string name;

    [SerializeField] AudioClip audioClip;

    [Range(0f, 1f)]
    [SerializeField] float volume;

    [Range(.1f, 3f)]
    [SerializeField] float pitch;

    [SerializeField] bool loop;

    [HideInInspector]
    public AudioSource source;
   

   public AudioClip GetAudioClip() { return audioClip; }
    public float GetVolume() { return volume; }
    public float GetPitch() { return pitch; }
    public string GetName() { return name; }
    public bool IsLoop() { return loop; }

}
   

