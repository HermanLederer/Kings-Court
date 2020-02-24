using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICore
{
	/// <summary>
	/// AIControlBrain is used by players to manipulate the entity's movement
	/// </summary>
	[RequireComponent(typeof(AIPerceptionBrain))]
	[RequireComponent(typeof(CharacterController))]
	public class AIControlBrain : MonoBehaviour
	{

		public float lookRadius = 1f;
		Transform target;
		NavMeshAgent agent;	
		AICombat combat;
		// Other components
		public AIPerceptionBrain perceptionBrain { get; private set; }
		private CharacterController characterController;

		// Editor variables
		[SerializeField] public AIType type { get; private set; }
		[SerializeField] private float maxMovementSpeed = 3f;
		[Tooltip("Maximum rotation angle per unit of time in radians")]
		[SerializeField] private float maxRotationSpeed = 0.5f;

		// Public variables
		[HideInInspector] private float _speedMultiplier;
		[HideInInspector] public Vector3 targetDirection;

		public float speedMultiplier
		{
			get { return _speedMultiplier; }
			set
			{
				_speedMultiplier = value;

				// limit to 1 as maximum value
				if (_speedMultiplier > 2) _speedMultiplier = 2;
			}
		}

		// Private variables

		//--------------------------
		// MonoBehaviour methods
		//--------------------------
		void Start()
		{
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
		combat = GetComponent<AICombat>();
		}
		
		void Awake()
		{
			perceptionBrain = GetComponent<AIPerceptionBrain>();
			characterController = GetComponent<CharacterController>();

			speedMultiplier = 0;
			targetDirection = Vector3.zero;
		}

		void Update()
		{
			// slow down or accelerate
			// NIY

			// rotate towards targetRotation
			Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, maxRotationSpeed * Time.deltaTime, 0.0f);
			transform.rotation = Quaternion.LookRotation(newDirection);
			float distance = Vector3.Distance(target.position, transform.position);
			if(distance <= lookRadius)
			{
				agent.SetDestination(target.position);
				if(distance <= agent.stoppingDistance)
				{
					AIStats targetStats = target.GetComponent<AIStats>();
					if (targetStats != null)
					{
						combat.Attack(targetStats);
					}
					FaceTarget();
				}
			}
			// moving
			characterController.SimpleMove(transform.forward * maxMovementSpeed * speedMultiplier);
		}

		void FaceTarget()
		{
			Vector3 direction = (target.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.001f, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
	}
		
}
