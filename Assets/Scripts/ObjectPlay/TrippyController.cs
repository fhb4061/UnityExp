using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrippyController : MonoBehaviour
{
    public Material material;
    public Vector2 position;
    public float scale;

    private void UpdateShader() {
        // change based on aspect of the screen
        float aspect = (float) Screen.width / (float) Screen.height;
        float scaleX = scale;
        float scaleY = scale;

        if (aspect > 1f) {
            scaleY /= aspect;
        } else {
            scaleX *= aspect;
        }

        material.SetVector("_Area", new Vector4(position.x, position.y, scaleX, scaleY));
    }
    void FixedUpdate()
    {
        UpdateShader();
    }
}
