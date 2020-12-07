using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_WaterWave : MonoBehaviour
{
    public Texture[] textures;
    private Material material;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        //重复调用同样的方法，0->立马调用这个函数，0.1f->每隔0.1秒调用一次
        InvokeRepeating(nameof(ChangeTexture),
                        0,
                        0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeTexture()
    {
        material.mainTexture = textures[index];
        index = (index + 1) % textures.Length;
    }
}
