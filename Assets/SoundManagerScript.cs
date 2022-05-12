using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip gol;
    static AudioSource audiosrc;
    // Start is called before the first frame update
    void Start()
    {
        gol = Resources.Load<AudioClip> ("golSoundNew");
        audiosrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void PlaySound(string clip)
    {
        Debug.Log("AUDIO DE GOL");
        if(clip == "gol"){
            audiosrc.PlayOneShot(gol);
        }
    }
}
