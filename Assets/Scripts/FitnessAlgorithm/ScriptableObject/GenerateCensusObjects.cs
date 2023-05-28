using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GenerateCensusObjects : MonoBehaviour
{


    //public CensusEntriesListSO allCensusEntriesListSO;

    public CensusSO templateCensusSO;
    public FitnessAlgorithm fitnessAlgorithm;

    public List<CensusSO> generatedAssets;

    [ContextMenu("DO IT")]
    public void CreateNewSO()
    {
        for (int i = 0; i < fitnessAlgorithm.censusData.Length; i++)
        {
            CensusSO asset = ScriptableObject.CreateInstance<CensusSO>();

            Census censusData = fitnessAlgorithm.censusData[i];

            string assetName = censusData.id + "_" + censusData.sex + "_" + censusData.age + "_" + censusData.race + "_"+Random.Range(0,999).ToString();

            asset.census = censusData;

            AssetDatabase.CreateAsset(asset, "Assets/ScriptableObjects/Characters/" + assetName + ".asset");
            AssetDatabase.SaveAssets();

            generatedAssets.Add(asset);
        }

    }


}
