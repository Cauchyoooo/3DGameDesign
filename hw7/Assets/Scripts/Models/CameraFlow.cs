using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public GameObject target;

    Vector3 offset;

    void Start()
    {
        offset = this.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.transform.position + offset;
        Rotate();
        Scale();
    }
    //缩放
    private void Scale()
    {
        float dis = offset.magnitude;
        dis += Input.GetAxis("Mouse ScrollWheel") * 5;
        if (dis < 3 || dis > 20)
        {
            return;
        }
        offset = offset.normalized * dis;
    }
    //左右移动
    private void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 pos = this.transform.position;
            this.transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 10);
            //  更新相对差值
            offset = this.transform.position - target.transform.position;
        }
    }
}