using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaysAI : PlayerAI
{
	// Other components

	// Editor variables

	// Public variables

	// Private variables
	private AICore.AIBrainInterface[] team;

	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	void Awake()
	{
		team = new AICore.AIBrainInterface[3];
		team[0] = target;
		team[1] = assassin;
		team[2] = stunner;
	}

	void Start()
	{
		
	}

	void Update()
	{
		
	}
	//--------------------------
	// RaysAI methods
	//--------------------------
}
