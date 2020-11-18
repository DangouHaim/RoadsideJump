using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour
{
    public int SpectrumSensetivity = 16;
    private Dictionary<int, Transform> _lines;
    private const int SpectrumSize = 256;

    // Start is called before the first frame update
    void Start()
    {
        _lines = new Dictionary<int, Transform>();
        int lineShift = SpectrumSize / SpectrumSensetivity / transform.childCount;
        int i = 0;
        
        foreach(Transform t in transform)
        {
            _lines.Add(i, t);
            i += lineShift;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrum = new float[SpectrumSize];
        
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        foreach(var line in _lines)
        {
            Vector3 scale = new Vector3(
                line.Value.localScale.x,
                spectrum[line.Key],
                line.Value.localScale.z
            );

            line.Value.localScale = scale;
            if(scale.y > 1)
            {
                Debug.Log(scale);
            }
        }
    }
}
