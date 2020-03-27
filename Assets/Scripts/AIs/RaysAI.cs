using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaysAI : PlayerAI {
	// Other components
	private int redPointCounter = 0;
	public List<GameObject> red;
	public List<GameObject> blue;
	public List<GameObject> green;
	GameObject[] redList; //test for seeing if this is a thing.
	

	// Public variables

	// Private variables
	private bool guardingStance = false;
	private bool amariStunGun = false;
	private bool WarBringer = false;
	private bool warbringerSplit = false;
	private bool snuffleSnuffSplit = false;
	private bool regroup = false;
	private bool greenSafe = true;
	private float greenTimer = 0f;
	private float greenBlockDistance = 0f;
	private float recordedDistanceGreen = 0f;
	private float newDistanceGreen;
	private float warbringerSplitTimer = 0.0f;

	private Transform nioDestination;
	private Transform lastNioDestination;
	private Transform snuffleSnuffeHidingSpot;
	private Transform amariDefense;
	private Transform lastAmariDefense;

	private int currentRedWP;
	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	new void Start()
	{
		//Load in all points.
		base.Start();
		AssassinWandererSet();
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
				snuffleSnuffSplit = true;
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
		if ((WarBringer == false) && (warbringerSplit == false) && (lastNioDestination == nioDestination))
		{
			AssassinWanderer();
		}

		if ((regroup = true) && (WarBringer == false))
		{
			//regroup with the others.
			assassin.SetDestination(this.targetPrefab.transform.position);
			//if we're in range of the others. trigger wanderer.
			if (Vector3.Distance(this.assassinPrefab.transform.position, this.targetPrefab.transform.position) < 5.0f)
			{
				AssassinWanderer();
				regroup = false;
			}
		}

		//-------------------------------------------------------------------------------



		//Lady 'Get fucked' Amari (Defender)
	
		

		//Amiri's Guarding stance
		if (guardingStance == true) 
		{
			//Amiri will patroll, in case amiri sees any attacker. she will run and stunn. --------------

			//Then trigger snuffleSnuffSplit will run the other way.
			snuffleSnuffSplit = true;

			if ((greenTimer % 0.2) == 0)
			{
				foreach (AICore.AIBrainInterface visibleEntityInterface in stunner.GetVisibleAIEntities())
				{
					if (((visibleEntityInterface.team != target.team && visibleEntityInterface.type == AICore.AIType.assassin && (Vector3.Distance(this.target.transform.position, visibleEntityInterface.transform.position) < recordedDistanceGreen))))
					{
						//if they are getting closer to snufflesnuff
						warbringerSplit = true;
						snuffleSnuffSplit = true;
						stunner.SetDestination(visibleEntityInterface.transform.position);
						recordedDistanceGreen = 0f;
					}
					else if (visibleEntityInterface.team != target.team && visibleEntityInterface.type == AICore.AIType.assassin)
					{
						//if they are not getting closer to snufflesnuff, record distance.
						//	not working?		Vector3.Distance(this.target.transform.position, visibleEntityInterface.transform.position) = recordedDistanceGreen;
					}
				}
			}
			//check the distance between a registered attacker and snuffle snuff
			//register the distance, and then check in the following 0.2secs if it's coming closer?

		}
		else
		{
			//follow SnuffleSnuff
			stunner.SetDestination(target.transform.position);
		}



		//------------------------------------------------------------------------------

		//Sir Noble SnuffleSnuff (Target)
		//Follow Nio.



		if (snuffleSnuffSplit == true)
		{

			//then check the nearest hiding spot and see if the the attackker is closer or snufflesnuff is. if so Run to the nearest hidingspot!
			//check the nerest hiding spot.
			//record the nearest hidingpot.

			foreach (AICore.AIBrainInterface visibleEntityInterface in target.GetVisibleAIEntities())
			{
				if (visibleEntityInterface.team != target.team && visibleEntityInterface.type == AICore.AIType.assassin &&(Vector3.Distance(this.target.transform.position, visibleEntityInterface.transform.position) > greenBlockDistance)) 
				{
					warbringerSplit = true;
					snuffleSnuffSplit = true;
				}
			}
			//if not, instead run away!
			//then wait untill the area is safe. (which we do within amari's check, since snufflesnuff is useless.
		}




		//Warbringer split
		if (warbringerSplit == true)
		{
			//SnuffleSnuff will run and hide.
			//target.SetDestination(... transform position);
			//Trigger Amari's GuardingStance.
			guardingStance = true;
		}
		else
		{
			target.SetDestination(assassin.transform.position); //wander after the assassin
		}



		//possible snuffsnuff sight
		if (guardingStance == true)
		{
			greenTimer = +1f * Time.deltaTime;
			//Overrule guardingStance, (this is done via the guardingstance if statement.)

			//if the enemy attacker hasnt gotten closer to SnuffleSnuff in the last 3 seconds, Go back to target and circling.
			if((greenTimer % 0.2) == 0)
			{
				foreach (AICore.AIBrainInterface visibleEntityInterface in target.GetVisibleAIEntities())
				{
					if (((visibleEntityInterface.team != target.team && visibleEntityInterface.type == AICore.AIType.assassin && (Vector3.Distance(this.target.transform.position, visibleEntityInterface.transform.position) < recordedDistanceGreen))))
					{
						//if they are getting closer to snufflesnuff
						warbringerSplit = true;
						snuffleSnuffSplit = true;
						stunner.SetDestination(visibleEntityInterface.transform.position);
						recordedDistanceGreen = 0f;
					}
					else if (visibleEntityInterface.team != target.team && visibleEntityInterface.type == AICore.AIType.assassin)
					{
						//if they are not getting closer to snufflesnuff, record distance.
						//	not working?		Vector3.Distance(this.target.transform.position, visibleEntityInterface.transform.position) = recordedDistanceGreen;
					}
				}
			}

			if (greenTimer == 3f && greenSafe == true)
			{
				regroup = true; //call for a regroup with Nio. this can only be overruled if she's hunting a target.
				snuffleSnuffSplit = false;
				amariStunGun = false;
				warbringerSplit = false;
			}
			
		}


		//-------------------------------------------------------------------------------




	}
	//--------------------------
	// RaysAI methods
	//--------------------------

	void AssassinWanderer()
	{
		//if the destination is not the same as the last visited destination. 

		int n = Random.Range(0, red.Count);
		//nioDestination = red[n];
		if ((nioDestination == lastNioDestination) && (regroup == false))
	{
			//walk to destination unless interupted.
		}

	}

	void AssassinWandererSet()
	{
		//get the list of all the red blocks
		//pick one at random and set as destination
		//compare destination with the last visited destination
		//is this the same? then we'll do it again.
		//is it not? then we trigger Assassin wanderer.
		AssassinWanderer();
	}

	private void OnCollisionStay(Collision other)
	{
		if (other.transform.CompareTag("Red") && (other.gameObject.transform == nioDestination))
		{
			lastNioDestination = other.transform;
			AssassinWandererSet();
		}
	}
}
