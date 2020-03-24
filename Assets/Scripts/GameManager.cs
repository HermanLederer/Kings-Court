using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton
	public static GameManager instance;
	#endregion

	// Editor variables
	[SerializeField] private GameUI gameUI;

	// Private variables
	private List<AICore.AITeam> teams;

	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	void Awake()
	{
		instance = this;

		teams = new List<AICore.AITeam>();
	}

	//--------------------------
	// AICombat methods
	//--------------------------
	public void Register(AICore.AITeam team)
	{
		if (!teams.Contains(team))
		{
			teams.Add(team);
			Debug.Log("Team " + team + " registered");
		}
	}

	public void Eliminate(AICore.AITeam team)
	{
		if (teams.Contains(team))
		{
			gameUI.Eliminate(team);
			Debug.Log("Team " + team + " eliminated");
			teams.Remove(team);
			if (teams.Count == 1) DecalreWinnder(teams[0]);
		}
	}

	public void DecalreWinnder(AICore.AITeam team)
	{
		gameUI.DeclareWinner(team);
	}
}