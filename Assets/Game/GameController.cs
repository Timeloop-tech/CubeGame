using UnityEngine;
using System.Collections;

public class GameController : Controller<MainApplcation> {

	void Start() {

		app.addController (this);

		app.model.TotalScore = 0;
		app.model.Level = Constants.START_LEVEL;

		//Debug.Log (" Score : " + app.model.TotalScore);
	}

	void Update() {

		if (GameModel.StartPlay) {
			StartCoroutine (CubeStart ());
			app.model.TotalScore = 0;
			app.model.Level = 0 ;
			GameModel.StartPlay = false;
			GameModel.StopPlay = false;
		}

		if (GameModel.StopPlay || GameModel.GameOver) {
			int index;
			//app.model.TotalScore = 0 ;
			index = CreateGameObject.upperLeft.transform.childCount;
			for ( int i = 0; i < index; i++) {
				Transform t = CreateGameObject.upperLeft.transform.GetChild(i);
				Destroy(t.gameObject);
			}

			index = CreateGameObject.upperMiddle.transform.childCount;
			for ( int i = 0; i < index; i++) {
				Transform t = CreateGameObject.upperMiddle.transform.GetChild(i);
				Destroy(t.gameObject);
			}

			index = CreateGameObject.upperRight.transform.childCount;
			for ( int i = 0; i < index; i++) {
				Transform t = CreateGameObject.upperRight.transform.GetChild(i);
				Destroy(t.gameObject);
			}

			GameModel.StopPlay = false;
			StopAllCoroutines();
		}

		if (GameModel.GameOver) {
			GameModel.StopPlay = true;
			Notify(Notifications.GAME_OVER);
			GameModel.GameOver = false;
		}
	}

	IEnumerator CubeStart () {

		yield return new WaitForSeconds ((app.model.CubeTime [app.model.Level]));

		app.model.Random1to10 = Random.Range (1, 11);
		app.model.RandomLane = Random.Range (1, 4);

		if (app.model.Random1to10 <= app.model.ProbWhite [app.model.Level]) {
			// Generate White Cube.
			app.model.CubeType = Constants.CUBE_ONE;
		} else if (app.model.Random1to10 > app.model.ProbWhite [app.model.Level] && app.model.Random1to10 <= app.model.ProbBlack [app.model.Level]) {
			// Generate Black Cube.
			app.model.CubeType = Constants.CUBE_TWO;
		} else {
			// Generate Golden Cube.
			app.model.CubeType = Constants.CUBE_THREE;		
		}

		if ( app.model.CubeGenerated == true) {
			app.model.CubeGenerated = false;
		}


		if (app.model.TotalScore >= app.model.Score [app.model.Level] && app.model.Level <9) {
			app.model.Level++;
		}

		if (!GameModel.StopPlay) {
			StartCoroutine (CubeStart ());
		}
	}

	string lastEvent = "";
	object lastTouchedCube;

	public override void OnNotification(string p_event, Object p_target, params object[] p_data) { 
	
		if((p_event == Notifications.CUBE_TOUCHED || p_event == Notifications.CUBE_TOUCHED_ONTOUCH_ENDED))
		{
			lastTouchedCube = p_data[0];
		}
	
		GameObject cubeTouched = null;

		if (lastTouchedCube != null) {
			cubeTouched = lastTouchedCube as GameObject;
		}

		/*if (p_event != "cube.generated" && p_event != "mouse.down" && p_event != "mouse.up" ) {
			Debug.Log(" event : " + p_event);
		}*/

		if (cubeTouched != null) {
			if ((p_event == Notifications.SWIPE_LEFT || p_event == Notifications.SWIPE_RIGHT || p_event == Notifications.SWIPE_UP || p_event == Notifications.SWIPE_DOWN) && (lastEvent == Notifications.CUBE_TOUCHED || lastEvent == Notifications.CUBE_TOUCHED_ONTOUCH_ENDED)) {
				//delete the cube
				if (cubeTouched.tag == "GoldenTag") {
					Destroy (cubeTouched);
					Notify (Notifications.CUBE_DESTROYED);
					app.model.TotalScore += Constants.GOLD_SCORE;
					//Debug.Log (" Level : " + app.model.Level + " Score : " + app.model.TotalScore);

					p_event = "";
					lastEvent = "";
				}
				cubeTouched = null;
				lastTouchedCube = null;
			} else if ((p_event == Notifications.DOUBlE_TAP && (lastEvent == Notifications.CUBE_TOUCHED || lastEvent == Notifications.CUBE_TOUCHED_ONTOUCH_ENDED)) || ((p_event == Notifications.CUBE_TOUCHED || p_event == Notifications.CUBE_TOUCHED_ONTOUCH_ENDED) && lastEvent == Notifications.DOUBlE_TAP)) {
				if (cubeTouched.tag == "BlackTag") {
					Destroy (cubeTouched);
					Notify (Notifications.CUBE_DESTROYED);
					app.model.TotalScore += Constants.BLACK_SCORE;
					//Debug.Log (" Level : " + app.model.Level + " Score : " + app.model.TotalScore);
					p_event = "";
					lastEvent = "";
				}
				cubeTouched = null;
				lastTouchedCube = null;
			}else if (p_event == Notifications.CUBE_TOUCHED_ONTOUCH_ENDED) {
				if (cubeTouched.tag == "WhiteTag") {
					Destroy (cubeTouched);
					Notify (Notifications.CUBE_DESTROYED);
					app.model.TotalScore += Constants.WHITE_SCORE;
					//Debug.Log (" Level : " + app.model.Level + " Score : " + app.model.TotalScore);
					p_event = "";
					lastEvent = "";
				}
				lastTouchedCube = null;
				cubeTouched = null;
			}
		}

		switch (p_event) {

		case Notifications.MOUSE_DOWN:

			//lastEvent = p_event;

			break;

		case Notifications.SWIPE_LEFT:

			lastEvent = p_event;

			break;

		case Notifications.SWIPE_RIGHT:

			lastEvent = p_event;

			break;

		case Notifications.SWIPE_UP:

			lastEvent = p_event;

			break;

		case Notifications.SWIPE_DOWN:
			
			lastEvent = p_event;

			break;

		case Notifications.CUBE_TOUCHED:
			lastEvent = p_event;
			//lastTouchedCube = p_data [0];

			break;

		case Notifications.CUBE_TOUCHED_ONTOUCH_ENDED:
			lastEvent = p_event;
			//lastTouchedCube = p_data[0];

			break;

		case Notifications.MOUSE_UP:

			break;

		case Notifications.SINGLE_TAP:
			//lastEvent = p_event;

			break;

		case Notifications.DOUBlE_TAP:
			lastEvent = p_event;

			break;

		case Notifications.CUBE_GENERATED:

			app.model.CubeGenerated = true;

			break;

		case Notifications.CUBE_DESTROYED:

			break;

		case Notifications.BASE_TOUCHED:

			GameModel.GameOver = true;

			break;
		}
	}

}