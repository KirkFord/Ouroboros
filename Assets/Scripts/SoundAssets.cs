using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    private static SoundAssets _i;

    public static SoundAssets i
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("FXAssets")) as GameObject)?.GetComponent<SoundAssets>();
                return _i;
        }
    }

    public SoundAudioClip[] FXArray;

    [Serializable]
    public class SoundAudioClip
    {
        public BGM.Sound sound;
        public AudioClip audioClip;
    }
    
}
