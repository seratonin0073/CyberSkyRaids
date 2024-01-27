using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneGetter : MonoBehaviour
{
    [SerializeField] private GameObject MDL;

    public GameObject GetMDL()
    {
        return MDL;
    }
}
