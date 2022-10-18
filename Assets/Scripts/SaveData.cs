using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/*
* Student name: Rikveet singh hayer
* Student id: 6590327
*/
public static class SaveData
{
    /*
    * This class is resposible for storing player data and loading player data in binary form.
    */
    static string path = Application.persistentDataPath + "/player.killswitch";
    public static void SavePlayer(world player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            return data;
        }
        return null;
    }
}
