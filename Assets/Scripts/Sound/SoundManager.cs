using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    public AudioSource bgm;
    public AudioSource soundEffect;

    public SoundManager()
    {
        GameObject bgmObject = new GameObject("Bgm Source");
        bgm = bgmObject.AddComponent<AudioSource>();

        GameObject effectObject = new GameObject("FX Source");
        soundEffect = effectObject.AddComponent<AudioSource>();

        Singleton.instance.gameEvent.onPlayBGM.AddListener(OnPlayBGM);
        Singleton.instance.gameEvent.onPlaySoundEffect.AddListener(OnPlaySoundEffect);
        Singleton.instance.gameEvent.onStopBGM.AddListener(OnStopBGM);
    }

    public void OnPlaySoundEffect(AudioClip audio)
    {
        soundEffect.PlayOneShot(audio);
    }

    public void OnPlayBGM(AudioClip bgmSound)
    {
        bgm.clip = bgmSound;
        bgm.Play();
    }

    public void OnStopBGM()
    {
        bgm.Stop();
    }
}
