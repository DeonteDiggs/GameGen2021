/*
 * File created by following the following Youtube tutorial: https://www.youtube.com/watch?v=eL_zHQEju8s
 * 
 * The purpose of this script is to create the waves.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    public static waveManager instance;
    public float amplitude = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float offset = 0f;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if( instance == null ){
            instance = this;
        } else if( instance != this){
            Destroy(this);
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        offset += Time.deltaTime*speed;
    }

    public float getWaveHeight(float _x){
        return amplitude*Mathf.Sin(_x/length+offset);
    }
}
