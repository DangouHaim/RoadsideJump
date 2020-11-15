using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saving : MonoBehaviour
{
    public PlayerData PlayerData;

    void Awake()
    {
        PlayerData = SaveManager.Load(PlayerData.GetType()) as PlayerData;
        if(PlayerData == null)
        {
            PlayerData = new PlayerData();
        }
    }
}
