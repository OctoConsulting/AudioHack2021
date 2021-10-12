using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public AudioSource gunSource;

    private bool isRecording = false;






    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            if (!gunSource.isPlaying)
            {
                isRecording = true;
                //BasicAudio.instance.ListenerStartStop();
                gunSource.Play();
            }
        }
        // if gun sound has stopped playing, then stop recording
        if (!gunSource.isPlaying && isRecording)
        {

            isRecording = false;
            //BasicAudio.instance.ListenerStartStop();

        }



    }









}
