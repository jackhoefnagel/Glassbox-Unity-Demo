using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class Census
{
    public int      id;
    public string   income_50k;
    public int      age;
    public string   class_worker;
    public string   education;
    public int      wage_per_hour;
    public string   hs_college;
    public string   marital_stat;
    public string   race;
    public string   hisp_origin;
    public string   sex;
    public string   unemp_reason;
    public string   country_father;
    public string   country_mother;
    public string   country_self;
    public string   citizenship;
}

[System.Serializable]
public class CensusEntriesList
{
    public List<int>    l_id;
    public List<string> l_income_50k;
    public List<int>    l_age;
    public List<string> l_class_worker;
    public List<string> l_education;
    public List<int>    l_wage_per_hour;
    public List<string> l_hs_college;
    public List<string> l_marital_stat;
    public List<string> l_race;
    public List<string> l_hisp_origin;
    public List<string> l_sex;
    public List<string> l_unemp_reason;
    public List<string> l_country_father;
    public List<string> l_country_mother;
    public List<string> l_country_self;
    public List<string> l_citizenship;
}

public class FitnessAlgorithm : MonoBehaviour
{
    public TextAsset jsonFile;
    public Census[] censusData;
    public CensusEntriesList censusEntriesList;
    public CensusEntriesList censusUniqueEntriesList;

    [ContextMenu("Fill Census Data")]
    public void FillCensusData()
    {
        censusData = JsonConvert.DeserializeObject<Census[]>(jsonFile.text);

        FillCensusList(censusData);
        FilterUniqueCensusEntries(censusEntriesList);
    }

    void FillCensusList(Census[] censusArray)
    {
        for (int i = 0; i < censusArray.Length; i++)
        {
            censusEntriesList.l_id.Add(             censusArray[i].id);
            censusEntriesList.l_income_50k.Add(     censusArray[i].income_50k);
            censusEntriesList.l_age.Add(            censusArray[i].age);
            censusEntriesList.l_class_worker.Add(   censusArray[i].class_worker);
            censusEntriesList.l_education.Add(      censusArray[i].education);
            censusEntriesList.l_wage_per_hour.Add(  censusArray[i].wage_per_hour);
            censusEntriesList.l_hs_college.Add(     censusArray[i].hs_college);
            censusEntriesList.l_marital_stat.Add(   censusArray[i].marital_stat);
            censusEntriesList.l_race.Add(           censusArray[i].race);
            censusEntriesList.l_hisp_origin.Add(    censusArray[i].hisp_origin);
            censusEntriesList.l_sex.Add(            censusArray[i].sex);
            censusEntriesList.l_unemp_reason.Add(   censusArray[i].unemp_reason);
            censusEntriesList.l_country_father.Add( censusArray[i].country_father);
            censusEntriesList.l_country_mother.Add( censusArray[i].country_mother);
            censusEntriesList.l_country_self.Add(   censusArray[i].country_self);
            censusEntriesList.l_citizenship.Add(    censusArray[i].citizenship);
        }        
    }

    void FilterUniqueCensusEntries(CensusEntriesList allEntries)
    {
        censusUniqueEntriesList.l_id =              allEntries.l_id.Distinct<int>().ToList();
        censusUniqueEntriesList.l_income_50k =      allEntries.l_income_50k.Distinct<string>().ToList();
        censusUniqueEntriesList.l_age =             allEntries.l_age.Distinct<int>().ToList();
        censusUniqueEntriesList.l_class_worker =    allEntries.l_class_worker.Distinct<string>().ToList();
        censusUniqueEntriesList.l_education =       allEntries.l_education.Distinct<string>().ToList();
        censusUniqueEntriesList.l_wage_per_hour =   allEntries.l_wage_per_hour.Distinct<int>().ToList();
        censusUniqueEntriesList.l_hs_college =      allEntries.l_hs_college.Distinct<string>().ToList();
        censusUniqueEntriesList.l_marital_stat =    allEntries.l_marital_stat.Distinct<string>().ToList();
        censusUniqueEntriesList.l_race =            allEntries.l_race.Distinct<string>().ToList();
        censusUniqueEntriesList.l_hisp_origin =     allEntries.l_hisp_origin.Distinct<string>().ToList();
        censusUniqueEntriesList.l_sex =             allEntries.l_sex.Distinct<string>().ToList();
        censusUniqueEntriesList.l_unemp_reason =    allEntries.l_unemp_reason.Distinct<string>().ToList();
        censusUniqueEntriesList.l_country_father =  allEntries.l_country_father.Distinct<string>().ToList();
        censusUniqueEntriesList.l_country_mother =  allEntries.l_country_mother.Distinct<string>().ToList();
        censusUniqueEntriesList.l_country_self =    allEntries.l_country_self.Distinct<string>().ToList();
        censusUniqueEntriesList.l_citizenship =     allEntries.l_citizenship.Distinct<string>().ToList();
    }
}
