using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Audio : MonoBehaviour
{
    public float sensitivity = 100;
    public float loudness = 0;
    AudioSource _audio;
    public AudioMixerGroup microphone;

    // Start is called before the first frame update
    void Start()
    {
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

        loudness = GetAverageVolume() * sensitivity;
        if (loudness > 8) {
            // this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 3);
            // this.GetComponent<Rigidbody2D>().SetRotation(loudness);
            // this.GetComponent<Rigidbody2D>().MoveRotation(loudness);
            this.GetComponent<Rigidbody2D>().gameObject.transform.localScale = new Vector3(1, loudness / 5, 1);
        }
    }

    float GetAverageVolume() {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);
        foreach(float s in data) {
            a += Mathf.Abs(s);
        }

        return a / 256;
    }
}
