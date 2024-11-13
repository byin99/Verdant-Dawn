using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightEffect : MonoBehaviour
{
    new Light light;

    public float time = 0.05f;

    private float timer;

    void Start()
    {
        light = GetComponent<Light>();
        timer = time;
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (timer > 0)
        {
            light.enabled = !light.enabled;

            timer -= Time.deltaTime;
            yield return null;

            light.enabled = !light.enabled;
        }
    }
}
