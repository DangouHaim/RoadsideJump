using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTracker : MonoBehaviour, ITracker
{
    public int Count()
    {
        return -(int)transform.position.z;
    }
}
