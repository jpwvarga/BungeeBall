using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[Serializable]
public class WinSave
{
    public int level = -1;
    public bool hasCompleted = false;
    public bool hasBeatTime = false;
    public bool hasAllCollectibles = false;
    public float highscoreTime = -1;

    private static string folderPath = "Assets/SaveInfo/";

    public static void WriteToFile(WinSave save)
    {
        string path = string.Format("{0}/save_lvl{1}.json", folderPath, save.level);
        StreamWriter writer = new StreamWriter(path);
        string json = JsonUtility.ToJson(save, true);
        writer.WriteLine(json);
        writer.Close();
        //Debug.Log(json);
    }

    public static WinSave ReadFromFile(int lvl) 
    {
        string json;
        string path = string.Format("{0}/save_lvl{1}.json", folderPath, lvl);
        
        try
        {
            StreamReader reader = new StreamReader(path);
            json = reader.ReadToEnd();
            reader.Close();
            return JsonUtility.FromJson<WinSave>(json);
        }
        catch (Exception err)
        {
            Debug.LogWarning(err);
            return null;
        }   
    }

    public static void ClearSave(int lvl)
    {
        string path = string.Format("{0}/save_lvl{1}_new.json", folderPath, lvl);

        try
        {
            StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();
            reader.Close();
            WriteToFile(JsonUtility.FromJson<WinSave>(json));
        }
        catch (Exception err)
        {
            Debug.LogWarning(err);
        }
    }
}
