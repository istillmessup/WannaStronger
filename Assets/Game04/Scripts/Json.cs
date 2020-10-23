using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace Game04
{
    public class Json
    {
        public static List<Vector3> road = new List<Vector3>();

        public static void Save(List<Vector3> pointList)
        {
            string path = "LevelInfo/Game04/Map_" + Directory.GetFiles("LevelInfo/Game04").Length + ".json";
            Debug.Log(path);
            if (File.Exists(path) == false)
            {
                File.Create(path).Dispose();
            }
            PointList p = new PointList();
            p.pointList = pointList;
            string json = JsonUtility.ToJson(p);
            File.WriteAllText(path, json);
        }

        public static void Load(string file)
        {
            string path = "LevelInfo/Game04/" + file + ".json";
            if (File.Exists(path) == false)
            {
                return;
            }
            string json = File.ReadAllText(path);
            PointList p = JsonUtility.FromJson<PointList>(json);
            foreach (Vector3 item in p.pointList)
            {
                Debug.Log(item);
            }
            road = p.pointList;
        }
    }
    [Serializable]
    public class PointList
    {
        public List<Vector3> pointList;
    }
}

