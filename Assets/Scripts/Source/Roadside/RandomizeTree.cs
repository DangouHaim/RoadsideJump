using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeTree : MonoBehaviour, IPoolable
{
    public float MinimumScale = 0.8f;
    public float MaximumScale = 1.3f;

    // Random scale & rotation
    public void OnSpawn()
    {
        float random = Random.Range(MinimumScale, MaximumScale);
        Vector3 scale = new Vector3(random, random, random);
        transform.localScale = scale;

        Vector3 eulerAngles = transform.eulerAngles;
        int angle = Random.Range(1, 4);
        eulerAngles.y = 90 * angle;
        transform.eulerAngles = eulerAngles;
    }
}
