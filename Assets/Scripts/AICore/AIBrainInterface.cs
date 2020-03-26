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
		private NavMeshAgent navMeshAgent;
		private AudioSource audioSource;
		public Light light;

		// Editor variables
		[SerializeField] public AITeam team;
		[SerializeField] public AIType type;
		[SerializeField] public GameObject deathFXPrefab;
		[SerializeField] public AudioClip stunSound;

		// Private variables
		public float stun { get; private set; }

		//--------------------------
		// MonoBehaviour methods
		//--------------------------
		void Awake()
		{
			perceptionBrain = GetComponent<AIPerceptionBrain>();
			combatBrain = GetComponent<AICombatBrain>();
			navMeshAgent = GetComponent<NavMeshAgent>();
			audioSource = GetComponent<AudioSource>();
			light = GetComponentInChildren<Light>();

			stun = 0;
		}

		private void OnDrawGizmos()
		{
			if (Application.isPlaying)
			{
				foreach (AIBrainInterface entityInterface in perceptionBrain.GetVisibleAIEntities())
				{
					Gizmos.color = Color.white;
					Gizmos.DrawLine(transform.position, entityInterface.transform.position);
					//if (stun >= Time.deltaTime) Gizmos.DrawSphere(transform.position, 1f);
				}
			}
		}

		//--------------------------
		// AIBrainInterface methods
		//--------------------------
		public void Stun(float time)
		{
			if (transform.parent.gameObject.GetComponent<PlayerAI>().isEliminated) return;
			if (stun > Time.time) return;

			SetDestination(transform.position);
			stun = Time.time + time;
			audioSource.PlayOneShot(stunSound);
		}

		public void SetDestination(Vector3 target)
		{
			if (transform.parent.gameObject.GetComponent<PlayerAI>().isEliminated) return;
			if (Time.time > stun) navMeshAgent.SetDestination(target);
		}

		public Vector3 GetVelocity()
		{
			return navMeshAgent.velocity;
		}

		public bool raycastForward(out RaycastHit hit)
		{
			return Physics.Raycast(transform.position, transform.forward, out hit, perceptionBrain.radiusOfView);
		}

		public List<AIBrainInterface> GetVisibleAIEntities()
		{
			if (transform.parent.gameObject.GetComponent<PlayerAI>().isEliminated) return new List<AIBrainInterface>();
			return perceptionBrain.GetVisibleAIEntities();
		}

		public void TakeDamage()
		{
			if (transform.parent.gameObject.GetComponent<PlayerAI>().isEliminated) return;

			audioSource.PlayOneShot(stunSound);

			// not receiving damage if not target
			if (type != AIType.target) return;

			Die();
		}

		/// <summary>
		/// Kills the entire team
		/// </summary>
		public void Die()
		{
			transform.parent.gameObject.GetComponent<PlayerAI>().Eliminate();
		}

		/// <summary>
		/// Visualizes dying on the entity
		/// </summary>
		public void Burn()
		{
			Instantiate(deathFXPrefab, transform.position, transform.rotation);
			StartCoroutine(BurnCorutine());
		}

		private IEnumerator BurnCorutine()
		{
			GetComponent<AIPerceptionBrain>().enabled = false;
			GetComponent<AICombatBrain>().enabled = false;
			GetComponent<Collider>().enabled = false;
			GetComponent<NavMeshAgent>().enabled = false;

			while (true)
			{
				transform.position += Vector3.down * Time.deltaTime * 2f;
				yield return null;
			}
		}
	}
}
