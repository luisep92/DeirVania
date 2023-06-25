using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static void EnableDisableGameObject(GameObject go)
    {
        if (go.activeInHierarchy)
            go.SetActive(false);
        else
            go.SetActive(true);
    }
}
