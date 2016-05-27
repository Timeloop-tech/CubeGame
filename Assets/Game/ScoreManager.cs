using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : Element<MainApplcation> {

	public static int Score;

	Text text;
	
	// Use this for initialization
	void Start () {

		text = GetComponent<Text> ();
		Score = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		Score = app.model.TotalScore;
		text.text = "Score: " + Score;

		if (GameModel.StopPlay) {
			text.text = "Score: 0";
		}
	}
}
