using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton
	public static GameManager instance;

	void Awake()
	{
		instance = this;
	}
	#endregion

	// 1 - Amar
	// 2 - Herman
	// 3 - Ray
	[SerializeField] private List<AICore.AITeam> teams;

	//--------------------------
	// AICombat methods
	//--------------------------
	public void Register(AICore.AITeam team)
	{
		if (!teams.Contains(team)) teams.Add(team);
	}

	public void Eliminate(AICore.AITeam team)
	{
		if (teams.Contains(team))
		{
			teams.Remove(team);
			if (teams.Count == 0) DecalreWinnder(teams[0]);
			//Debug.Log("" + teams.Count);}
		}
	}

	public void DecalreWinnder(AICore.AITeam team)
	{
		Debug.Log("Team " + team + " won!");
		//Time.timeScale = 0f;
	}
}