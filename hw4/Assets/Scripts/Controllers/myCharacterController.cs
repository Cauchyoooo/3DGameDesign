using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCharacterController
{
    readonly GameObject character;
	// readonly Moveable moveScript;
	readonly Click click;
	/// 0->priest, 1->devil
	readonly int characterType;	
	/// change frequently
	bool _isOnBoat;
	CoastController coastController;

	public myCharacterController(string _character) {
		if (_character == "Priest") {
			character = Object.Instantiate (Resources.Load ("Prefabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, 180, 0), null) as GameObject;
			characterType = 0;
		} 
		else {
			character = Object.Instantiate (Resources.Load ("Prefabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, 180, 0), null) as GameObject;
			characterType = 1;
		}
		// moveScript = character.AddComponent (typeof(Moveable)) as Moveable;

		click = character.AddComponent (typeof(Click)) as Click;
		click.setController (this);
	}

	public void setName(string name) {
		character.name = name;
	}

    public void setTag(string tag) {
		character.tag = tag;
	}

	public void setPosition(Vector3 pos) {
		character.transform.position = pos;
	}

	// public void moveTo(Vector3 destination) {
	// 	moveScript.setDestination(destination);
	// }

	public int getType() {
		return characterType;
	}

	public string getName() {
		return character.name;
	}

    public string getTag() {
		return character.tag;
	}

	public GameObject GetGameObject(){
		return character;
	}

	public void getOnBoat(BoatController boat) {
		coastController = null;
		character.transform.parent = boat.getGameobj().transform;
		_isOnBoat = true;
	}

	public void getOnCoast(CoastController coast) {
		coastController = coast;
		character.transform.parent = null;
		_isOnBoat = false;
	}

	public bool isOnBoat() {
		return _isOnBoat;
	}

	public CoastController getCoastController() {
		return coastController;
	}

	public void reset() {
		// moveScript.reset ();
		coastController = (Director.getInstance ().currentSceneController as FirstController).startCoast;
		getOnCoast (coastController);
		setPosition (coastController.getEmptyPosition ());
		coastController.getOnCoast (this);
	}

}