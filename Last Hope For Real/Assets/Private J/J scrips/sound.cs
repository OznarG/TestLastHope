using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioClip Sound;
    public AudioClip Sound2;
    public AudioClip Sound3;
   new  AudioSource audio;
   

    public float StartTime2 = 0.1f;

    

        public float endTime2 = 0.2f;

    public float StartTime = 4.5f;
    public float endTime = 5f;
    public bool Inventory ;
    

    // Start is called before the first frame update
    void Start()
    {
       
        
        audio = GetComponent<AudioSource>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.I) && Inventory || Input.GetKeyDown(KeyCode.Escape) && Inventory)
        {
            audio.Stop();
            Inventory = false;
           
            Debug.Log("Inventory false");
            
        } else if (Input.GetKeyDown(KeyCode.I) && !Inventory || Input.GetKeyDown(KeyCode.Escape) && !Inventory)
        {
           Inventory=true;
            audio.pitch = 1f;

            audio.clip = Sound3;

            audio.Play();
            Debug.Log("Inventory true");
        }




    }
    private IEnumerator StopAudioAfterTime(float delay) 
    { yield return new WaitForSeconds(delay);
        audio.Stop();
       
    }
    public void JUMP()
    {
        audio.pitch = 0.75f;
        audio.clip = Sound;
        audio.time = StartTime;
        audio.Play();
        StartCoroutine(StopAudioAfterTime(endTime - StartTime));
    }
    public void SWING()
    {
        audio.pitch = 0.75f;
        audio.clip = Sound2;

        audio.time = StartTime2;
        audio.Play();
        StartCoroutine(StopAudioAfterTime(endTime2 - StartTime2));
    }
}
