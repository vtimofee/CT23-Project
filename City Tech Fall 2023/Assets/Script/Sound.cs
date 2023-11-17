using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource SurfaceAudio;
    public AudioSource DeepSeaAudio;
   
    void Update()
    {
        if (PauseMenu.isPaused == true)
        {
           SurfaceAudio.Pause();
           DeepSeaAudio.Pause();
        } else if(PauseMenu.isPaused == false)
        {
            SurfaceAudio.UnPause();
            DeepSeaAudio.UnPause();
        }

    }
}
