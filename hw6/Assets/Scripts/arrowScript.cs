using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    public int cnt = 0;
    private string ColliderTag;
    private ScoreController scoreController;

    void OnTriggerEnter(Collider tar) {
        cnt++;
        if(cnt>1) return;
        ColliderTag = tar.gameObject.tag;
        // Debug.Log(ColliderTag);
        Rigidbody parent = this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>();
        parent.isKinematic = true;
        parent.velocity = Vector3.zero;
        parent.gameObject.tag = "Hit";
		// this.gameObject.SetActive(false);
        scoreController.Record(ColliderTag);
    }

    public void reset(){
        cnt = 0;
    }

    void Awake(){
        scoreController = (ScoreController)FindObjectOfType(typeof(ScoreController));
    }
}
