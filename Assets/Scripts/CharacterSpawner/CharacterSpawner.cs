using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public List<CensusSO> censusSOs;
    public List<GameObject> spawnedCharacters;

    public CityNavigation cityNavigationReference;
    public float spawnRadius = 10f;
    public float spawnHeight = 8f;

    private void Start()
    {
        for (int i = 0; i < censusSOs.Count; i++)
        {
            GameObject spawnedCharacter = Instantiate(censusSOs[i].characterPrefab);
            spawnedCharacter.name = censusSOs[i].name;
            spawnedCharacter.GetComponent<CharacterCensusData>().censusScriptableObject = censusSOs[i];
            spawnedCharacter.GetComponent<NavMeshAgentDemo>().cityNavigation = cityNavigationReference;
            spawnedCharacter.transform.parent = transform;
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPos.y = spawnHeight;
            spawnedCharacter.transform.position = spawnPos;
            
            spawnedCharacters.Add(spawnedCharacter);
        }
    }
}
