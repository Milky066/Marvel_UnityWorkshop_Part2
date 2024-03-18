using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public static class PlayerSaveLoader
{
    // For saving player position, Vector3 is not serializable
    public class PlayerData
    {
        public float positionX;
        public float positionY;
        public float positionZ;

        public PlayerData(Vector3 position)
        {
            positionX = position.x;
            positionY = position.y;
            positionZ = position.z;
        }
    }
    private static string savePath = "./Saves/playerSave.json";
    public static void SaveProgressJson()
    {
        PlayerData playerData = new PlayerData(PlayerController.instance.transform.position);
        string jsonData = JsonUtility.ToJson(playerData);

        FileStream fileStream = new FileStream(savePath, FileMode.Create);
        StreamWriter writer = new StreamWriter(fileStream);
        writer.Write(jsonData);

        writer.Close();
        fileStream.Close();

        Debug.Log("Saved");
    }

    public static void LoadProgressJson()
    {
        FileStream fileStream = new FileStream(savePath, FileMode.Open);
        StreamReader reader = new StreamReader(fileStream);

        string jsonData = reader.ReadToEnd().ToString();
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        Vector3 loadPosition = new Vector3(playerData.positionX, playerData.positionY, playerData.positionZ);
        PlayerController.instance.transform.position = loadPosition;
        Physics.SyncTransforms();

        reader.Close();
        fileStream.Close();
        Debug.Log(string.Format("Loaded at {0}", loadPosition));
    }

}
