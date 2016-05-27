using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel : Model<MainApplcation> {

	
	public int TotalScore = 0;
	public int Level = 0;
	
	public List<int> ProbGold;
	public List<int> ProbBlack;
	public List<int> ProbWhite;

	/*List<int> ProbGold = new List<int> {0,0,1,1,2,2,2,2,2,2};
	List<int> ProbBlack = new List<int> {0,1,1,2,2,3,3,3,3,3};
	List<int> ProbWhite = new List<int> {10,9,8,7,6,5,5,5,5,5};*/
	
	public List<float> CubeTime;
	public List<int> Score;

	// CubeType i.e.
	// 1. White
	// 2. Black
	// 3. Golden
	public int CubeType = Constants.CUBE_ONE;
	public bool CubeGenerated = true;

	public int Random1to10 = 1;
	public int RandomLane = Constants.LANE_ONE;

	public static bool StartPlay = false;
	public static bool StopPlay = false;

	public static bool GameOver = false;

	public static bool GodsMode = false;

	void Start() {

		TotalScore = 0;
		Level = Constants.START_LEVEL;

		ProbGold = new List<int> {0,0,10,10,10,10,10,10,10,10};
		ProbBlack = new List<int> {0,10,9,9,8,8,8,8,8,8};
		ProbWhite = new List<int> {10,9,8,7,6,5,5,5,5,5};

		CubeTime = new List<float> {1.5f,1.4f,1.3f,1.2f,1.1f,1.0f,1.0f,1.0f,1.0f,1.0f};

		Score = new List<int> { 10,20,30,40,50,60,70,80,90,100 };

		CubeType = Constants.CUBE_ONE;
		CubeGenerated = true;

		Random1to10 = 1;
		RandomLane = Constants.LANE_ONE;

		GodsMode = false;
	}
}
