using UnityEngine;
using System.Collections;

public static class Notifications {

	public const string MOUSE_DOWN  = "mouse.down";
	public const string MOUSE_UP  = "mouse.up";

	public const string SWIPE_LEFT  = "swipe.left";
	public const string SWIPE_RIGHT = "swipe.right";
	public const string SWIPE_UP    = "swipe.up";
	public const string SWIPE_DOWN  = "swipe.down";

	public const string SINGLE_TAP = "single.tap"; 
	public const string DOUBlE_TAP = "double.tap";


	//Game related events
	public const string CUBE_TOUCHED_ONTOUCH_ENDED  = "cube.touched.touch.up";
	public const string CUBE_TOUCHED  = "cube.touched";
	public const string CUBE_GENERATED = "cube.generated";
	public const string CUBE_DESTROYED = "cube.destroyed";

	public const string BASE_TOUCHED = "base.touched";
	public const string GAME_OVER = "game.over";

}
