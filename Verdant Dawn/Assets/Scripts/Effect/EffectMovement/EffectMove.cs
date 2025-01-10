using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMove : MonoBehaviour
{
    public float power;

    Transform parent;

    private void Awake()
    {
        parent = transform.parent;
    }

    private void OnEnable()
    {
        transform.position = parent.position;
        transform.rotation = parent.rotation;
    }

    void Update()
    {
        transform.Translate(transform.forward * power, Space.World);
    }
}
