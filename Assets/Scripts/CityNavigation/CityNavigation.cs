using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityNavigation : MonoBehaviour
{

    public List<Transform> navigationPoints;

    public Transform GetRandomNavigationPoint()
    {
        return navigationPoints[Random.Range(0, navigationPoints.Count)];
    }
}
