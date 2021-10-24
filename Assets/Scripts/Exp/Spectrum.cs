using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour
{

    public GameObject prefab;
    public GameObject[] prefab_array = new GameObject[512];
    // Start is called before the first frame update
    void Start()
    {
        float x = -512, y = 0, z =0;
        // get local x position of the prefab
        float increment = prefab.transform.localScale.x;

        for (int i = 0; i < prefab_array.Length; i++) {
            // instantiate the square prefabs
            GameObject go = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
            go.GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0, 1));

            //increment the x position
            x += increment;
            go.name = "spectrum" + i;
            
            // set a child of this waveform - useful when you want to move it as one rather than one at a time
            go.transform.parent = this.transform;
            go.transform.localScale = new Vector3(2, 5, 1);
            // set to active
            go.SetActive(true);
            // put into the array
            prefab_array[i] = go;
        }

        this.transform.localPosition = new Vector3(513, -200, this.transform.position.z);
        this.transform.localScale = new Vector3(2, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrum = AudioIn.spectrum;

        // transform position based on waveform data
        for(int i = 0; i < prefab_array.Length; i++) {
            float y = 600 * Mathf.Sqrt(spectrum[i]);
            prefab_array[i].transform.localScale = new Vector3(prefab_array[i].transform.localScale.x, y, prefab_array[i].transform.localScale.z);
            prefab_array[i].transform.localPosition = new Vector3(prefab_array[i].transform.localPosition.x, y / 2, prefab_array[i].transform.localPosition.z);
        }
    }
}
