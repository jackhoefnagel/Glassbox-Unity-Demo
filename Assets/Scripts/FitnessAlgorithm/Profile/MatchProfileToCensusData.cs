using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ColumnHeaderNameSOs
{
    public string columnName;
    public List<CensusUniqueDataEntrySO> SOs;

    public List<CensusUniqueDataEntrySO> GetSOsByColumnName(string soLookupString)
    {
        if(soLookupString == columnName) // fuzzy search necessary?
        {
            return SOs;
        }
        else
        {
            return null;
        }
    }

    public CensusUniqueDataEntrySO GetSOByData(string dataLookupString = null, int dataLookupInt = 0)
    {
        CensusUniqueDataEntrySO returnSO = null;

        if (dataLookupString != null)
        {
            for (int i = 0; i < SOs.Count; i++)
            {
                if(SOs[i].GetSOByString(dataLookupString) != null)
                {
                    returnSO = SOs[i].GetSOByString(dataLookupString);
                    break;
                }
            }
        }
        if (dataLookupInt != 0)
        {            
            for (int i = 0; i < SOs.Count; i++)
            {
                if (SOs[i].GetSOByInt(dataLookupInt) != null)
                {
                    returnSO = SOs[i].GetSOByInt(dataLookupInt);
                    break;
                }
            }
        }
        return returnSO;
    }
}

public class MatchProfileToCensusData : MonoBehaviour
{
    public ProfileJSONParser profileJSONParser;
    public List<CharacterCensusData> availableAvatarPrefabs;
    public List<ColumnHeaderNameSOs> columnHeaderNameSOs;

    public CharacterCensusData defaultAvatarPrefab;
    // columnHeaderNameSOs: sort by this order of importance. loop through, check if it matches column header,
    // and then filter available prefabs list


    //TODO: Make a function that checks which columns are present in this profile, before using GetDataByHeader
    // Make functions for each column that returns a list of avatars. 
    // generate four lists, cross-reference to see if there's an avatar that has the most matches
    // Make FindAvatar function return CharacterCensusData (avatar)
    [ContextMenu("FindAvatarForProfile")]
    public void DoFindAvatar()
    {
        ProfielColumnsFitness profiel = profileJSONParser.profielColumnsFitnesses[0];
        CharacterCensusData avatarPrefab = FindAvatarForProfile(profiel);

        //Debug.Log(avatarPrefab.name, avatarPrefab.gameObject);

        //CharacterCensusData instantiatedAvatar = Instantiate(avatarPrefab);
        //AvatarProfileData avatarProfileData = instantiatedAvatar.gameObject.AddComponent<AvatarProfileData>();
        

    }

    public CharacterCensusData FindAvatarForProfile(ProfielColumnsFitness profiel)
    {
        List<CharacterCensusData> allAvailableAvatarPrefabs = new List<CharacterCensusData>();
        CharacterCensusData selectedAvatarPrefab = defaultAvatarPrefab;

        for (int i = 0; i < columnHeaderNameSOs.Count; i++)
        {            
            List<CharacterCensusData> tempAvatarPrefabs = new List<CharacterCensusData>();
            if (profiel.columnHeaders.Contains(columnHeaderNameSOs[i].columnName))
            {
                switch (columnHeaderNameSOs[i].columnName)
                {
                    case "age":
                        tempAvatarPrefabs = GetAgePrefabs(profiel);                        
                        if (tempAvatarPrefabs != null)
                            allAvailableAvatarPrefabs.AddRange(GetAgePrefabs(profiel));
                        break;
                    case "wage_per_hour":
                        tempAvatarPrefabs = GetIntDataPrefabsByHeader(columnHeaderNameSOs[i].columnName, profiel);
                        if (tempAvatarPrefabs != null)
                            allAvailableAvatarPrefabs.AddRange(GetIntDataPrefabsByHeader(columnHeaderNameSOs[i].columnName, profiel)); 
                        break;
                    default:                        
                        tempAvatarPrefabs = GetStringDataPrefabsByHeader(columnHeaderNameSOs[i].columnName, profiel);
                        if (tempAvatarPrefabs != null)
                            allAvailableAvatarPrefabs.AddRange(GetStringDataPrefabsByHeader(columnHeaderNameSOs[i].columnName, profiel));
                        break;
                }
            }
        }

        List<CharacterCensusData> distinctAvailableAvatarPrefabs = allAvailableAvatarPrefabs.Distinct().ToList();
        foreach(CharacterCensusData character in distinctAvailableAvatarPrefabs)
        {
            Debug.Log(character.name);
        }
        

        

        // do Age check, have a list of available avatars, then...?
        // if no Age available in profile, skip to next data feature

        // Income 50K - range?
        // *EMPTY*
        // [ empty string/null, "" ]

        /*

        //if age is present, fill the initial avatarsList with age-based avatars.
        int ageIndex = profiel.columnHeaders.FindIndex(x => x == columnHeaderNameSOs[i].columnName);
        if (ageIndex > -1)
        {
            distinctAvailableAvatarPrefabs.AddRange(GetAgePrefabs());
        }            
        else
        {
            int index = profiel.columnHeaders.FindIndex(x => x == columnHeaderNameSOs[i].columnName);
            if (index > -1)
            {
                string headerName = profiel.columnHeaders[index];
                string stringData = profiel.GetDataByHeader(headerName);
                CensusUniqueDataEntrySO so = columnHeaderNameSOs[i].GetSOByData(stringData, 0);

                for (int j = 0; j < availableAvatarPrefabs.Count; j++)
                {
                    if (availableAvatarPrefabs[j].DoesCharacterHaveCensusSO(so))
                    {
                        distinctAvailableAvatarPrefabs.Add(availableAvatarPrefabs[j]);
                    }
                }

                break;
            }
        }

            // First filteredAvailableAvatarPrefabs list is now made, let's remove stuff from it
            // Make a backup of current filtered avatar list, if everything is filtered, use backup.
            // If something is filtered, use filtered list.

        


        // If age is even present:
        //List<CharacterCensusData> firstAgeList = GetAgePrefabs(profiel);
        // Else, skip to another data set,


        for (int i = 0; i < profiel.columnHeaders.Count; i++)
        {
            int index = columnHeaderNameSOs.FindIndex(x => x.columnName == profiel.columnHeaders[i]);            
            if(index > -1)
            {
                // list of SOs per column:
                // columnHeaderNameSOs[index].SOs


            }
        }
        */
         

        return selectedAvatarPrefab;

    }

    

    private List<CharacterCensusData> GetIntDataPrefabsByHeader(string columnHeader, ProfielColumnsFitness tempProfiel)
    {
        List<CharacterCensusData> selectedAvatarPrefabs = new List<CharacterCensusData>();

        Debug.Log("retrieved: ");
        Debug.Log(tempProfiel.GetDataByHeader(columnHeader));
        Debug.Log("from: "+columnHeader);

        return selectedAvatarPrefabs;
    }

    private List<CharacterCensusData> GetStringDataPrefabsByHeader(string columnHeader, ProfielColumnsFitness tempProfiel)
    {

        string stringData = tempProfiel.GetDataByHeader(columnHeader);
        if (stringData == null)
        {
            return null;
        }
        else
        {
            List<CharacterCensusData> selectedAvatarPrefabs = new List<CharacterCensusData>();

            for (int j = 0; j < availableAvatarPrefabs.Count; j++)
            {
                // Header is known (columnheader), but not connected to SO (CensusDataSOLists which contain CensusUniqueDataEntrySO)
                // Columnheader is string, not a SO. Is there a CensusUniqueDataEntrySO lookup? - Not really
                // TODO: So the question is how to go from columnheader string to actual data (range).

            }

            return selectedAvatarPrefabs;
        }
        

        //return null;
    }

    private List<CharacterCensusData> GetAgePrefabs(ProfielColumnsFitness tempProfiel)
    {
        string age = tempProfiel.GetDataByHeader("age");
        if (age == null)
        {
            return null;
        }
        else
        {
            List<CharacterCensusData> selectedAvatarPrefabs = new List<CharacterCensusData>();

            string[] ageRangeString = age.Split(":");
            Vector2Int ageRange = new Vector2Int(int.Parse(ageRangeString[0]), int.Parse(ageRangeString[1]));

            for (int j = 0; j < availableAvatarPrefabs.Count; j++)
            {
                float profileAgeRangeCenter = ((ageRange.x + ageRange.y) / 2);
                float prefabAgeRangeCenter = ((availableAvatarPrefabs[j].potentialCensusData.range_age.x + availableAvatarPrefabs[j].potentialCensusData.range_age.y) / 2);

                float startA = profileAgeRangeCenter - 10f;
                float endA = profileAgeRangeCenter + 10f;

                float startB = prefabAgeRangeCenter - 10f;
                float endB = prefabAgeRangeCenter + 10f;

                bool isPrefabWithinAgeRange = startA <= endB && endA >= startB;

                if (isPrefabWithinAgeRange)
                {
                    selectedAvatarPrefabs.Add(availableAvatarPrefabs[j]);
                }
            }

            return selectedAvatarPrefabs;
        }
        
    }
    
}
