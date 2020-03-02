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
		[SerializeField] public AITeam team;
		[SerializeField] public AIType type;

		// Private variables
		public float stun { get; private set; }

		//--------------------------
		// MonoBehaviour methods
		//--------------------------
		void Awake()
		{
			perceptionBrain = GetComponent<AIPerceptionBrain>();
			characterController = GetComponent<CharacterController>();
			combatBrain = GetComponent<AICombatBrain>();
			navMeshAgent = GetComponent<NavMeshAgent>();

			stun = 0;
		}

		private void Start()
		{
			GameManager.instance.Register(team); // registering the team in game manager
		}

		private void OnDrawGizmos()
		{
			if (Application.isPlaying)
			{
				foreach (AIBrainInterface entityInterface in perceptionBrain.GetVisibleAIEntities())
				{
					Gizmos.color = Color.white;
					Gizmos.DrawLine(transform.position, entityInterface.transform.position);
				}
			}
		}

		//--------------------------
		// AIBrainInterface methods
		//--------------------------
		public void Stun(float time)
		{
			SetDestination(transform.position);
			stun = Time.time + time;
		}

		public void SetDestination(Vector3 target)
		{
			if (Time.time > stun) navMeshAgent.SetDestination(target);
		}

		public List<AIBrainInterface> GetVisibleAIEntities()
		{
			return perceptionBrain.GetVisibleAIEntities();
		}

		public void TakeDamage()
		{
			// not receiving damage if not target
			if (type != AIType.target) return;

			//currentHealth--;
			//Debug.Log(transform.name + " was hit");

			//if (currentHealth <= 0)
			//{
				Die();
			//}
		}

		public virtual void Die()
		{
			Debug.Log(transform.name + " died");
			GameManager.instance.Eliminate(team);
			Destroy(transform.parent.gameObject);
		}

	}
}
