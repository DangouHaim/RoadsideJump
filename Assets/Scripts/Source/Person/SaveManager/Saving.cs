using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saving : MonoBehaviour
{
    public PlayerData PlayerData;

    void Awake()
    {
        PlayerData = new PlayerData();
        PlayerData = SaveManager.Load(PlayerData.GetType()) as PlayerData;
    }
}
