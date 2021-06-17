using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManger : MonoBehaviour
{
    public AmountSounds[] Sounds;

    public static AudioManger instance;

    //FindObjectOfType<AudioManger>().Play("PlayerDamage");
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);
        /* foreach(AmountSounds s in Sounds)
         {
             s.source = gameObject.AddComponent<AudioSource>();
             s.source.clip = s.clip;

             s.source.volume = s.volume;
             s.source.pitch = s.pitch;
             s.source.loop = s.loop;

         }*/
    }
    void Start()
    {
        Play("Theme");
        Play("SeaGulls");
    }


    public void Play(string name)
    {
       AmountSounds s=   Array.Find(Sounds, sound=> sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found");
            return;
        }

        s.source.Play();
    }

 
}
