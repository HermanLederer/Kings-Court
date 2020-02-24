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
	[SerializeField] private List<GameTeam> teams;

	//--------------------------
	// AICombat methods
	//--------------------------
	public void Register(GameTeam team)
	{
		teams.Add(team);
	}

	public void Eliminate(GameTeam team)
	{
		teams.Remove(team);
		if (teams.Count <= 1) DecalreWinnder(teams[0]);
	}

	public void DecalreWinnder(GameTeam team)
	{
		Debug.Log("Team " + team + " won!");
		Time.timeScale = 0;
	}
}

public enum GameTeam
{
	Amar,
	Herman,
	Ray
}