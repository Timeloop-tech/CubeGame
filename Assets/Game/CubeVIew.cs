using UnityEngine;
using System.Collections;

public class CubeVIew : Element<MainApplcation> {

	public Cube cube_black;
	public Cube cube_white;
	public Cube cube_golden;

	float offSet;
	
	const int numberOfCubes = 1;
	int cubeCount = 0;

	// Use this for initialization
	void Start () {

		Sprite cube = Resources.Load<Sprite> ("BlackCube");
		float xSize = cube.bounds.size.x;
		float ySize = cube.bounds.size.y;

		BoxCollider2D collider = cube_black.GetComponent<BoxCollider2D> ();
		collider.size = new Vector2 ( xSize + ( 0.2f * xSize ) , ySize +( 0.2f * ySize ) );

		collider = cube_white.GetComponent<BoxCollider2D> ();
		collider.size = new Vector2 ( xSize + ( 0.2f * xSize ) , ySize +( 0.2f * ySize ) );

		collider = cube_golden.GetComponent<BoxCollider2D> ();
		collider.size = new Vector2 ( xSize + ( 0.2f * xSize ) , ySize +( 0.2f * ySize ) );

		offSet = ((CreateGameObject.referenceWidth / (4.0f * Constants.PIXEL_PER_UNIT)) - (xSize / 2.0f)) * CreateGameObject.scaleFactor;
		Debug.Log (offSet);

//		Sprite mySprite = cube.GetComponent<SpriteRenderer>().sprite;
//		float pixel2units = mySprite.rect.width / mySprite.bounds.size.x;
	}
	
	void Update() {

		if (app.model.CubeGenerated == false) {
			if (app.model.RandomLane == Constants.LANE_ONE) {
				StartCoroutine (generateLeftCubes (app.model.CubeType));
			} else if (app.model.RandomLane == Constants.LANE_TWO) {
				StartCoroutine (generateCenterCubes (app.model.CubeType));
			} else {
				StartCoroutine (generateRightCubes (app.model.CubeType));
			}
		}
	}


	IEnumerator generateLeftCubes( int type ) {

		Notify (Notifications.CUBE_GENERATED, this);

		Cube tempCube;

		if (type == Constants.CUBE_ONE) {
			tempCube = Instantiate ( cube_white , Vector3.zero ,Quaternion.identity) as Cube;
			tempCube.name = "WhiteCube " + cubeCount.ToString();
		} else if (type == Constants.CUBE_TWO) {
			tempCube = Instantiate (cube_black, Vector3.zero ,Quaternion.identity) as Cube;
			tempCube.name = "BlackCube " + cubeCount.ToString();
		} else {
			tempCube = Instantiate (cube_golden, Vector3.zero ,Quaternion.identity) as Cube;
			tempCube.name = "GoldenCube " + cubeCount.ToString();
		}

		cubeCount++;
		tempCube.transform.parent = CreateGameObject.upperLeft.transform;

		var bounds = tempCube.GetComponent<SpriteRenderer> ().sprite.bounds;
		float xSize = bounds.size.x / 2 ;

		tempCube.transform.localScale = Vector3.one;
		tempCube.transform.localPosition = new Vector3 (xSize, 0.0f, 0.0f) + new Vector3 (offSet,0.0f,0.0f);

		yield return new WaitForEndOfFrame ();
		//yield return new WaitForSeconds( app.model.CubeTime [ app.model.Level ] );

		//StartCoroutine (generateLeftCubes(app.model.CubeType));
	}

	IEnumerator generateCenterCubes(int type) {

		Notify (Notifications.CUBE_GENERATED, this);

		Cube tempCube;

		if (type == Constants.CUBE_ONE) {
			tempCube = Instantiate ( cube_white , Vector3.zero ,Quaternion.identity) as Cube;
			tempCube.name = "WhiteCube " + cubeCount.ToString();
		} else if (type == Constants.CUBE_TWO) {
			tempCube = Instantiate (cube_black, Vector3.zero ,Quaternion.identity) as Cube;
			tempCube.name = "BlackCube " + cubeCount.ToString();
		} else {
			tempCube = Instantiate (cube_golden, Vector3.zero ,Quaternion.identity) as Cube;
			tempCube.name = "GoldenCube " + cubeCount.ToString();
		}
		
		cubeCount++;

		tempCube.transform.parent = CreateGameObject.upperMiddle.transform;

		tempCube.transform.localScale = Vector3.one;
		tempCube.transform.localPosition = Vector3.zero;

		yield return new WaitForEndOfFrame ();

		//yield return new WaitForSeconds( app.model.CubeTime [ app.model.Level ] );

		//StartCoroutine (generateCenterCubes(app.model.CubeType));
	}

	IEnumerator generateRightCubes(int type) {

		Notify (Notifications.CUBE_GENERATED, this);

		Cube tempCube;
		
		if (type == Constants.CUBE_ONE) {
			tempCube = Instantiate ( cube_white , Vector3.zero ,Quaternion.identity) as Cube;
			tempCube.name = "WhiteCube " + cubeCount.ToString();
		} else if (type == Constants.CUBE_TWO) {
			tempCube = Instantiate (cube_black, Vector3.zero ,Quaternion.identity) as Cube;
			tempCube.name = "BlackCube " + cubeCount.ToString();
		} else {
			tempCube = Instantiate (cube_golden, Vector3.zero ,Quaternion.identity) as Cube;
			tempCube.name = "GoldenCube " + cubeCount.ToString();
		}
		
		cubeCount++;	

		tempCube.transform.parent = CreateGameObject.upperRight.transform;

		var bounds = tempCube.GetComponent<SpriteRenderer> ().sprite.bounds;
		float xSize = bounds.size.x / 2 ;

		tempCube.transform.localScale = Vector3.one;
		tempCube.transform.localPosition = new Vector3 (-xSize, 0.0f, 0.0f) - new Vector3 (offSet,0.0f,0.0f);

		yield return new WaitForEndOfFrame ();

		//yield return new WaitForSeconds( app.model.CubeTime [ app.model.Level ] );

		//StartCoroutine (generateRightCubes(app.model.CubeType));
	}
}