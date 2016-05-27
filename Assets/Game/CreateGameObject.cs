using UnityEngine;
using System.Collections;

public class CreateGameObject : MonoBehaviour {

	public static GameObject upperLeft;
	public static GameObject upperRight;
	public static GameObject upperMiddle;
	public static GameObject centerLeft;
	public static GameObject centerRight;
	public static GameObject centerMiddle;
	public static GameObject bottomLeft;
	public static GameObject bottomRight;
	public static GameObject bottomMiddle;

	public static float referenceWidth;
	public static float referenceHeight;

	public static float scaleFactor ;
	
	
	[Range(0, 1)]
	public int widthOrHeight = 0;

	// Use this for initialization
	void Start () {

		referenceWidth = 1536.0f;
		referenceHeight = 2048.0f;

		upperLeft = new GameObject ("upperLeft");
		upperLeft.transform.parent = gameObject.transform;

		upperRight = new GameObject ("upperRight");
		upperRight.transform.parent = gameObject.transform;

		upperMiddle = new GameObject ("upperMiddle");
		upperMiddle.transform.parent = gameObject.transform;

		centerLeft = new GameObject ("centerLeft");
		centerLeft.transform.parent = gameObject.transform;

		centerRight = new GameObject ("centerRight");
		centerRight.transform.parent = gameObject.transform;

		centerMiddle = new GameObject ("centerMiddle");
		centerMiddle.transform.parent = gameObject.transform;

		bottomLeft = new GameObject ("bottomLeft");
		bottomLeft.transform.parent = gameObject.transform;

		bottomRight = new GameObject ("bottomRight");
		bottomRight.transform.parent = gameObject.transform;

		bottomMiddle = new GameObject ("bottomMiddle");
		bottomMiddle.transform.parent = gameObject.transform;

		scaleFactor = ((Screen.width / referenceWidth) * (1.0f - widthOrHeight)) + ((Screen.height / referenceHeight) * (widthOrHeight));
		
		Debug.Log ("scale factor :: "+scaleFactor+ "camera height :: "+Screen.height / 200.0f);
		
		Camera.main.orthographicSize = Screen.height / 200.0f;
		
		Camera.main.transform.position = new Vector3 (Screen.width / 200.0f, Screen.height / 200.0f, -10.0f);
		
		//set scale
		//upperLeft.transform.localScale = new Vector3 (scaleFactor,scaleFactor,1.0f);
		upperLeft.transform.localScale = new Vector3 (scaleFactor, scaleFactor, 1.0f);
		upperMiddle.transform.localScale = new Vector3 (scaleFactor,scaleFactor,1.0f);
		upperRight.transform.localScale = new Vector3 (scaleFactor,scaleFactor,1.0f);
		bottomLeft.transform.localScale = new Vector3 (scaleFactor,scaleFactor,1.0f);
		bottomMiddle.transform.localScale = new Vector3 (scaleFactor,scaleFactor,1.0f);
		bottomRight.transform.localScale = new Vector3 (scaleFactor,scaleFactor,1.0f);
		centerMiddle.transform.localScale = new Vector3 (scaleFactor,scaleFactor,1.0f);
		centerLeft.transform.localScale = new Vector3 (scaleFactor,scaleFactor,1.0f);
		centerRight.transform.localScale = new Vector3 (scaleFactor,scaleFactor,1.0f);
		
		//set position
		upperLeft.transform.position = new Vector2 (0.0f, (Screen.height/100.0f));
		upperMiddle.transform.position = new Vector2 ((Screen.width/200.0f), (Screen.height/100.0f));
		upperRight.transform.position = new Vector2 ((Screen.width/100.0f), (Screen.height/100.0f));
		bottomLeft.transform.position = new Vector2 (0.0f,0.0f);
		bottomMiddle.transform.position = new Vector2 (Screen.width/200.0f,0.0f);
		bottomRight.transform.position = new Vector2 ((Screen.width/100.0f),0);
		centerMiddle.transform.position = new Vector2 (Screen.width / 200.0f, Screen.height / 200.0f);
		centerLeft.transform.position = new Vector2 (0.0f, Screen.height / 200.0f);
		centerRight.transform.position = new Vector2 (Screen.width / 100.0f, Screen.height / 200.0f);

	}

	// Update is called once per frame
	void Update () {
	
	}
}
