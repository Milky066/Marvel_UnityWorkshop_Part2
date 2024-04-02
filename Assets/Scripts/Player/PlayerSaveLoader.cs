using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Pipes;

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
        public override string ToString()
        {
            return string.Format("({0:F2}, {1:F2}, {2:F2})", positionX, positionY, positionZ);
        }
    }
    private static string savePath = "./Saves/playerSave.json";
    public static void SaveProgressJson()
    {
        PlayerData playerData = new PlayerData(PlayerController.instance.transform.position);
        string jsonData = JsonUtility.ToJson(playerData);

        try
        {
            //using (BinaryWriter writer = new BinaryWriter(fileStream))
            using (StreamWriter writer = new StreamWriter(savePath))
            {
                writer.Write(jsonData);
            }
            Debug.Log("Saved player's position: " + playerData);
        }
        catch (IOException error)
        {
            Debug.LogError("Failed to save player data: " + error.Message);
        }
    }

    public static void LoadProgressJson()
    {
        if (File.Exists(savePath))
        {
            try
            {
                string jsonData = File.ReadAllText(savePath);
                PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                Vector3 loadPosition = new Vector3(playerData.positionX, playerData.positionY, playerData.positionZ);
                PlayerController.instance.transform.position = loadPosition;
                Physics.SyncTransforms();
                Debug.Log("Loaded player's position: " + loadPosition);
            }
            catch (IOException error)
            {
                Debug.LogError("Failed to load player data: " + error.Message);
            }
        }
        else
        {
            Debug.LogWarning("Save not found");
        }
    }

}
