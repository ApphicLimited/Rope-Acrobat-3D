using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sndMan;
    private AudioSource audioSrc;
    private AudioClip[] coinSounds;
    private int randomCoinSounds;



    // Start is called before the first frame update
    void Start()
    {
        sndMan = this;
        audioSrc = GetComponent<AudioSource>();
        coinSounds = Resources.LoadAll<AudioClip>("CoinSounds");

    }


    void PlayCoinSounds()
    {
        randomCoinSounds = Random.Range(0, 1);
        audioSrc.PlayOneShot(coinSounds[randomCoinSounds]);

    }
}
