using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && this.gameObject.activeSelf)
        {
            this.gameObject.GetComponent<Animator>().SetBool("isFind",true);
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            Singleton<GameEventManager>.Instance.reduceBabyNum();
        }
    }
}
