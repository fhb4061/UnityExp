using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 

public class AudioInput : MonoBehaviour
{ public AudioSource _audioSource;
    public AudioMixerGroup microphoneMixer;
    public string[] micDevices;
    public static float spectrumValue {get; private set;}
    public static float waveFormValue {get; private set;}
    private float[] waveform;
    private float[] spectrum;

    // Start is called before the first frame update
    void Start()
    {
        waveform = new float[1024];
        spectrum = new float[128];

        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.mute = false;
        _audioSource.outputAudioMixerGroup = microphoneMixer;
        
        if (Microphone.devices.Length > 0) {
            micDevices = Microphone.devices;
            _audioSource.clip = Microphone.Start(micDevices[0], true, 1, AudioSettings.outputSampleRate);
        }

        while(!(Microphone.GetPosition(micDevices[0]) > 0)) { }
        
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        _audioSource.GetOutputData(waveform, 0);
        _audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
        
        if (waveform != null && waveform.Length > 0) {
            waveFormValue = waveform[0] * 100;
        }
        if (spectrum != null && spectrum.Length > 0) {
            spectrumValue = spectrum[0] * 100;
        }
    }
}
