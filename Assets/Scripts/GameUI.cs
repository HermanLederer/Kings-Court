using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	// Editor variables
	[SerializeField] private Image lineAmar = null;
	[SerializeField] private Image lineHerman = null;
	[SerializeField] private Image lineRay = null;
	[SerializeField] private Text winner = null;

	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	void Start()
    {
        
    }

    void Update()
    {
        
    }

	//--------------------------
	// GameUI methods
	//--------------------------
	public void Eliminate(AICore.AITeam team)
	{
		if (team == AICore.AITeam.Amar)
			lineAmar.gameObject.SetActive(true);

		if (team == AICore.AITeam.Herman)
			lineHerman.gameObject.SetActive(true);

		if (team == AICore.AITeam.Ray)
			lineRay.gameObject.SetActive(true);
	}

	public void DeclareWinner(AICore.AITeam team)
	{
		winner.text = "Team " + team + " has won";
		winner.gameObject.SetActive(true);
	}
}
