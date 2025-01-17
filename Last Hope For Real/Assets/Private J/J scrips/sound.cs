using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioClip Sound;
    AudioSource audio;
    public float StartTime = 4.5f;
    public float endTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
        
        audio = GetComponent<AudioSource>();
        audio.clip = Sound;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            audio.time = StartTime;
            audio.Play();
            StartCoroutine(StopAudioAfterTime(endTime - StartTime));
        }
    }
    private IEnumerator StopAudioAfterTime(float delay) 
    { yield return new WaitForSeconds(delay);
        audio.Stop();
    }
}
