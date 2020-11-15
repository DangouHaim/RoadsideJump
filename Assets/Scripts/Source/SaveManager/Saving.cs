using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saving : MonoBehaviour
{
    public PlayerData PlayerData;
    public UserModel UserModel;

    void Start()
    {
        DontDestroyOnLoad(this);

        PlayerData = SaveManager.Load(PlayerData.GetType()) as PlayerData;
        if(PlayerData == null)
        {
            PlayerData = new PlayerData();
        }

        UserModel = new UserModel()
        {
            Coins = PlayerData.Coins,
            PathRecord = PlayerData.PathRecord,
            Path = 0
        };
    }
}
