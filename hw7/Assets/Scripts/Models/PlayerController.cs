using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.1f;
    private Animator ani;
    private int targetRot = 0;
    // private Transform RigTran;
    // private Rigidbody rig;

    void Start()
    {
        ani = GetComponent<Animator>();
        // RigTran = this.transform.Find("Rig");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float verti = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");
        if(verti != 0 || horiz !=0){
            ani.SetBool("isWalk",true);
            if(Input.GetKey(KeyCode.LeftShift)){
                ani.SetBool("isRun",true);
                speed = 0.3f;
            }
            else{
                ani.SetBool("isRun",false);
                speed = 0.1f;
            }       
        }
        else{
            ani.SetBool("isWalk",false);
            return;
        }
        
        this.transform.position += new Vector3(horiz * speed,0,verti * speed);
        // (-180,90)AND(90,180) s=180/-180
        // (-90,0) a=-90
        //  w = 0
        //  (0,90) d=90
           
        targetRot = 0;
        if(horiz != 0){
            targetRot = (int)(horiz/Mathf.Abs(horiz)) * 90;
            if(verti>0){
                targetRot = targetRot/2;
            }
            if(verti < 0){
                targetRot = targetRot/2*3;
            }
        }
        else if(horiz == 0 && verti < 0){
            targetRot = this.transform.rotation.y >= 0 ? 180 : -180;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, targetRot, 0), 0.05f);
        // this.transform.Rotate(0,horiz,0);
        // Debug.Log(verti);
        // RigTran.Rotate(0, 0, horiz);


    }
}
