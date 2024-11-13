using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class WFX_LightFlicker : MonoBehaviour
{
	public float time = 0.05f;
	
	private float timer;
	
	void Start ()
	{
		timer = time;
		StartCoroutine(Flicker());
	}
	
	IEnumerator Flicker()
	{
		while(timer > 0)
		{
			GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
			
			timer -= Time.deltaTime;
			yield return null;

            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        }
	}
}
