using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICore
{
	public class AIEntity
	{
		public Transform transform { get; private set; }
		public AIType type { get; private set; }
		public GameTeam team { get; private set; }

		public AIEntity(Transform transform, AIType type, GameTeam team)
		{
			this.transform = transform;
			this.type = type;
			this.team = team;
		}
	}
}
