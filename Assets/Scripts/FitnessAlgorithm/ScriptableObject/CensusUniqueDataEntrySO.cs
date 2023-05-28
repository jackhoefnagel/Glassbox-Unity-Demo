using UnityEngine;

public class CensusUniqueDataEntrySO : ScriptableObject
{
    public string stringValue;
    public int intValue;

    public CensusUniqueDataEntrySO GetSOByString(string lookupString)
    {
        if (lookupString == stringValue)
            return this;
        else       
            return null;
    }

    public CensusUniqueDataEntrySO GetSOByInt(int lookupInt)
    {
        if (lookupInt == intValue)
            return this;
        else
            return null;
    }
}
