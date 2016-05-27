using UnityEngine;
using System.Collections;

public class GameView : View<MainApplcation> {

	void Start(){
		gameObject.AddComponent<TouchView> ();
	}

}
