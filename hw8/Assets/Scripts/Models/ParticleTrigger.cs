using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        GameObject Rain = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Rain"));
        Rain.transform.position = new Vector3(this.transform.position.x,0,this.transform.position.z);
        this.gameObject.tag = "Finish";
        // this.gameObject.SetActive(false);
    }
}
