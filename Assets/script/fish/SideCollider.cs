using UnityEngine;

public class SideCollider : MonoBehaviour
{
    [Header("border colliders --- ")]
    public Transform leftBorderCollider;
    public Transform rightBorderCollider;
    public Transform upBorderCollider;
    public Transform downBorderCollider;


    [Header("half collider width --- ")]
    public float halfColliderWidth = 0.5f;
    public float halfColliderHeight = 0.5f;

    // half screen width size
    private float halfScreenWidth;
    private float halfSreenHeight;

    private void Start()
    {
        halfSreenHeight = Camera.main.orthographicSize;
        halfScreenWidth = Screen.width / (float)Screen.height * halfSreenHeight;
        Debug.Log(halfScreenWidth);
        //
        leftBorderCollider.position = new Vector3(-halfScreenWidth - halfColliderWidth, leftBorderCollider.position.y, leftBorderCollider.position.z);
        rightBorderCollider.position = new Vector3(halfScreenWidth + halfColliderWidth, rightBorderCollider.position.y, rightBorderCollider.position.z);
        upBorderCollider.position = new Vector3(upBorderCollider.position.x, halfSreenHeight+halfColliderHeight, upBorderCollider.position.z);
        downBorderCollider.position = new Vector3(downBorderCollider.position.x, -halfSreenHeight - halfColliderHeight, downBorderCollider.position.z);
    }
}

