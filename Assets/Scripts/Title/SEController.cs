using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClip;

    void Start()
    {
        // サウンドソースコンポーネント取得
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip[0];
    }

    public void StopSE()
    {
        audioSource.Stop();
    }
    public void SeButton()
    {
        //スタートSEを再生します
        audioSource.PlayOneShot(audioClip[0]);
    }
    public void playSE(int playSENo)
    {
        //スタートSEを再生します
        audioSource.PlayOneShot(audioClip[playSENo]);
    }
}
