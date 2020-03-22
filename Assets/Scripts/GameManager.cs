using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Space(5)]
// -----Canvas for when a team dies-----
        public GameObject team1Down;
        public GameObject team2Down;
        public GameObject team3Down;

		AICore.AITeam currentTeam;

	#region Singleton
	public static GameManager instance;

	void Awake()
	{
		currentTeam = AICore.AITeam.Amar;
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
			Debug.Log("Team " + team + " eliminated");
			teams.Remove(team);
			if (teams.Count == 1) DecalreWinnder(teams[0]);
			
			if (currentTeam == AICore.AITeam.Amar)
			{
				team1Down.SetActive(true);
			}

			if (currentTeam == AICore.AITeam.Herman)
			{
				team2Down.SetActive(true);
			}

			if (currentTeam == AICore.AITeam.Ray)
			{
				team3Down.SetActive(true);
			}
		}
	}

	public void DecalreWinnder(AICore.AITeam team)
	{
		Debug.Log("Team " + team + " won!");
		//Time.timeScale = 0f;
	}
}