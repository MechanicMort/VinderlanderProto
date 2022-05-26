using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceChunk", menuName = "ScriptableObjects/Resource", order = 1)]
public class Resource : ScriptableObject
{
    public string resource;
    public float amount;
}
