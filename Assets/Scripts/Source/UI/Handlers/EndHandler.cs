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

    private Toggle _useSoundToggle;
    private Button _retryButton;

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
        _useSoundToggle = GetComponentInChildren<Toggle>();
        _retryButton = GetComponentInChildren<Button>();
    }

    private void SetHandlers()
    {
        _useSoundToggle.onValueChanged.AddListener(
            delegate {
                OnUseSoundChanged();
            }
        );

        _retryButton.onClick.AddListener(
            delegate {
                OnRetryClick();
            }
        );
    }

    private void OnUseSoundChanged()
    {
        _service.SettingsModel.UseSound = _useSoundToggle.isOn;
    }

    private void OnRetryClick()
    {
        AudioManager.Instance.Play("Button");
        _service.LoadingModel.Loading = true;
        _player.GetComponent<PersonController>().Respawn();
    }
}
