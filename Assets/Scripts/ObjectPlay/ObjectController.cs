using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public float x;

    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);

        if (transform.localPosition.y > 7) {
            transform.localPosition = new Vector3(x, 1, 0);
        }
    }
}
