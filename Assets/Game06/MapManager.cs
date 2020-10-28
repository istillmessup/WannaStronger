using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game06
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager _instance;

        public Button button;
        public GameObject gridPrefab_green;
        public GameObject gridPrefab_purple;
        public Dictionary<Vector2, GameObject> gridDict = new Dictionary<Vector2, GameObject>();

        private void Awake()
        {
            _instance = this;
            Init();
            button.onClick.AddListener(() =>
            {
                for (int i = 0; i < 7; i++)
                {
                    gridDict[new Vector2(0, i)].GetComponent<Button>().enabled = true;
                }
            });
        }

        private void Init()
        {
            for (int i = 0; i < 7; i++)  // y
            {
                for (int j = 0; j < 7; j++) // x
                {
                    GameObject grid;
                    if (i % 2 == 0)
                    {
                        grid = Instantiate(gridPrefab_green, transform.Find("GridParent"));
                    }
                    else
                    {
                        grid = Instantiate(gridPrefab_purple, transform.Find("GridParent"));
                    }
                    if (j != 0)
                    {
                        grid.GetComponent<Button>().enabled = false;
                    }
                    grid.transform.localPosition = new Vector2(j * 100, i * 100);
                    gridDict.Add(new Vector2(j, i), grid);
                    grid.GetComponent<GridController>().x = j;
                    grid.GetComponent<GridController>().y = i;
                    int r = Random.Range(0, 4);
                    SetGridDirection((DirectionType)r, grid);
                }
            }
        }

        private void SetGridDirection(DirectionType dt, GameObject go)
        {
            switch (dt)
            {
                case DirectionType.Up:
                    go.GetComponent<GridController>().direction = 0;
                    go.transform.localEulerAngles = new Vector3(0, 0, 0);
                    break;
                case DirectionType.Left:
                    if (go.GetComponent<GridController>().colorType == ColorType.Green)
                    {
                        go.GetComponent<GridController>().direction = 3;
                        go.transform.localEulerAngles = new Vector3(0, 0, 90);
                    }
                    else
                    {
                        go.GetComponent<GridController>().direction = 1;
                        go.transform.localEulerAngles = new Vector3(0, 0, 90);
                    }
                    break;
                case DirectionType.Down:
                    go.GetComponent<GridController>().direction = 2;
                    go.transform.localEulerAngles = new Vector3(0, 0, 180);
                    break;
                case DirectionType.Right:
                    if (go.GetComponent<GridController>().colorType == ColorType.Green)
                    {
                        go.GetComponent<GridController>().direction = 1;
                        go.transform.localEulerAngles = new Vector3(0, 0, -90);
                    }
                    else
                    {
                        go.GetComponent<GridController>().direction = 3;
                        go.transform.localEulerAngles = new Vector3(0, 0, -90);
                    }
                    break;
                default:
                    break;

            }
        }
    }

    public enum ColorType
    {
        Green,
        Purple
    }

    public enum DirectionType
    {
        Up,
        Left,
        Down,
        Right
    }
}

