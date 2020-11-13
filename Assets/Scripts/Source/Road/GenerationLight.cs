using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class GenerationLight : MonoBehaviour
{
    private Light _light;
    private CarGenerator _generator;

    void Awake()
    {
        _light = GetComponent<Light>();

        if(!transform.parent.TryGetComponent<CarGenerator>(out _generator))
        {
            Debug.LogWarning("Light parent must have CarGenerator.");
        }

        _generator.BeforeGeneration += (s, e) =>
        {
            StartCoroutine(Signal(e.Seconds));
        };
    }

    private IEnumerator Signal(int seconds)
    {
        yield return new WaitForSeconds(seconds - 1);
        _light.enabled = true;
        yield return new WaitForSeconds(2);
        _light.enabled = false;
    }
}
