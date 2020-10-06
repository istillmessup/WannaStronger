using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game03
{
    public class MapManager : MonoBehaviour
    {
        public GameObject gridPrefab;
        public Color[] color;

        private List<Color> gridColorList = new List<Color>();
        private Transform gridParent;

        private void Awake()
        {
            gridParent = GameObject.Find("GridParent").transform;
            SetColorList();
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    GameObject grid = Instantiate(gridPrefab, gridParent);
                    grid.transform.localPosition = new Vector2(i * 100, j * 100);
                    grid.GetComponent<Image>().color = gridColorList[0];
                    gridColorList.RemoveAt(0);
                }
            }
        }
        private void SetColorList()
        {
            List<Color> list = new List<Color>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    list.Add(color[i]);
                }
            }

            while (list.Count > 0)
            {
                int r = Random.Range(0, list.Count);
                gridColorList.Add(list[r]);
                list.RemoveAt(r);
            }
        }
    }
}

