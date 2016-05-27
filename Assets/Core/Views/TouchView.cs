using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//<summery>
//base class for all the view that require touch related features
//</summery>

public class TouchView : View{ 
	
	//inside class
	
	int ClickCount = 0 ;

	public const float maxTimer = 0.3f;

	public float minSwipeLength;

	public void Start() {
		Sprite cube = Resources.Load<Sprite> ("BlackCube");
		float xSize = cube.bounds.size.x * Constants.PIXEL_PER_UNIT;
		minSwipeLength = CreateGameObject.scaleFactor * xSize;
	}

	public void Update() {
		Swipe ();
		SingleClick ();
		DoubleClick ();
	}

	Vector2 firstPressPos_sw = Vector2.zero;
	Vector2 secondPressPos_sw = Vector2.zero;
	Vector2 currentSwipe_sw = Vector2.zero;
	
	public void Swipe()
	{
		float Magnitude; 

		if (!isMobile) {
			if (Input.GetMouseButtonDown (0)) {
				//save began touch 2d point
				firstPressPos_sw = Input.mousePosition;
				Notify (Notifications.MOUSE_DOWN, this);
			}

			if (Input.GetMouseButtonUp (0)) {
				//save ended touch 2d point
				secondPressPos_sw = Input.mousePosition;
			
				//create vector from the two points
				currentSwipe_sw = new Vector2 (secondPressPos_sw.x - firstPressPos_sw.x, secondPressPos_sw.y - firstPressPos_sw.y);

				//magnitude of 2d vector i.e. currentSwipe_sw
				Magnitude = DistanceBetweenPoints ( currentSwipe_sw, Vector2.zero);
				//normalize the 2d vector
				currentSwipe_sw.Normalize ();

				if ( Magnitude >= minSwipeLength ) {
				
					//swipe upwards
					if (currentSwipe_sw.y > 0 && currentSwipe_sw.x > -0.707f && currentSwipe_sw.x < 0.707f) {
						Notify (Notifications.SWIPE_UP, this);
					}
					//swipe down
					if (currentSwipe_sw.y < 0 && currentSwipe_sw.x > -0.707f && currentSwipe_sw.x < 0.707f) {
						Notify (Notifications.SWIPE_DOWN, this);
					}
					//swipe left
					if (currentSwipe_sw.x < 0 && currentSwipe_sw.y > -0.707f && currentSwipe_sw.y < 0.707f) {
						Notify (Notifications.SWIPE_LEFT, this);
					}
					//swipe right
					if (currentSwipe_sw.x > 0 && currentSwipe_sw.y > -0.707f && currentSwipe_sw.y < 0.707f) {
						Notify (Notifications.SWIPE_RIGHT, this);
					}

					Notify (Notifications.MOUSE_UP, this);
				}
			}
		}
		else
		{
			if (Input.touches.Length > 0) {
				Touch touch = Input.GetTouch (0);

				if (touch.phase == TouchPhase.Began) {
					//save began touch 2d point
					firstPressPos_sw = touch.position;
					Notify (Notifications.MOUSE_DOWN, this);

				} 
				else if (touch.phase == TouchPhase.Ended) {
				//save ended touch 2d point
					secondPressPos_sw = touch.position;

					//create vector from the two points
					currentSwipe_sw = new Vector2 (secondPressPos_sw.x - firstPressPos_sw.x, secondPressPos_sw.y - firstPressPos_sw.y);

					//magnitude of 2d vector i.e. currentSwipe_sw
					Magnitude = DistanceBetweenPoints ( currentSwipe_sw, Vector2.zero);

					//normalize the 2d vector
					currentSwipe_sw.Normalize ();

					if ( Magnitude >= minSwipeLength ) {
						//swipe upwards
						if (currentSwipe_sw.y > 0 && currentSwipe_sw.x > -0.707f && currentSwipe_sw.x < 0.707f) {
							Notify (Notifications.SWIPE_UP, this);
						}
						//swipe down
						if (currentSwipe_sw.y < 0 && currentSwipe_sw.x > -0.707f && currentSwipe_sw.x < 0.707f) {
							Notify (Notifications.SWIPE_DOWN, this);
						}
						//swipe left
						if (currentSwipe_sw.x < 0 && currentSwipe_sw.y > -0.707f && currentSwipe_sw.y < 0.707f) {
							Notify (Notifications.SWIPE_LEFT, this);
						}
						//swipe right
						if (currentSwipe_sw.x > 0 && currentSwipe_sw.y > -0.707f && currentSwipe_sw.y < 0.707f) {
							Notify (Notifications.SWIPE_RIGHT, this);
						}
					}

					Notify (Notifications.MOUSE_UP, this);
				}
					

			}
		}
	}

	//In-case we need SingleClick Event separately.


	Vector2 firstPressPos_sc = Vector2.zero;
	Vector2 secondPressPos_sc = Vector2.zero;

	public void SingleClick () {

		if (!isMobile) {
			if (Input.GetMouseButtonDown (0)) {
				//save began touch 2d point
				firstPressPos_sc = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			}
			if (Input.GetMouseButtonUp (0)) {
				//save began touch 2d point
				secondPressPos_sc = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				if ((DistanceBetweenPoints (firstPressPos_sc, secondPressPos_sc) <= Constants.TOUCH_PIXEL)) {
					//Debug.Log ("Single Click");
					Notify (Notifications.SINGLE_TAP, this);
				}
			}
		} else {
			if (Input.touches.Length > 0) {
				Touch touch = Input.GetTouch (0);
			
				if (touch.phase == TouchPhase.Began) {
					firstPressPos_sc = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				} else if(touch.phase == TouchPhase.Ended) {
					secondPressPos_sc = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					if ((DistanceBetweenPoints (firstPressPos_sc, secondPressPos_sc) <= Constants.TOUCH_PIXEL)) {
						//Debug.Log ("Single Click");
						Notify (Notifications.SINGLE_TAP, this);
					}
				}
			}
		}
	}

	
	Vector2 firstPressPos_dc = Vector2.zero;
	Vector2 secondPressPos_dc = Vector2.zero;
	Vector2 thirdPressPos_dc = Vector2.zero;
	Vector2 fourthPressPos_dc = Vector2.zero;
	float startTime;
	float endTime;

	public void DoubleClick() {

		if (!isMobile) {

			if (ClickCount == 0) {

				if (Input.GetMouseButtonDown (0)) {
					firstPressPos_dc = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				}

				if (Input.GetMouseButtonUp (0)) {
					secondPressPos_dc = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					if (DistanceBetweenPoints (firstPressPos_dc, secondPressPos_dc) <= Constants.TOUCH_PIXEL) {
						ClickCount ++;
						startTime = (float)Time.time;
					}
				}
			} else if (ClickCount == 1) {

				endTime = Time.time - startTime;

				if (endTime >= maxTimer) {

					ClickCount = 0;

					//Notify (Notifications.SINGLE_TAP, this);
				} else {

					if (Input.GetMouseButtonDown (0)) {
						thirdPressPos_dc = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
						endTime = (float)Time.time;
					}

					if (Input.GetMouseButtonUp (0)) {

						fourthPressPos_dc = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

						if (DistanceBetweenPoints (thirdPressPos_dc, fourthPressPos_dc) <= Constants.TOUCH_PIXEL) {
							ClickCount = 0;
							//Debug.Log(" Double click");
							Notify (Notifications.DOUBlE_TAP, this);
						} else {
							ClickCount = 0;
						//	Notify (Notifications.SINGLE_TAP, this);
						}
					}
				}
			}
		} else {
			if (Input.touches.Length > 0) {
				Touch touch = Input.GetTouch (0);

				if (ClickCount == 0) {
					if (touch.phase == TouchPhase.Began) {
						firstPressPos_dc = touch.position;
					}
					if (touch.phase == TouchPhase.Ended) {
						secondPressPos_dc = touch.position;
						if (DistanceBetweenPoints (firstPressPos_dc, secondPressPos_dc) <= Constants.TOUCH_PIXEL) {
							ClickCount ++;
							startTime = (float)Time.time;
						}
					}
				} else if (ClickCount == 1) {
					endTime = Time.time - startTime;
					
					if (endTime >= maxTimer) {
						ClickCount = 0;
						Notify (Notifications.SINGLE_TAP, this);
					} else {
						if (touch.phase == TouchPhase.Began) {
							thirdPressPos_dc = touch.position;
							endTime = (float)Time.time;
						}
						if (touch.phase == TouchPhase.Ended) {
							fourthPressPos_dc = touch.position;
							
							if (DistanceBetweenPoints (thirdPressPos_dc, fourthPressPos_dc) <= Constants.TOUCH_PIXEL) {
								ClickCount = 0;
								Notify (Notifications.DOUBlE_TAP, this);
							} else {
								ClickCount = 0;
								Notify (Notifications.SINGLE_TAP, this);
							}
						}
					}
				}
			}
		}
	}

	float DistanceBetweenPoints ( Vector2 PointA , Vector2 PointB) {

		return Mathf.Sqrt (((PointA.x - PointB.x) * (PointA.x - PointB.x)) + ((PointA.y - PointB.y) * (PointA.y - PointB.y)));

	}

}


/// <summary>
/// base class for all the view that require touch
/// </summary>
public class TouchView<T> : TouchView where T : BaseApplication
{
	new public T app { get { return (T)base.app; } }
}