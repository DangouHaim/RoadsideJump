using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashHandler : MonoBehaviour
{
    public int HideAfter = 1;
    
    private static bool _init = false;
    
    void Awake()
    {
        if(_init)
        {
            return;
        }
        _init = true;
        GetComponent<Canvas>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(HideAfter);
        GetComponent<Canvas>().enabled = false;
    }
}
