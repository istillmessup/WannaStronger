using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game05
{
    public class GridController : MonoBehaviour
    {
        private Image image;

        public int x, y;
        public Sprite white, black;
        public bool isWhite = true;

        private void Awake()
        {
            image = GetComponent<Image>();
            GetComponent<Button>().onClick.AddListener(() =>
            {
                MapManager._instance.ChangeColor(x, y);
                Check();
            });
        }

        public void OnClick()
        {
            isWhite = !isWhite;
            if (isWhite)
            {
                image.sprite = white;
            }
            else
            {
                image.sprite = black;
            }
        }

        private void Check()
        {
            if (MapManager._instance.Check())
            {
                Debug.Log("game over");
                Status.stat = true;
            }
        }
    }
}

