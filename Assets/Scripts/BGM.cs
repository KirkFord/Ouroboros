using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    static BGM instance;
 
    // Drag in the .mp3 files here, in the editor
    public AudioClip[] MusicClips;
 
    public AudioSource Audio;
 
    // Singelton to keep instance alive through all scenes
    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
 
        DontDestroyOnLoad(this);
        Audio = GetComponent<AudioSource>();
        //Audio.clip = Resources.Load(name) as AudioClip;
        // Hooks up the 'OnSceneLoaded' method to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
 
    // Called whenever a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        // Replacement variable (doesn't change the original audio source)
        //AudioSource source = new AudioSource();
 
        // Plays different music in different scenes
        switch (scene.name)
        {
            case "MainHall":
                int random = Random.Range(0, 2);
                if (random == 0)
                {
                    Audio.enabled = false;
                    Audio.clip = MusicClips[0];
                    Audio.enabled = true;
                }
                else if (random == 1)
                {
                    Audio.enabled = false;
                    Audio.clip = MusicClips[1];
                    Audio.enabled = true;
                }
                break;
            case "Shop":
                Audio.enabled = false;
                Audio.clip = MusicClips[2];
                Audio.enabled = true;
                break;
            case "Heal":
                Audio.enabled = false;
                Audio.clip = MusicClips[3];
                Audio.enabled = true;
                break;
            case "Puzzle":
                Audio.enabled = false;
                Audio.clip = MusicClips[4];
                Audio.enabled = true;
                break;
            // default:
            //     source.clip = MusicClips[4];
            //     break;
        }
 
        // Only switch the music if it changed
        // if (source.clip != Audio.clip)
        // {
        //     Audio.enabled = false;
        //     Audio.clip = source.clip;
        //     Audio.enabled = true;
        // }
    }
}