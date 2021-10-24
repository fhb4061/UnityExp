using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Visual : MonoBehaviour
{
    public float minHeight = 15;
    public float maxHeight = 425;
    public Color color = Color.blue;

    [Space(15)]
    public int simple = 64;

    AudioSource _audio;
    public AudioMixerGroup microphone;
    VisualObjects[] visualObjects;
    // Start is called before the first frame update
    void Start()
    {
        visualObjects = GetComponentsInChildren<VisualObjects>();

        _audio = GetComponent<AudioSource>();
        _audio.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
        _audio.loop = true;
        _audio.mute = false;
        _audio.outputAudioMixerGroup = microphone;
        
         while(!(Microphone.GetPosition(null) > 0 )) {
            _audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float[] sample = new float[1024];
        _audio.GetSpectrumData(sample, 0, FFTWindow.Rectangular);

        for (int i = 0; i < visualObjects.Length; i++) {
            Vector2 newSize = visualObjects[i].GetComponent<RectTransform>().rect.size;
            newSize.y = minHeight + (sample[i] * (maxHeight * minHeight) * 5);

            visualObjects[i].GetComponent<RectTransform>().sizeDelta = newSize;
            visualObjects[i].GetComponent<Image>().color = color;
        }
    }
}
