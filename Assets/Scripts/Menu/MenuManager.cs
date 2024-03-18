using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void SavePlayerProgress()
    {
        PlayerSaveLoader.SaveProgressJson();
    }
    public void LoadPlayerProgress()
    {
        PlayerSaveLoader.LoadProgressJson();
    }
}
