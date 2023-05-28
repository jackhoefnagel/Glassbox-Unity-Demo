using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CensusData")]
public class CensusSO : ScriptableObject
{
    public Census census;
    public GameObject characterPrefab;
}
