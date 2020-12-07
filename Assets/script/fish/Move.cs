using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform[] aimPositions;//鱼游动的目标
    public float speed = 10;
    public Vector3 dir;//this.gameobject的运动方向
    public int index;//记录随机点
    private float time;//定时器
    public bool isWalk;//状态判断
    public bool coll;



    private float angle;
   

   
    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);//给游戏物体一个初始方向，让它去撞击边界触发器
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        //Anim = GetComponent<Animator>();
        //Anim.Play("run");
        time = 0;
        isWalk = true;
        coll = false;


    
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;//定时



        if ( time > 6)//3秒改变一次方向，让游戏物体在不同的方向走，
        {
            ChangeState();
        }
        
        //一种运动中状态
        //一种停止状态 //两秒变换一次状态
        if (isWalk)
        {
            
            transform.localPosition += dir.normalized * speed * Time.deltaTime;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    
    }
    void ChangeState()
    {
        int value = Random.Range(0, 6);
        if (value == 0) //停下来
        {
            isWalk = false;//停止
        }
        else //继续走
        {
            if (!isWalk)//如果本来是停下来的鱼，现在变为走动，那就转一下方向
            {
                dir = dir+new Vector3(Random.Range(-2, 2), Random.Range(-2, 10), 0);
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;//旋转的角度
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



            }
            isWalk = true;
        }
        time = 0;//定时器清零
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        dir = -dir;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;//旋转的角度
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        coll = true;
    }

}
