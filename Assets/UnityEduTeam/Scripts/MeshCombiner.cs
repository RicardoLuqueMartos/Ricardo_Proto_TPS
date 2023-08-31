using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{



    public static void PrepareCombine(GameObject parentObj, bool switchToStatic, bool IsDisableMyPhysics/*, bool ByMesh*/) {

        List<GameObject> ToMergeObjectsList = new List<GameObject>();
        List<GameObject> ToMergeObjectsByMeshList = new List<GameObject>();

        var mfList = parentObj.transform.GetComponentsInChildren<MeshFilter>();
        Debug.Log(parentObj.name + " "+ mfList.Length);

        for (int j = 0; j < mfList.Length; j++)
        {
            if (mfList[j].gameObject != null 
                && (IsDisableMyPhysics || mfList[j].GetComponent<DisableMyPhysics>() == false))
                     
                ToMergeObjectsList.Add(mfList[j].gameObject);

            if (switchToStatic && mfList[j].gameObject != null)
            {
                mfList[j].gameObject.isStatic = true;
            }
        }
        Debug.Log(parentObj.name + " " + ToMergeObjectsList.Count+" " + IsDisableMyPhysics.ToString());

        Combine(ToMergeObjectsList, parentObj);
    }

    public static void Combine(List<GameObject> list, GameObject root)
    {
        try
        {
            StaticBatchingUtility.Combine(list.ToArray(), list[0]);
        }
        catch (ArgumentOutOfRangeException){ 
            Debug.LogError(root.name + " pb combining, count = "+ list.Count); 
        }
    }
}
