using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All players must inherit from this class
/// PlayerAI contains references to all their team members
/// </summary>
public class PlayerAI : MonoBehaviour
{
	// Editor variables
	[SerializeField] protected AICore.AIControlBrain target;
	[SerializeField] protected AICore.AIControlBrain assasin;
	[SerializeField] protected AICore.AIControlBrain stunner;
}
