using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumSyncer : MonoBehaviour
{
    // hold what value spectrum value that will trigger a beat
    public float bias;
    // determine minimal interval between each beat
    public float timeStep;
    // determine how much time before the visualization completes
    public float timeToBeat;
    // determine how fast the object goes back to rest after a beat
    public float restSmoothTime;
    
    [Space(15)]
    private float previousAudioValue;
    private float audioValue;
    private float timer;

    protected bool isBeat;

    // Update is called once per frame
    private void Update()
    {
        OnUpdate();
    }

    // set to virtual so subclasses can do somthing with it.
    public virtual void OnBeat() {
        Debug.Log("beat");
        timer = 0;
        isBeat = true;
    }
    public virtual void OnUpdate() {
        previousAudioValue = audioValue;
        audioValue = AudioInput.spectrumValue;

        if (previousAudioValue > bias && audioValue <= bias) {
            if (timer > timeStep) {
                OnBeat();
            }
        }

        if (previousAudioValue <= bias && audioValue > bias) {
            if (timer > timeStep) {
                OnBeat();
            }
        }

        // finally
        timer += Time.deltaTime;
    }
}
