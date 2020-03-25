using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaysAI : PlayerAI {
	// Other components
	private int redPointCounter = 0;
	public List<GameObject> red;
	public List<GameObject> blue;
	public List<GameObject> green;
	// Editor variables

	// Public variables

	// Private variables
	private bool WarBringer = false;
	private bool warbringerSplit = false;
	private float warbringerSplitTimer = 0.0f;

	private Vector3 nioDestination;
	private Vector3 lastNioDestination;
	private Vector3 snuffleSnuffeHidingSpot;
	private Vector3 amariDefense;
	private Vector3 lastAmariDefense;
	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	new void Start()
	{
		//Load in all points.
	}


	void Update()
	{
		//-------------------------------------------------------------------------------
		//The war bringer, Nio (Assassin)
		//check what Nio sees.
		//if at any time Nio sees the target, it will chase.
		//if Nio sees an attacker, Trigger WarbringerSplit.
		//else if Nio sees nothing, then she'll return to roaming.
		foreach (AICore.AIBrainInterface visibleEntityInterface in assassin.GetVisibleAIEntities())
		{
			if (visibleEntityInterface.team != target.team && visibleEntityInterface.type == AICore.AIType.assassin)
			{
				warbringerSplit = true;
			}
			if (visibleEntityInterface.team != target.team && visibleEntityInterface.type == AICore.AIType.target)
			{
				assassin.SetDestination(visibleEntityInterface.transform.position);
				WarBringer = true;
			}
			else if ((visibleEntityInterface.team != target.team && visibleEntityInterface.type == AICore.AIType.target) && (WarBringer == false))
			{
				assassin.SetDestination(visibleEntityInterface.transform.position);
			}
			else
			{
				WarBringer = false;
			}
		}

		if (warbringerSplit == true)
		{
			warbringerSplitTimer += Time.deltaTime;
		}
		//check every second if we still see anyone.
		if (warbringerSplitTimer >= 1f)
		{
			warbringerSplit = false;
		}

		//roaming if nothing else is going on~
		if ((WarBringer == false) && (warbringerSplit == false))
		{
			//get the 2 closest red points from the list. currently just one
		
			//assassin.SetDestination(tMin.position);
			//set tMin as the place to walk to.

		}


		//-------------------------------------------------------------------------------



		//Lady 'Get fucked' Amari (Defender)
		//Circle SnuffleSnuff
		//If Amari sees an attacker, Trigger snufflsnuff split.

		//Amiri's Guarding stance
		//Amiri will patroll, in case amiri sees any attacker. she will run and stunn. Then trigger snuffleSnuffSplit will run the other way.


		//------------------------------------------------------------------------------

		//Sir Noble SnuffleSnuff (Target)
		//Follow Nio.
		//Check what SnuffleSnuff sees.
		//If an attacker is approaching SnuffleSnuff, trigger AmariStungun to stun it. 
		//then check the nearest hiding spot and see if the the attackker is closer or snufflesnuff is. if so Run to the nearest hidingspot!
		//if not, instead run to the second nearest.


		//snufflesnuffSplit
		//run the oposite direction from the attaker Amari sees and hide out of sight.
		//then wait for amari to have registered the area to be safe.


		//Warbringer split
		if (warbringerSplit == true)
		{
			//SnuffleSnuff will run and hide behind a nearby corner.
			//Trigger Amari's GuardingStance.
		}
		else
		{
			//roam normally.
		}



		//AmariStungun
		//Overrule guardingStance, and only attack the enemy attacker.
		//if the enemy attacker hasnt gotten closer to SnuffleSnuff in the last 3 seconds, Go back to target and circling.

		//-------------------------------------------------------------------------------




	}
	//--------------------------
	// RaysAI methods
	//--------------------------

	void AssassinWanderer()
	{
		//get the list of all the red blocks
		//pick one at random and set as destination
		//compare destination with the last visited destination
		//is this the same? then we'll do it again.
	}

	private void OnCollisionStay(Collision other)
	{
		if (other.gameObject.CompareTag("red"))
		{

		}
	}
	void redReset()
	{
		redPointCounter = 0;
		//reload all red points.
	}

}
