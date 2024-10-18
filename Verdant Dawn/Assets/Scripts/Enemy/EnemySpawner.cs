using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private void Start()
    {
        //Factory.Instance.GetGhoul(new Vector3(20, 0, 0), Vector3.zero);
        Factory.Instance.GetSkeleton(new Vector3(-20, 0, 0), Vector3.zero);
    }
}
