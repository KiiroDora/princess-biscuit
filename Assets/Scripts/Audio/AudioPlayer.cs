using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private string initialBGM;

    public static AudioPlayer instance;

    void Awake()
    {
        instance = this;

        if (initialBGM != null && initialBGM != "")
        {
            StopAllBGM();
            PlayAudio(initialBGM);
        }
    }


    public void PlayAudio(string name)
    {
        GameObject.Find(name).GetComponent<AudioSource>().Play();
    }


    public void StopAudio(string name)
    {
        GameObject.Find(name).GetComponent<AudioSource>().Stop();
    }
    

    public void StopAllBGM()
    {
        foreach (AudioSource source in FindObjectsOfType<AudioSource>())
        {
            if (source.outputAudioMixerGroup.name == "BGM")
            {
                source.Stop();
            }
        }
    }
}
