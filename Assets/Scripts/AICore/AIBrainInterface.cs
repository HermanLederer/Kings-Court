using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICore
{
	/// <summary>
	/// AIBrainInterface is used by players to manipulate the entity
	/// </summary>
	[RequireComponent(typeof(AIPerceptionBrain))]
	[RequireComponent(typeof(AICombatBrain))]
	public class AIBrainInterface : MonoBehaviour
	{
		// Other components
		private AIPerceptionBrain perceptionBrain;
		private AICombatBrain combatBrain;
		private CharacterController characterController;
		private NavMeshAgent navMeshAgent;

		// Editor variables
		[SerializeField] public GameTeam team;
		[SerializeField] public AIType type;

		//--------------------------
		// MonoBehaviour methods
		//--------------------------
		void Awake()
		{
			perceptionBrain = GetComponent<AIPerceptionBrain>();
			characterController = GetComponent<CharacterController>();
			combatBrain = GetComponent<AICombatBrain>();
			navMeshAgent = GetComponent<NavMeshAgent>();
		}

		private void Start()
		{
			GameManager.instance.Register(team); // registering the team in game manager
		}

		void Update()
		{
			//float distance = Vector3.Distance(target.position, transform.position);
			//if(distance <= lookRadius)
			//{
			//	agent.SetDestination(target.position);
			//	if(distance <= agent.stoppingDistance)
			//	{
			//		AIStats targetStats = target.GetComponent<AIStats>();
			//		if (targetStats != null)
			//		{
			//			combat.Attack(targetStats);
			//		}
			//		FaceTarget();
			//	}
			//}
		}

		private void OnDrawGizmos()
		{
			if (Application.isPlaying)
			{
				foreach (AIEntity entity in perceptionBrain.GetVisibleAIEntities())
				{
					Gizmos.color = Color.white;
					Gizmos.DrawLine(transform.position, entity.transform.position);
				}
			}
		}

		//--------------------------
		// AIBrainInterface methods
		//--------------------------
		public void SetDestination(Vector3 target)
		{
			navMeshAgent.SetDestination(target);
		}

		public List<AIEntity> GetVisibleAIEntities()
		{
			return perceptionBrain.GetVisibleAIEntities();
		}
	}
}
