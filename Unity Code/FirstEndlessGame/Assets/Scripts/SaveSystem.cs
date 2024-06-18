using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEditor;
using UnityEngine;

using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SensorData<T> {

}
[Serializable]
public class GyroData
{
    public string type;
    public string time;
    public string value;

    public GyroData(string s, float t, string v) 
    { 
        type = s;
        time = t.ToString().Replace(',', '.');
        value = v;
    }
}
public static class SaveSystem
{

    private static string ToCSV(List<GyroData> gyroData, bool b)
    {
        var sb = new StringBuilder("Type,Time,x,y,z" + '\n');
        if (b)
        {
            sb = new StringBuilder('\n');
        }
        foreach (var data in gyroData)
        {
            sb.Append(data.type.ToString()).Append(',').Append(data.time).Append(',').Append(data.value.ToString()).Append('\n');
        }

        return sb.ToString();
    }
    public static void SaveGyroData(List<GyroData> gyro)
    {
        var path = Application.persistentDataPath;
        var filePath = Path.Combine(path, "export.csv");
        var exists = false;
        if (File.Exists(filePath))
        {
            exists = true;
        }
        var data = ToCSV(gyro, exists);
        using (var writer = new StreamWriter(filePath, true))
        {
            writer.Write(data);
        }
        Debug.Log("I wrote here:" + Application.persistentDataPath);
    }

}
