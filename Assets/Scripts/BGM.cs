using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    public static BGM instance;

    // Drag in the .mp3 files here, in the editor
    public AudioClip[] MusicClips;
 
    public AudioSource MusicAudioSource;

    [Range(0f, 1f)]
    public float fxvolume = 0.5f;
    
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    
    public enum Sound
    {
        PlayerSlash,
        PlayerBolt,
        PlayerWeapon3,
        PlayerWeapon4,
        MenuSelectFX,
        PauseMenuFX,
    }

 
    // Singelton to keep instance alive through all scenes
    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
 
        DontDestroyOnLoad(this);
        MusicAudioSource = GetComponent<AudioSource>();
        MusicAudioSource.volume = musicVolume;
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
                    MusicAudioSource.enabled = false;
                    MusicAudioSource.clip = MusicClips[0];
                    MusicAudioSource.enabled = true;
                }
                else if (random == 1)
                {
                    MusicAudioSource.enabled = false;
                    MusicAudioSource.clip = MusicClips[1];
                    MusicAudioSource.enabled = true;
                }
                break;
            case "Shop":
                MusicAudioSource.enabled = false;
                MusicAudioSource.clip = MusicClips[2];
                MusicAudioSource.enabled = true;
                break;
            case "Heal":
                MusicAudioSource.enabled = false;
                MusicAudioSource.clip = MusicClips[3];
                MusicAudioSource.enabled = true;
                break;
            case "Puzzle1":
                MusicAudioSource.enabled = false;
                MusicAudioSource.clip = MusicClips[4];
                MusicAudioSource.enabled = true;
                break;
            case "Puzzle2":
                MusicAudioSource.enabled = false;
                MusicAudioSource.clip = MusicClips[4];
                MusicAudioSource.enabled = true;
                break;
            case "StartScreen":
                MusicAudioSource.enabled = false;
                MusicAudioSource.clip = MusicClips[7];
                MusicAudioSource.enabled = true;
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
        MusicAudioSource.enabled = false;
        MusicAudioSource.loop = false;
        MusicAudioSource.clip = MusicClips[5];
        MusicAudioSource.enabled = true;
    }

    public void StatsSwitch()
    {
        MusicAudioSource.enabled = false;
        MusicAudioSource.loop = true;
        MusicAudioSource.clip = MusicClips[6];
        MusicAudioSource.enabled = true;
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