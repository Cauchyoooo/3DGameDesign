using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public GameObject target;

    // public Transform target;
    Vector3 offset;
    // Use this for initialization
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
    //左右上下移动
    private void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 pos = this.transform.position;
            Vector3 rot = this.transform.eulerAngles;

            this.transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 10);
            float x = this.transform.eulerAngles.x;
            float y = this.transform.eulerAngles.y;

            // // //控制移动范围
            // if (y > 60 && y < 300)
            // {
            //     this.transform.position = pos;
            //     this.transform.eulerAngles = rot;
            // }
            //  更新相对差值
            offset = this.transform.position - target.transform.position;
        }
    }
}