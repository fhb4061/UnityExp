using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioIn : MonoBehaviour
{
    public AudioSource _audioSource;
    public AudioMixerGroup microphoneMixer;
    public string[] micDevices;
    // represent the soundwave itself
    public static float[] waveform = new float[1024];
    // represent the fequency of the sound
    public static float[] spectrum = new float[512];

    // Start is called before the first frame update
    void Start()
    {
        // get audio source reference in the gameobject
        _audioSource = GetComponent<AudioSource>();
        // we want it to loop when playback
        _audioSource.loop = true;
        // we want it not to be muted
        _audioSource.mute = false;
        // assign a different audio mixer and mute it so we don't get any feedback
        _audioSource.outputAudioMixerGroup = microphoneMixer;
        
        if (Microphone.devices.Length > 0) {
            micDevices = Microphone.devices;
            _audioSource.clip = Microphone.Start(micDevices[0], true, 1, AudioSettings.outputSampleRate);
        }

        // do nothing to decrease latency of mic to audio input source.
        while(!(Microphone.GetPosition(micDevices[0]) > 0)) { }
        
        // play the audio
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        _audioSource.GetOutputData(waveform, 0);
        _audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
    }
}
