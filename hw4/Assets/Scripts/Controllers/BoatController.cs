using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController
{
    readonly GameObject boat;
	// readonly Moveable moveScript;
	readonly Vector3 startPosition = new Vector3 (5, 1, 0);
	readonly Vector3 endPosition = new Vector3 (-5, 1, 0);
	readonly Vector3[] startPositions;
	readonly Vector3[] endPositions;

/// change frequently
    string direction; 
	myCharacterController[] passenger = new myCharacterController[2];

	public BoatController() {
		direction = "Start";

		startPositions = new Vector3[] { new Vector3 (4.5F, 1.1F, 0), new Vector3 (5.5F, 1.1F, 0) };
		endPositions = new Vector3[] { new Vector3 (-5.5F, 1.1F, 0), new Vector3 (-4.5F, 1.1F, 0) };

		boat = Object.Instantiate (Resources.Load ("Prefabs/Boat", typeof(GameObject)), startPosition, Quaternion.identity, null) as GameObject;
		boat.name = "boat";
        boat.tag = "Boat";

		// moveScript = boat.AddComponent (typeof(Moveable)) as Moveable;
		boat.AddComponent (typeof(Click));
	}

	// public void Move() {
	// 	if (direction == "End") {
	// 		moveScript.setDestination(startPosition);
	// 		direction = "Start";
	// 	} 
	// 	else {
	// 		moveScript.setDestination(endPosition);
	// 		direction = "End";
	// 	}
	// }

	public int getEmptyIndex() {
		for (int i = 0; i < passenger.Length; i++) {
			if (passenger [i] == null) {
				return i;
			}
		}
		return -1;
	}

	public bool isEmpty() {
		for (int i = 0; i < passenger.Length; i++) {
			if (passenger [i] != null) {
				return false;
			}
		}
		return true;
	}

	public Vector3 getEmptyPosition() {
		Vector3 pos;
		int emptyIndex = getEmptyIndex ();
		if (direction == "End") {
			pos = endPositions[emptyIndex];
		} 
		else {
			pos = startPositions[emptyIndex];
		}
		return pos;
	}

	public void getOnBoat(myCharacterController characterCtrl) {
		int index = getEmptyIndex ();
		passenger [index] = characterCtrl;
	}

	public myCharacterController getOffBoat(string passenger_name) {
		for (int i = 0; i < passenger.Length; i++) {
			if (passenger [i] != null && passenger [i].getName () == passenger_name) {
				myCharacterController charactorCtrl = passenger [i];
				passenger [i] = null;
				return charactorCtrl;
			}
		}
		return null;
	}
	// public Moveable getMoveScript(){
	// 	return moveScript;
	// }

	public GameObject getGameobj() {
		return boat;
	}

	public string getDirection() {
		return direction;
	}

	public void setDirection(string s){
		this.direction = s;
	}

	public int[] getNum() {
		int[] count = {0, 0};
		for (int i = 0; i < passenger.Length; i++) {
			if (passenger [i] == null)
				continue;
			/// 0->priest, 1->devil
			if (passenger [i].getType () == 0) {	
				count[0]++;
			} 
			else {
				count[1]++;
			}
		}
		return count;
	}

	public Vector3 getPosition(){
		if (direction == "End") {
			return startPosition;
		} 
		else {
			return endPosition;
		}
	}

	public void reset() {
		// moveScript.reset ();
		if (direction == "End") {
		// 	moveScript.setDestination(startPosition);
			direction = "Start";
		}
		passenger = new myCharacterController[2];
	}

	public bool isStop(){
		if(boat.transform.position == startPosition || boat.transform.position == endPosition){
			return true;
		}
		return false;
	}
}