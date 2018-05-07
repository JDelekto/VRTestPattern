using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

using UnityEngine;

// Test Pattern Initial Transform & Scale
// Transform [x, y, z] (0, 0, 4)
// Rotation  [x, y, z] (0, 0, 0)
// Scale     [x, y, z] (5.5, 5.5, 0.1)

public class PatternController : MonoBehaviour {

    // comfortable values before clipping occurs
    private const float MIN_DISTANCE = 0.0f;
    private const float MAX_DISTANCE = 1000.0f;
    private const float MIN_ANGLE = -90.0f;
    private const float MAX_ANGLE = 90.0f;
    private const float Z_INCREMENT = 0.10f;
    private const float ROTATE_INCREMENT = 1.0f;
    private const float DEFAULT_Z_POSITION = 4.0f;

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

    private float zPosition = DEFAULT_Z_POSITION;
    	
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
                UnparentPattern();
                break;
            case PatternMode.HmdFixed:
                ParentPatternToHmd();
                break;
            default:
                break;
        }
    }

    private void ParentPatternToHmd()
    {
        if ((this.patternParent != null) && (this.pattern != null))
        {            
            this.pattern.parent = this.patternParent;
            this.pattern.localPosition = new Vector3(0.0f, 0.0f, DEFAULT_Z_POSITION);
            this.pattern.localRotation = Quaternion.identity;
        }
    }

    private void UnparentPattern()
    {
        if (this.pattern != null)
        {
            this.pattern.parent = null;
            this.pattern.position = new Vector3(0.0f, 2.5f, DEFAULT_Z_POSITION);
            this.pattern.rotation = Quaternion.identity;
        }
    }

    private void MovePattern(float increment)
    {
        if (this.pattern != null)
        {
            bool hasParent = (this.pattern.parent != null);

            zPosition += increment;

            if ((zPosition >= MIN_DISTANCE) && (zPosition <= MAX_DISTANCE))
            {
                if (hasParent)
                {
                    this.pattern.localPosition = new Vector3(0.0f, 0.0f, zPosition);
                    this.pattern.localRotation = Quaternion.identity;
                }
                else
                {
                    this.pattern.position = new Vector3(0.0f, 0.0f, zPosition);
                    this.pattern.rotation = Quaternion.identity;
                }
            }
        }
    }

    private void RotatePattern(float increment)
    {        
        if (this.pattern != null)
        {
            bool hasParent = (this.pattern.parent != null);

            if (hasParent)
            {
                Vector3 eulerAngles = pattern.localRotation.eulerAngles;
                float xAngle = (float)(Convert.ToInt32((eulerAngles.x + increment) * 100) / 100);

                this.pattern.localRotation = Quaternion.Euler(xAngle, 0, 0);
            }
            else
            {
                Vector3 eulerAngles = pattern.rotation.eulerAngles;
                float xAngle = (float)(Convert.ToInt32((eulerAngles.x + increment) * 100) / 100);

                this.pattern.rotation = Quaternion.Euler(xAngle, 0, 0);
            }
        }
    }
}
