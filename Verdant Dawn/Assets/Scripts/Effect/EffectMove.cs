using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMove : MonoBehaviour
{
    public float power;
    float changedFactor;

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
        changedFactor = VariousEffectsScene.m_gaph_scenesizefactor;

        transform.Translate(transform.forward * power * changedFactor, Space.World);
        
    }
}
