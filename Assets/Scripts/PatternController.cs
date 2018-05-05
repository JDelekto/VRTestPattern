using System;
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
    const float MIN_ANGLE = -90.0f;
    const float MAX_ANGLE = 90.0f;
    const float Z_INCREMENT = 1.0f;
    const float ROTATE_INCREMENT = 1.0f;

    private enum PatternMode
    {
        Default = 0,
        HmdFixed = 1
    }

    private PatternMode mode = PatternMode.Default;

    [Tooltip("Canvas which contains Test Pattern image.")]
    public Transform pattern;

    [Tooltip("Parent for fixed Pattern Mode.")]
    public Transform patternParent;
    	
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
        else if (Input.GetKey(KeyCode.F))
        {
            RotatePattern(ROTATE_INCREMENT);
        }
        else if (Input.GetKey(KeyCode.B))
        {
            RotatePattern(-ROTATE_INCREMENT);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetMode(PatternMode.Default);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetMode(PatternMode.HmdFixed);
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
    
    private void SetMode(PatternMode newMode)
    {
        if (this.mode == newMode)
        {
            return;
        }

        this.mode = newMode;

        switch (newMode)
        {
            case PatternMode.Default:
                UnparentCanvas();
                break;
            case PatternMode.HmdFixed:
                ParentCanvasToHmd();
                break;
            default:
                break;
        }
    }

    private void ParentCanvasToHmd()
    {
        if ((this.patternParent != null) && (this.pattern != null))
        {
            this.pattern.parent = this.patternParent;
        }
    }

    private void UnparentCanvas()
    {
        if (this.pattern != null)
        {
            this.pattern.parent = null;
        }
    }

    private void MovePattern(float increment)
    {
        if (this.pattern != null)
        {
            var newVector = new Vector3(0.0f, 0.0f, pattern.position.z + increment);

            if ((newVector.z >= MIN_DISTANCE) && (newVector.z <= MAX_DISTANCE))
            {
                pattern.position = newVector;
            }
        }
    }

    private void RotatePattern(float increment)
    {
        if (this.pattern != null)
        {
            Vector3 eulerAngles = pattern.rotation.eulerAngles;
            float xAngle = (float)(Convert.ToInt32((eulerAngles.x + increment) * 100) / 100);

            this.pattern.rotation = Quaternion.Euler(xAngle, 0, 0);
        }
    }
}
