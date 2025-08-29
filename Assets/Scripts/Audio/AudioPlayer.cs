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

    public void PlayIngredientAudio(int type)
    {
        string name;
        bool willPlay;
        switch ((PlayerSpawner.IngredientType)type)
        {
            case PlayerSpawner.IngredientType.flour:
                name = "Flour";
                willPlay = PlayerSpawner.flourAmount > 0;
                break;
            case PlayerSpawner.IngredientType.sugar:
                name = "Sugar";
                willPlay = PlayerSpawner.sugarAmount > 0;
                break;
            case PlayerSpawner.IngredientType.egg:
                name = "Egg";
                willPlay = PlayerSpawner.eggAmount > 0;
                break;
            case PlayerSpawner.IngredientType.butter:
                name = "Butter";
                willPlay = PlayerSpawner.butterAmount > 0;
                break;
            case PlayerSpawner.IngredientType.milk:
                name = "Milk";
                willPlay = PlayerSpawner.milkAmount > 0;
                break;
            default:
                name = "Flour";
                willPlay = PlayerSpawner.flourAmount > 0;
                break;
        }

        if (willPlay)
        {
            GameObject.Find(name).GetComponent<AudioSource>().Play();
        }
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
