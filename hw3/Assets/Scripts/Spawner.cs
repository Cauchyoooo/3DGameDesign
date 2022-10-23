using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Homework.Assets.Scripts;

namespace Homework.Assets.Scripts
{
    /// <<Summary>>
    /// Director: Singleton mode Control all the top level things
    /// <<Summary>>
    public class Director : System.Object
    {
        private static Director _instance;

        public ISceneController currentSceneController { get; set;}

        public static Director getInstance()
        {
            if(_instance == null){
                _instance = new Director();
            }
            return _instance;
        }
    }

    /// <<Summary>>
    /// interface SceneController: to help Director handle different Scene
    /// <<Summary>>
    public interface ISceneController
    {
        void LoadResources();
    }

    /// <<Summary>>
    /// Interface UserAction: What users can do
    /// <<Summary>>
    public interface IUserAction
    {
        void ClickCharacter(myCharacterController charactorController);
        void moveBoat();
        void Restart();  
    }

    /// <<Summary>>
    /// Moveable: Script makes thing move
    /// <<Summary>>
    public class Moveable : MonoBehaviour
    {
        readonly float moveSpeed = 20;
        /// 0->not moving 1->moving to middle 2->moving to destination
        int moveStatus;
        Vector3 destination;
        Vector3 middle;

        void Update() {
			if (moveStatus == 1) {
				transform.position = Vector3.MoveTowards (transform.position, middle, moveSpeed * Time.deltaTime);
				if (transform.position == middle) {
					moveStatus = 2;
				}
			} 
			else if (moveStatus == 2) {
				transform.position = Vector3.MoveTowards (transform.position, destination, moveSpeed * Time.deltaTime);
				if (transform.position == destination) {
					moveStatus = 0;
				}
			}
		}
		public void setDestination(Vector3 d) {
			destination = d;
			middle = d;
			/// Moving Boat
			if (d.y == transform.position.y) {	
				moveStatus = 2;
			}
			/// Move character from coast to boat
			else if (d.y < transform.position.y) {
				middle.y = transform.position.y;
			} 
			/// Move character from boat to coast
			else {
				middle.x = transform.position.x;
			}
			moveStatus = 1;
		}

		public void reset() {
			moveStatus = 0;
		}
    }

    /// <<Summary>>
    /// CoastController
    /// <<Summary>>
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

    /// <<Summary>>
    /// BoatController
    /// <<Summary>>
    public class BoatController
    {
        readonly GameObject boat;
		readonly Moveable moveScript;
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

			moveScript = boat.AddComponent (typeof(Moveable)) as Moveable;
			boat.AddComponent (typeof(Click));
		}

		public void Move() {
			if (direction == "End") {
				moveScript.setDestination(startPosition);
				direction = "Start";
			} 
			else {
				moveScript.setDestination(endPosition);
				direction = "End";
			}
		}

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

		public GameObject getGameobj() {
			return boat;
		}

		public string getDirection() {
			return direction;
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

		public void reset() {
			moveScript.reset ();
			if (direction == "End") {
				Move ();
			}
			passenger = new myCharacterController[2];
		}
    }

    /// <<Summary>>
    /// CharacterController
    /// <<Summary>>
    public class myCharacterController
    {
        readonly GameObject character;
		readonly Moveable moveScript;
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
			moveScript = character.AddComponent (typeof(Moveable)) as Moveable;

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

		public void moveTo(Vector3 destination) {
			moveScript.setDestination(destination);
		}

		public int getType() {
			return characterType;
		}

		public string getName() {
			return character.name;
		}

        public string getTag() {
			return character.tag;
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
			moveScript.reset ();
			coastController = (Director.getInstance ().currentSceneController as FirstController).startCoast;
			getOnCoast (coastController);
			setPosition (coastController.getEmptyPosition ());
			coastController.getOnCoast (this);
		}

    }

    

}