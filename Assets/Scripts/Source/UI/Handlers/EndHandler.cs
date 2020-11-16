using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndHandler : MonoBehaviour
{
    #region Context

    private DataService _service;
    private GameObject _player;

    #endregion

    #region Elements

    private Toggle _useSound;

    #endregion
    
    void Start()
    {
        Initialize();
        SetHandlers();
        
        if(!GameObject.FindGameObjectWithTag("DataService").TryGetComponent<DataService>(out _service))
        {
            Debug.LogWarning("DataService is null.");
        }

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Initialize()
    {
        _useSound = GetComponentInChildren<Toggle>();
    }

    private void SetHandlers()
    {
        _useSound.onValueChanged.AddListener(delegate { OnUseSoundChanged(); });
    }

    private void OnUseSoundChanged()
    {
        _service.SettingsModel.UseSound = _useSound.isOn;
    }
}
