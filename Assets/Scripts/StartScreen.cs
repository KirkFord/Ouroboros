using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    private LevelChanger _lc;
    private BGM _bgm;
    private GameManager _gm;

    [SerializeField] private Camera mainCam;
    [SerializeField] private Transform cameraStartPosition;
    [SerializeField] private Animator mainCameraAnimator;
    [SerializeField] private GameObject startObject;

    [Header("SETTINGS")]
    [SerializeField] private GameObject settingsObject;

    [Header("Video")] 
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    
    [Header("Sound")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider fxVolumeSlider;
    private bool _isFullscreen;
    private Resolution _currentResolution;



    private void Start()
    {
        _currentResolution = Screen.currentResolution;
        _bgm = BGM.Instance;
        _lc = LevelChanger.Instance;
        _gm = GameManager.Instance;
        mainCam.transform.position = cameraStartPosition.position;
        mainCam.transform.rotation = cameraStartPosition.rotation;

        fxVolumeSlider.value = _bgm.fxVolume;
        musicVolumeSlider.value = _bgm.musicAudioSource.volume;
        
        startObject.SetActive(true);
        settingsObject.SetActive(false);
    }

    public void StartButtonPressed()
    {
        _bgm.PlaySound(Sound.MenuSelectFX);
        startObject.SetActive(false);
        settingsObject.SetActive(false);
        mainCameraAnimator.Play("CameraToDoor");
        //Debug.Log("Start Button Pressed");
    }

    public void SettingsButtonPressed()
    {
        _bgm.PlaySound(Sound.MenuSelectFX);
        mainCameraAnimator.Play("MainToSettings");
        //Debug.Log("Settings Button Pressed");
    }

    public void ExitButtonPressed()
    {
        _bgm.PlaySound(Sound.MenuSelectFX);
        ExitGame();
    }

    public void StartGame()
    {
        _gm.ResetRun();
        _lc.FadeToLevel(Level.LoadPlayerLevel);
    }

    public void ToggleFullscreen()
    {
        _isFullscreen = !_isFullscreen;
        ResetResolution();
    }

    public void ResolutionChanged()
    {
        switch (resolutionDropdown.value)
        {
            case 0:
                _currentResolution.width = 1920;
                _currentResolution.height = 1080;
                ResetResolution();
                break;
            
            case 1:
                _currentResolution.width = 1600;
                _currentResolution.height = 900;
                ResetResolution();
                break;
            
            case 2:
                _currentResolution.width = 1280;
                _currentResolution.height = 720;
                ResetResolution();
                break;
        }
    }

    public void FxVolumeChanged()
    {
        _bgm.fxVolume = fxVolumeSlider.value;
    }

    public void MusicVolumeChanged()
    {
        _bgm.musicAudioSource.volume = musicVolumeSlider.value;
    }

    public void ExitSettings()
    {
        _bgm.PlaySound(Sound.MenuSelectFX);
        mainCameraAnimator.Play("SettingsToMain");
    }

    public void ArrivedAtSettings()
    {
        settingsObject.SetActive(true);
    }

    public void ArrivedAtMain()
    {
        startObject.SetActive(true);
    }
    
    public void LeavingSettings()
    {
        settingsObject.SetActive(false);
    }

    public void LeavingMain()
    {
        startObject.SetActive(false);
    }
    private void ResetResolution()
    {
        //Debug.Log("Setting Resolution to: " +_currentResolution.width + "x" + _currentResolution.height);
        //Debug.Log(_isFullscreen ? "Game is in Fullscreen" : "Game is in Windowed");
        Screen.SetResolution(_currentResolution.width, _currentResolution.height, _isFullscreen);
    }
    private static void ExitGame()
    {
        Application.Quit();
    }

    public void PlayTestSound()
    {
        _bgm.PlaySound(Sound.PlayerSlash);
    }

    
    
}
