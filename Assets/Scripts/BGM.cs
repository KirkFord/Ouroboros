using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    public static BGM instance;

    // Drag in the .mp3 files here, in the editor
    public AudioClip[] MusicClips;
 
    public AudioSource Audio;

    [Range(0f, 1f)]
    public float fxvolume;
    
    [Range(0f, 1f)]
    public float musicVolume;
    
    public enum Sound
    {
        PlayerSlash,
        PlayerBolt,
        PlayerWeapon3,
        PlayerWeapon4,
        MenuSelectFX,
    }

 
    // Singelton to keep instance alive through all scenes
    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
 
        DontDestroyOnLoad(this);
        Audio = GetComponent<AudioSource>();
        Audio.volume = musicVolume;
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
        switch (scene.name) {
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
            case "StartScreen":
                Audio.enabled = false;
                Audio.clip = MusicClips[7];
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
    // Start is called before the first frame update

    public void GameOverSwitch()
    {
        Audio.enabled = false;
        Audio.loop = false;
        Audio.clip = MusicClips[5];
        Audio.enabled = true;
    }

    public void StatsSwitch()
    {
        Audio.enabled = false;
        Audio.loop = true;
        Audio.clip = MusicClips[6];
        Audio.enabled = true;
    }
    
    
    public void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audiosource = soundGameObject.AddComponent<AudioSource>();
        var ac = GetAudioClip(sound);
        audiosource.PlayOneShot(ac, fxvolume);
        Destroy(soundGameObject,ac.length);
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.i.FXArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + " not found!");
        return null;
    }
}