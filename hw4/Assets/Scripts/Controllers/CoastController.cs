using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoastController 
{
	readonly GameObject coast;
	readonly Vector3 startPos = new Vector3(9,1,0);
	readonly Vector3 endPos = new Vector3(-9,1,0);
	readonly Vector3[] positions;
		/// direction : Start or End
	readonly string direction;	

	/// change frequently
	myCharacterController[] passengerPlaner;

	public CoastController(string direc) {
		positions = new Vector3[] {new Vector3(6.5F,2.0F,0), new Vector3(7.5F,2.0F,0), new Vector3(8.5F,2.0F,0), 
			new Vector3(9.5F,2.0F,0), new Vector3(10.5F,2.0F,0), new Vector3(11.5F,2.0F,0)};

		passengerPlaner = new myCharacterController[6];

		if (direc == "Start") {
			coast = Object.Instantiate (Resources.Load ("Prefabs/Coast", typeof(GameObject)), startPos, Quaternion.identity, null) as GameObject;
			coast.name = "Start";
            coast.tag = "Start";
			direction = "Start";
		} 
        else if(direc == "End"){
			coast = Object.Instantiate (Resources.Load ("Prefabs/Coast", typeof(GameObject)), endPos, Quaternion.identity, null) as GameObject;
			coast.name = "End";
            coast.tag = "End";
			direction = "End";
		}
	}

	public int getEmptyIndex() {
		for (int i = 0; i < passengerPlaner.Length; i++) {
			if (passengerPlaner [i] == null) {
				return i;
			}
		}
		return -1;
	}

	public Vector3 getEmptyPosition() {
		Vector3 pos = positions [getEmptyIndex ()];
        if(direction == "End")
            pos.x *= -1;
		return pos;
	}

	public void getOnCoast(myCharacterController cC) {
		int index = getEmptyIndex ();
		passengerPlaner [index] = cC;
	}
		
	public myCharacterController getOffCoast(string passengerName) {	
		for (int i = 0; i < passengerPlaner.Length; i++) {
			if (passengerPlaner [i] != null && passengerPlaner [i].getName () == passengerName) {
				myCharacterController cC = passengerPlaner [i];
				passengerPlaner [i] = null;
				return cC;
			}
		}
		return null;
	}

	public string getDirection() {
		return direction;
	}

	public int[] getNum() {
		int[] count = {0, 0};
		for (int i = 0; i < passengerPlaner.Length; i++) {
			if (passengerPlaner [i] == null)
				continue;
			/// 0->priest, 1->devil
			if (passengerPlaner [i].getType () == 0) {	
				count[0]++;
			} else {
				count[1]++;
			}
		}
		return count;
	}

	public void reset() {
		passengerPlaner = new myCharacterController[6];
	}
}