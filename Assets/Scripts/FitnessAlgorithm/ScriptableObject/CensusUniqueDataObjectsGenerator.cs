using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class CensusDataSOs
{
    public int age;    
    public CensusUniqueDataEntrySO income_50k;
    public CensusUniqueDataEntrySO class_worker;
    public CensusUniqueDataEntrySO education;
    public CensusUniqueDataEntrySO wage_per_hour;
    public CensusUniqueDataEntrySO hs_college;
    public CensusUniqueDataEntrySO marital_stat;
    public CensusUniqueDataEntrySO race;
    public CensusUniqueDataEntrySO hisp_origin;
    public CensusUniqueDataEntrySO sex;
    public CensusUniqueDataEntrySO unemp_reason;
    public CensusUniqueDataEntrySO country_father;
    public CensusUniqueDataEntrySO country_mother;
    public CensusUniqueDataEntrySO country_self;
    public CensusUniqueDataEntrySO citizenship;
}

[System.Serializable]
public class CensusDataSOLists
{
    public Vector2Int range_age;    
    public List<CensusUniqueDataEntrySO> soList_income_50k;
    public List<CensusUniqueDataEntrySO> soList_class_worker;
    public List<CensusUniqueDataEntrySO> soList_education;
    public List<CensusUniqueDataEntrySO> soList_wage_per_hour;
    public List<CensusUniqueDataEntrySO> soList_hs_college;
    public List<CensusUniqueDataEntrySO> soList_marital_stat;
    public List<CensusUniqueDataEntrySO> soList_race;
    public List<CensusUniqueDataEntrySO> soList_hisp_origin;
    public List<CensusUniqueDataEntrySO> soList_sex;
    public List<CensusUniqueDataEntrySO> soList_unemp_reason;
    public List<CensusUniqueDataEntrySO> soList_country_father;
    public List<CensusUniqueDataEntrySO> soList_country_mother;
    public List<CensusUniqueDataEntrySO> soList_country_self;
    public List<CensusUniqueDataEntrySO> soList_citizenship;
}

public class CensusUniqueDataObjectsGenerator : MonoBehaviour
{
    public FitnessAlgorithm fitnessAlgorithm;
    public List<CensusUniqueDataEntrySO> generatedAssets;

    //[ContextMenu("Clean Up List")]
    //public void CleanupList()
    //{
        
    //    for (int i = 0; i < generatedAssets.Count; i++)
    //    {
    //        if(generatedAssets[i] == null)
    //        {
    //            generatedAssets.RemoveAt(i);
    //            CleanupList();
    //            break;
    //        }
    //    }
    //}


    [ContextMenu("Create All SOs")]
    public void CreateAllSOs()
    {
        
        CensusEntriesList entriesList = fitnessAlgorithm.censusUniqueEntriesList;
        FieldInfo[] fields = entriesList.GetType().GetFields();

        foreach (FieldInfo fieldInfo in fields)
        {
            //TODO: Make Editor IF statement
            if (!AssetDatabase.IsValidFolder("Assets/Scripts/FitnessAlgorithm/ScriptableObject/CensusUniqueDataEntries/" + fieldInfo.Name))
            {
                AssetDatabase.CreateFolder("Assets/Scripts/FitnessAlgorithm/ScriptableObject/CensusUniqueDataEntries", fieldInfo.Name);
            }
        }

        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType == typeof(List<int>))
            {
                CreateIntSO((List<int>)fieldInfo.GetValue(entriesList), fieldInfo.Name);
            }
            if (fieldInfo.FieldType == typeof(List<string>))
            {
                CreateStringSO((List<string>)fieldInfo.GetValue(entriesList), fieldInfo.Name);
            }
        }
    }

    private void CreateStringSO(List<string> stringList, string fieldName)
    {
        foreach(string str in stringList)
        {
            string escapedString = str.Replace("/","");
            escapedString = escapedString.Replace("?","unknown");
            escapedString = escapedString.Trim();

            CensusUniqueDataEntrySO asset = ScriptableObject.CreateInstance<CensusUniqueDataEntrySO>();

            string escapedFieldName = fieldName.Replace("l_","");
            escapedFieldName += "_" + escapedString;
            string assetName = escapedFieldName;

            asset.stringValue = str;

            AssetDatabase.CreateAsset(asset, "Assets/Scripts/FitnessAlgorithm/ScriptableObject/CensusUniqueDataEntries/" + fieldName + "/" + assetName + ".asset");
            AssetDatabase.SaveAssets();
            generatedAssets.Add(asset);
        }
    }

    private void CreateIntSO(List<int> intList, string fieldName)
    {
        foreach (int integer in intList)
        {
            CensusUniqueDataEntrySO asset = ScriptableObject.CreateInstance<CensusUniqueDataEntrySO>();

            string escapedFieldName = fieldName.Replace("l_", "");
            escapedFieldName += "_" + integer.ToString();
            string assetName = escapedFieldName;

            asset.intValue = integer;

            AssetDatabase.CreateAsset(asset, "Assets/Scripts/FitnessAlgorithm/ScriptableObject/CensusUniqueDataEntries/" + fieldName + "/" + assetName + ".asset");
            AssetDatabase.SaveAssets();
            generatedAssets.Add(asset);
        }
    }

    public string Filter(string str, char charToRemove)
    {
        
            str = str.Replace(charToRemove.ToString(), string.Empty);
        

        return str;
    }
}
