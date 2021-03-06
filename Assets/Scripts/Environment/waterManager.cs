/*
 * File created by following the following Youtube tutorial: https://www.youtube.com/watch?v=eL_zHQEju8s
 * 
 * The purpose of this script is to move the mesh around with the waves calculated by waveManager.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class waterManager : MonoBehaviour
{
    private MeshFilter meshFilter;

    public void Awake() {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update(){
        Vector3[] vertices = meshFilter.mesh.vertices;

        for(int i = 0; i < vertices.Length; i++){
            vertices[i].y = waveManager.instance.getWaveHeight(transform.position.x + vertices[i].x);
        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateNormals();
    }

}
