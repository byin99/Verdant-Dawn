using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariousTranslateMove : MonoBehaviour 
{
    public float m_power;
    public float m_changedFactor;

	void Update () 
    {
        m_changedFactor = VariousEffectsScene.m_gaph_scenesizefactor;

            transform.Translate(transform.forward * m_power * m_changedFactor, Space.World);
    }
}
