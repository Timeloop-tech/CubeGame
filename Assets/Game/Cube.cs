using UnityEngine;
using System.Collections;

public class Cube : View<MainApplcation> {

	BoxCollider2D physicsBody;
	Rigidbody2D rigidBody ;

	public void Start() {
		physicsBody = GetComponent<BoxCollider2D> ();
		rigidBody = GetComponent<Rigidbody2D> ();

		if (GameModel.GodsMode) {
			rigidBody.gravityScale = Constants.SPEED_ON;
		} else {
			rigidBody.gravityScale = Constants.SPEED_OFF;
		}
	}

	public void Update() {
		cubeTouched ();
	}

	Vector2 firstPressPos_sc = Vector2.zero;
	Vector2 secondPressPos_sc = Vector2.zero;

	void cubeTouched () {

		if (!isMobile) {
			if (Input.GetMouseButton (0)) {
				Vector3 wp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				Vector2 touchPos = new Vector2 (wp.x, wp.y);
				if (physicsBody == Physics2D.OverlapPoint (touchPos)) {
					Notify (Notifications.CUBE_TOUCHED, gameObject);
				}
			}

			if (Input.GetMouseButtonDown (0)) {
				//save began touch 2d point
				firstPressPos_sc = Input.mousePosition;
			}
			if (Input.GetMouseButtonUp (0)) {
				//save began touch 2d point
				secondPressPos_sc = Input.mousePosition;
				if ((DistanceBetweenPoints (firstPressPos_sc, secondPressPos_sc) <= Constants.TOUCH_PIXEL)) {

					Vector3 wp_te = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					Vector2 touchPos_te = new Vector2 (wp_te.x, wp_te.y);
					if (physicsBody == Physics2D.OverlapCircle (touchPos_te,0.1f)) {
						//Debug.Log("touch ended on cube");
						Notify (Notifications.CUBE_TOUCHED_ONTOUCH_ENDED, gameObject);
					}

				}
			}

		}
		else {
			
			if (Input.touches.Length > 0) {
				Vector3 wp = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				Vector2 touchPos = new Vector2 (wp.x, wp.y);

				if (physicsBody == Physics2D.OverlapPoint (touchPos)) {
	
					Notify (Notifications.CUBE_TOUCHED, gameObject);
				}


				if(Input.GetTouch(0).phase == TouchPhase.Ended) {
					Vector3 wp_te = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
					Vector2 touchPos_te = new Vector2 (wp_te.x, wp_te.y);

					if (physicsBody == Physics2D.OverlapPoint (touchPos_te)) {
						Debug.Log( " pixels : " + Physics2D.OverlapCircle (touchPos_te,1.0f));
						Notify (Notifications.CUBE_TOUCHED_ONTOUCH_ENDED, gameObject);
					}
				}
			}
		}
	}

	float DistanceBetweenPoints ( Vector2 PointA , Vector2 PointB) {
		
		return Mathf.Sqrt (((PointA.x - PointB.x) * (PointA.x - PointB.x)) + ((PointA.y - PointB.y) * (PointA.y - PointB.y)));
		
	}
}
