using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        Paddle,
        Wall,
        Goal,
        Win,
        Lose
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObj = new GameObject("Sound");
        AudioSource audioSource = soundGameObj.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetSoundAudioClip(sound));
        
    }

    private static AudioClip GetSoundAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip sAudioClip in GameAssets.i.soundAudioClips)
        {
            if(sAudioClip.sound == sound)
            {
                return sAudioClip.audioClip;
            }
        }
        Debug.LogError($"Sound {sound} not found!!");
        return null;
    }
}
