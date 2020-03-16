using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaysAI : PlayerAI
{
	// Other components

	// Editor variables

	// Public variables

	// Private variables
	private bool WarBringer = false;
	private bool warbringerSplit = false;
	private float warbringerSplitTimer = 0.0f;
	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	new void Start()
	{

	}


	void Update()
	{
		//-------------------------------------------------------------------------------
		//The war bringer, Nio (Assassin)
		//check what Nio sees.
		//if Nio sees an enemy defender, nio head in that direction.
		//Nio will then move to pass the defender, continuously checking if they see the target.
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
		if(warbringerSplitTimer >= 1f)
		{
			warbringerSplit = false;
		}

		//roaming isn't done yet.


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
		//If something is approaching SnuffleSnuff, trigger AmariStungun to stun it. then run the other way.
		//T


		//snufflesnuffSplit
		//run the oposite direction from the attaker Amari sees and hide out of sight.
		//then wait for amari to have registered the area to be safe.


		//Warbringer split
		if (warbringerSplit == true)
		{
			//SnuffleSnuff will run and hide behind a nearby corner.
			//Trigger Amari's GuardingStance.
		}



		//AmariStungun
		//Overrule guardingStance, and only attack the enemy attacker.
		//if the enemy attacker hasnt gotten closer to SnuffleSnuff in the last 3 seconds, Go back to target and circling.

		//-------------------------------------------------------------------------------




	}
	//--------------------------
	// RaysAI methods
	//--------------------------


}
