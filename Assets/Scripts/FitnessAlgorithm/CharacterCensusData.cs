using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCensusData : MonoBehaviour
{
    public CensusSO censusScriptableObject; // external data

    public CensusDataSOLists potentialCensusData; // internal data to define prefab and configure correct CensusSO

    public bool DoesCharacterHaveCensusSO(CensusUniqueDataEntrySO censusUniqueDataEntrySO)
    {
        bool isCensusSOPresent = false;

        if(potentialCensusData.soList_citizenship.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_class_worker.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_country_father.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_country_mother.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_country_self.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_education.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_hisp_origin.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_hs_college.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_income_50k.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_marital_stat.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_race.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_sex.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_unemp_reason.Contains(censusUniqueDataEntrySO) ||
            potentialCensusData.soList_wage_per_hour.Contains(censusUniqueDataEntrySO))
        {
            isCensusSOPresent = true;
        }

        return isCensusSOPresent;
    }


    public string GetMultilineCensusDataString()
    {
        string data = "";
        data += censusScriptableObject.census.income_50k + "\n";
        data += censusScriptableObject.census.age + "\n";
        data += censusScriptableObject.census.class_worker + "\n";
        data += censusScriptableObject.census.education + "\n";
        data += censusScriptableObject.census.wage_per_hour + "\n";
        data += censusScriptableObject.census.hs_college + "\n";
        data += censusScriptableObject.census.marital_stat + "\n";
        data += censusScriptableObject.census.race + "\n";
        data += censusScriptableObject.census.hisp_origin + "\n";
        data += censusScriptableObject.census.sex + "\n";
        data += censusScriptableObject.census.unemp_reason + "\n";
        data += censusScriptableObject.census.country_father + "\n";
        data += censusScriptableObject.census.country_mother + "\n";
        data += censusScriptableObject.census.country_self + "\n";
        data += censusScriptableObject.census.citizenship + "\n";
        return data;
    }
}
