using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicBehaviour : MonoBehaviour
{
    public AudioClip introClip ,mainLoop;
    public AudioSource musicSource;

    private bool hasIntroPlayed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(musicSource.time >= introClip.length && !hasIntroPlayed)
        {
            musicSource.Stop();
            musicSource.clip = mainLoop;
            musicSource.loop = true;
            musicSource.Play();
            hasIntroPlayed = true;
        }
    }
}
