using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FXManager
{
    public enum Sound
    {
        PlayerSlash,
        PlayerBolt,
        PlayerWeapon3,
        PlayerWeapon4,
    }
    // Start is called before the first frame update
    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audiosource = soundGameObject.AddComponent<AudioSource>();
        audiosource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound)
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
