using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game02
{
    public class GridController : MonoBehaviour
    {
        public int x, y;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                GetComponentInParent<MapManager>().Rotate(x, y);
                Check();
            });
        }

        private void Check()
        {
            if (MapManager._instance.Check())
            {
                Debug.Log("game over");
            }
        }
    }
}

