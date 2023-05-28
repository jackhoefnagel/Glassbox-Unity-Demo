using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json;

[System.Serializable]
public class ProfielColumnsFitness
{
    public List<string> columnHeaders;
    public List<string> columnData;
    public float fitness;

    public ProfielColumnsFitness()
    {
        columnHeaders = new List<string>();
        columnData = new List<string>();
    }

    public string GetDataByHeader(string headerName)
    {
        int index = columnHeaders.FindIndex(x => x == headerName);        
        if(index > 0 && columnData[index] != null)
        {
            return columnData[index];
        }
        else
        {
            return null;
        }
    }
}

public class ProfileJSONParser : MonoBehaviour
{
    public TextAsset profilejsonFile;

    public List<ProfielColumnsFitness> profielColumnsFitnesses;

    [ContextMenu("Parse JSON")]
    public void ParseJSONFile()
    {
        ProfilePartial.Profiel[] profiels = ProfilePartial.Profiel.FromJson(profilejsonFile.text);
        
        for (int i = 0; i < profiels.Length; i++)
        {
            ProfielColumnsFitness profiel = new ProfielColumnsFitness();

            for (int j = 0; j < profiels[i].Chromosome.Columns[0].Length; j++)
            {
                profiel.columnHeaders.Add(profiels[i].Chromosome.Columns[0][j].ToString());
                profiel.columnData.Add(profiels[i].Chromosome.Columns[1][j].ToString());
            }

            profiel.fitness = float.Parse(profiels[i].Fitness.ToString());

            profielColumnsFitnesses.Add(profiel);
        }
    }
}
