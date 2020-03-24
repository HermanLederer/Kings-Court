using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All players must inherit from this class
/// PlayerAI contains references to all their team members
/// </summary>
public class PlayerAI : MonoBehaviour
{
	// Other components variables
	protected AICore.AIBrainInterface target;
	protected AICore.AIBrainInterface assassin;
	protected AICore.AIBrainInterface stunner;

	// Editor variables
	[SerializeField] protected AICore.AITeam team;
	[Header("Prefabs")]
	[SerializeField] protected GameObject targetPrefab;
	[SerializeField] protected GameObject assassinPrefab;
	[SerializeField] protected GameObject stunnerPrefab;

	// Private variables
	private Vector3 spawnPosition;
	protected AICore.AIBrainInterface[] members;

	// Public
	public bool isEliminated { get; protected set; }

	protected void Awake()
	{
		// instantiating prefabs
		target = Instantiate(targetPrefab, transform.position + Vector3.right * -2f, transform.rotation, transform).GetComponent<AICore.AIBrainInterface>();
		assassin = Instantiate(assassinPrefab, transform.position, transform.rotation, transform).GetComponent<AICore.AIBrainInterface>();
		stunner = Instantiate(stunnerPrefab, transform.position + Vector3.right * 2f, transform.rotation, transform).GetComponent<AICore.AIBrainInterface>();

		// assinging the teams
		target.team = team;
		assassin.team = team;
		stunner.team = team;

		isEliminated = false;
		spawnPosition = transform.position;

		// creating a members array for ease of use
		members = new AICore.AIBrainInterface[3];
		members[0] = target;
		members[1] = assassin;
		members[2] = stunner;
	}

	protected void Start()
	{
		// registering the team in game manager
		Debug.Log(team);
		GameManager.instance.Register(team);
	}

	public void Eliminate()
	{
		GameManager.instance.Eliminate(team);
		isEliminated = true;

		target.Burn();
		assassin.Burn();
		stunner.Burn();
	}
}
