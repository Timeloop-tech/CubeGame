using UnityEngine;
using System.Collections;

public class Base : View<MainApplcation> {

	public BoxCollider2D coll ;

	void Start(){
		coll = coll.GetComponent<BoxCollider2D> ();
	}

	void OnCollisionEnter2D () {

		if (! GameModel.GodsMode) {
			Notify (Notifications.BASE_TOUCHED);
		}
	}

	void Update() {
		if (! GameModel.GodsMode) {
			coll.isTrigger = false;
		} else {
			coll.isTrigger = true;
		}
	}
}
