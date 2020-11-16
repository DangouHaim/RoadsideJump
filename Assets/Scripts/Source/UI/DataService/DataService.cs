using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataService : MonoBehaviour
{
    public UserModel UserModel;
    public SettingsModel SettingsModel;
    public LoadingModel LoadingModel;

    private static bool _init = false;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        if(_init)
        {
            return;
        }
        _init = true;

        object data = SaveManager.Load(typeof(SettingsModel));
        SettingsModel settingsModel = data == null ? new SettingsModel() : data as SettingsModel;
        SettingsModel = new SettingsModel(settingsModel);

        data = SaveManager.Load(typeof(UserModel));
        UserModel userModel = data == null ? new UserModel() : data as UserModel;
        UserModel = new UserModel(userModel);

        LoadingModel = new LoadingModel()
        {
            Loading = true
        };
    }
}
