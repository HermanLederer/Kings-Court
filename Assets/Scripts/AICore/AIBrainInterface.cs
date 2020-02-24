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

		// Private variables
		[SerializeField] private int maxHealth = 3;
		public int currentHealth { get; private set; }
		private float stun;
		private bool isDead;

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
			isDead = false;
		}

		private void Start()
		{
			currentHealth = maxHealth;

			GameManager.instance.Register(team); // registering the team in game manager
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
		public void Stun(float time)
		{
			SetDestination(transform.position);
			stun = Time.time + time;
		}

		public void SetDestination(Vector3 target)
		{
			if (Time.time > stun && !isDead) navMeshAgent.SetDestination(target);
		}

		public List<AIEntity> GetVisibleAIEntities()
		{
			return perceptionBrain.GetVisibleAIEntities();
		}

		public void TakeDamage()
		{
			currentHealth--;
			Debug.Log(transform.name + " was hit");

			if (currentHealth <= 0)
			{
				Die();
			}
		}

		public virtual void Die()
		{
			Debug.Log(transform.name + " died");
			isDead = true;
			GameManager.instance.Eliminate(team);
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<CapsuleCollider>().enabled = false;
			GetComponent<NavMeshAgent>().enabled = false;
		}

	}
}
