using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum Sound
{
    PlayerSlash,
    PlayerBolt,
    PlayerWeapon3,
    PlayerWeapon4,
    MenuSelectFX,
    PauseMenuFX,
}
public class BGM : MonoBehaviour
{
    public static BGM Instance;
    
    [Tooltip("Drag the Sound Files in here")]
    [SerializeField] private AudioClip[] musicClips;
 
    public AudioSource musicAudioSource;

    [Range(0f, 1f)]
    public float fxVolume = 0.5f;
    
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;

    // Singelton to keep instance alive through all scenes
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
 
        DontDestroyOnLoad(this);
        musicAudioSource.volume = musicVolume;
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
                if (!GameManager.Instance.firstTimeWep)
                {
                    GameManager.Instance.firstTimeWep = true;
                    wepSelectSwitch();
                    break;
                }
                int random = Random.Range(0, 2);
                if (random == 0)
                {
                    musicAudioSource.enabled = false;
                    musicAudioSource.clip = musicClips[0];
                    musicAudioSource.enabled = true;
                }
                else if (random == 1)
                {
                    musicAudioSource.enabled = false;
                    musicAudioSource.clip = musicClips[1];
                    musicAudioSource.enabled = true;
                }
                break;
            case "Shop":
                musicAudioSource.enabled = false;
                musicAudioSource.clip = musicClips[2];
                musicAudioSource.enabled = true;
                break;
            case "Heal":
                musicAudioSource.enabled = false;
                musicAudioSource.clip = musicClips[3];
                musicAudioSource.enabled = true;
                break;
            case "Puzzle1":
                musicAudioSource.enabled = false;
                musicAudioSource.clip = musicClips[4];
                musicAudioSource.enabled = true;
                break;
            case "Puzzle2":
                musicAudioSource.enabled = false;
                musicAudioSource.clip = musicClips[4];
                musicAudioSource.enabled = true;
                break;
            case "StartScreen":
                musicAudioSource.enabled = false;
                musicAudioSource.clip = musicClips[7];
                musicAudioSource.enabled = true;
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
        musicAudioSource.enabled = false;
        musicAudioSource.loop = false;
        musicAudioSource.clip = musicClips[5];
        musicAudioSource.enabled = true;
    }

    public void StatsSwitch()
    {
        musicAudioSource.enabled = false;
        musicAudioSource.loop = true;
        musicAudioSource.clip = musicClips[6];
        musicAudioSource.enabled = true;
    }

    public void wepSelectSwitch()
    {
        musicAudioSource.enabled = false;
        musicAudioSource.loop = true;
        musicAudioSource.clip = musicClips[8];
        musicAudioSource.enabled = true;
    }

    public void mainHallSwitch()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            musicAudioSource.enabled = false;
            musicAudioSource.clip = musicClips[0];
            musicAudioSource.enabled = true;
        }
        else if (random == 1)
        {
            musicAudioSource.enabled = false;
            musicAudioSource.clip = musicClips[1];
            musicAudioSource.enabled = true;
        }
    }
    
    public void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audiosource = soundGameObject.AddComponent<AudioSource>();
        var ac = GetAudioClip(sound);
        audiosource.PlayOneShot(ac, fxVolume);
        Destroy(soundGameObject,ac.length);
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.i.fxArray)
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