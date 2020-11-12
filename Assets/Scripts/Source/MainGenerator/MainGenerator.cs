using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGenerator : MonoBehaviour
{
    public int StartRoadsideLenght = 10;
    public int GenerationDistance = 50;
    public bool ReverceAxis = true;

    private PoolManager _pool;
    private PathTracker _playerPath;
    private ITracker _cameraPath;
    private int _currentGenerationIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _pool = PoolManager.Instance;
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject camera = GameObject.FindGameObjectWithTag("Camera");
        
        if(!player.TryGetComponent<PathTracker>(out _playerPath))
        {
            Debug.LogWarning("PathTracker is null.");
        }

        if(!camera.TryGetComponent<ITracker>(out _cameraPath))
        {
            Debug.LogWarning("ITracker is null.");
        }

        GenerateStartRoadside();
    }

    void FixedUpdate()
    {
        if(_currentGenerationIndex < _playerPath.Count() + GenerationDistance
            || _currentGenerationIndex < _cameraPath.Count() + GenerationDistance)
        {
            GenerateNext();
        }
    }

    private void GenerateStartRoadside()
    {
        for(int i = StartRoadsideLenght; i >= 0 ; i--)
        {
            GenerateByTag("Roadside", i);
        }
    }

    // Generates random part
    // 0.4 Road, 0.3 Roadside, 0.2 Water, 0.1 Rails
    private void GenerateNext()
    {
        _currentGenerationIndex++;

        int value = Random.Range(1, 11);

        int index = _currentGenerationIndex;

        if(ReverceAxis)
        {
            index = -index;
        }

        if(value <= 4)
        {
            GenerateByTag("Road", index);
        }
        if(value > 4 && value <= 7)
        {
            GenerateByTag("Roadside", index);
        }
        if(value > 7 && value <= 9)
        {
            GenerateByTag("Water", index);
        }
        if(value == 10)
        {
            GenerateByTag("Rails", index);
        }
    }

    private void GenerateByTag(string tag, int index)
    {
        Vector3 spawnVector = new Vector3(
                transform.position.x,
                transform.position.y,
                (float)index
            );

            _pool.Spawn(tag, spawnVector, Quaternion.identity);
    }
}
