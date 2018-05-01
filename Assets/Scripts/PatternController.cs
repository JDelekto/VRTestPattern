using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

using UnityEngine;

public class PatternController : MonoBehaviour {

    // comfortable values before clipping occurs
    const float MIN_DISTANCE = 10.0f;
    const float MAX_DISTANCE = 900.0f;
    const float Z_INCREMENT = 1.0f;

    [Tooltip("Canvas which contains Test Pattern image.")]
    public Transform canvas;
    	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.DownArrow))
        {
            MovePattern(Z_INCREMENT);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            MovePattern(-Z_INCREMENT);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Application.isEditor)
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif // UNITY_EDITOR
            }
            else
            {
                Application.Quit();
            }
        }
	}

    private void MovePattern(float increment)
    {
        if (this.canvas != null)
        {
            var newVector = new Vector3(0.0f, 0.0f, canvas.position.z + increment);

            if ((newVector.z >= MIN_DISTANCE) && (newVector.z <= MAX_DISTANCE))
            {
                canvas.position = newVector;
            }
        }
    }
}
