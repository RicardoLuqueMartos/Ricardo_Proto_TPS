using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;




public class MazeSpawner : MonoBehaviour {

    [SerializeField] List<GameObject> Modules = new List<GameObject>();

    [SerializeField] private List<GameObject> SpawnPoints = new List<GameObject>();

    [SerializeField] private List<GameObject> MazeModules = new List<GameObject>();



	// Use this for initialization
	void Start () {
        SpawnPoints.AddRange(GameObject.FindGameObjectsWithTag("ModuleLoc"));
                

        //Boucle sur les modules du maze
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            List<GameObject> ToMergeObjectsList = new List<GameObject>();

            GameObject instanceModule = Instantiate(Modules[Random.Range(0, Modules.Count)], SpawnPoints[i].transform.position, Quaternion.identity);
            MazeModules.Add(instanceModule);

           var mfList = instanceModule.transform.GetComponentsInChildren<MeshFilter>();

           for (int j = 0; j < mfList.Length; j++)
            {
                if (mfList[j].gameObject != null && mfList[j].GetComponent<DisableMyPhysics>() == false)                
                    ToMergeObjectsList.Add(mfList[j].gameObject);
            }

            Destroy(SpawnPoints[i]);

            Combine(ToMergeObjectsList, instanceModule);

        }

        void Combine(List<GameObject> list, GameObject root)
        {
            StaticBatchingUtility.Combine(list.ToArray(), root);
        }
    }	
}
