using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveForm : MonoBehaviour
{
    // prefab reference
    public GameObject prefab;
    public GameObject[] prefab_array = new GameObject[1024];
    // Start is called before the first frame update
    void Start()
    {
        float x = -512, y = 0, z =0;
        // get local x position of the prefab
        float increment = prefab.transform.localScale.x;

        for (int i = 0; i < prefab_array.Length; i++) {
            // instantiate the square prefabs
            GameObject go = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
            go.GetComponent<Renderer>().material.SetColor("_Color", new Color(0,1,0));
            //increment the x position
            x += increment;
            go.name = "waveform" + i;
            
            // set a child of this waveform - useful when you want to move it as one rather than one at a time
            go.transform.parent = this.transform;
            go.transform.localScale = new Vector3(1, 10, 1);
            // set to active
            go.SetActive(true);
            // put into the array
            prefab_array[i] = go;
        }

        this.transform.localPosition = new Vector3(this.transform.position.x, 150, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // reference waveform
        float[] wave_form = AudioIn.waveform;

        // transform position based on waveform data
        for(int i = 0; i < prefab_array.Length; i++) {
            prefab_array[i].transform.localPosition = new Vector3(prefab_array[i].transform.localPosition.x, 100 * wave_form[i], prefab_array[i].transform.localPosition.z);
        }
    }
}
