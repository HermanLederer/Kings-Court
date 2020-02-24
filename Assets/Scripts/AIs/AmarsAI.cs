using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmarsAI : PlayerAI
{
	// Editor variables

	// Public variables
	public float lookRadius = 20;

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
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

	//--------------------------
	// AmarsAI methods
	//--------------------------
}
