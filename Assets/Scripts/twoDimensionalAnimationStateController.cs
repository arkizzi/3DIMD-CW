using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDimensionalAnimationStateController : MonoBehaviour
{
	Animator animator;
	float velocityZ = 0.0f;
	float velocityX = 0.0f;
	public float acceleration = 2.0f;
	public float deceleration = 2.0f;
	public float maxWalkVelocity = 0.5f;
	public float maxRunVelocity = 2.0f;

	//increase performance
	int VelocityZHash;
	int VelocityXHash;

	// Start is called before the first frame update
	void Start()
	{
		//search game object this script linked to and get animator component
		animator = GetComponent<Animator>();
		Debug.Log(animator); // debug log of getcomponant
		// increase performance
		VelocityZHash = Animator.StringToHash("Velocity Z");
		VelocityXHash = Animator.StringToHash("Velocity X");
	}

	//Handles acceleration and Deceleration 
	void changeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
	{
		float timedecelation = Time.deltaTime * deceleration;
		float timeaccelation = Time.deltaTime * acceleration;
		// if player presses forward, increase velocity in Z direction       
		if (forwardPressed && velocityZ < currentMaxVelocity)
		{
			velocityZ += timeaccelation;
		}

		// increase velocity in left  direction       
		if (leftPressed && velocityX > -currentMaxVelocity)
		{
			velocityX -= timeaccelation;
		}

		// increase velocity in Right direction       
		if (rightPressed && velocityX < currentMaxVelocity)
		{
			velocityX += timeaccelation;
		}

		// decrease VelocityZ     
		if (!forwardPressed && velocityZ > 0.0f)
		{
			velocityZ -= timedecelation;
		}

		// increase velocityX if left is not pressed and velocity x < 0      
		if (!leftPressed && velocityX < 0.0f)
		{
			if (velocityX + timedecelation > 0.0f)
			{
				velocityX = 0.0f;
			}
			else
			{
				velocityX += timedecelation;
			}
		}

		// decrease velocityX if Right is not pressed and velocity x < 0       
		if (!rightPressed && velocityX > 0.0f)
		{
			if (velocityX - timedecelation < 0.0f)
			{
				velocityX = 0.0f;
			}
			else
			{
				velocityX -= timedecelation;
			}
		}

	}

	//handles reset and locking of Velocity 
	void lockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
	{
		// Reset VelocityZ
		if (!forwardPressed && velocityZ < 0.0f)
		{
			velocityZ = 0.0f;
		}

		// reset velocity X
		//if (!leftPressed && !rightPressed && velocityX < 0.0f && velocityX > -0.0001f)
		//{
		//	velocityX = 0.0f;
		//}

		velocityZ = LockFunction(forwardPressed, velocityZ, currentMaxVelocity, runPressed);
		velocityX = LockFunction(rightPressed, velocityX, currentMaxVelocity, runPressed);


		// lock Left
		if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
		{
			velocityX = -currentMaxVelocity;
		}
		//decelerate to max walk velocity 
		else if (leftPressed && velocityX < -currentMaxVelocity)
		{
			velocityX += Time.deltaTime * deceleration;
			// Round to the currentMaxVelocity if wthin offset
			if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
			{
				velocityX = -currentMaxVelocity;
			}
		}
		//round to the current max velocity if wthin offset
		else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
		{
			velocityX = -currentMaxVelocity;
		}

	}

	float LockFunction(bool buttonX, float SpeedY, float currentMaxVelocity, bool runPressed)
	{
		// lock forward
		if (buttonX && runPressed && SpeedY > currentMaxVelocity)
		{
			SpeedY = currentMaxVelocity;
		}
		//decelerate to the max walk velocity 
		else if (buttonX && SpeedY > currentMaxVelocity)
		{
			SpeedY -= Time.deltaTime * deceleration;
			// Round to the currentMaxVelocity if wthin offset
			if (SpeedY > currentMaxVelocity && SpeedY < (currentMaxVelocity + 0.05f))
			{
				SpeedY = currentMaxVelocity;
			}
		}
		else if (buttonX && SpeedY < currentMaxVelocity && SpeedY > (currentMaxVelocity - 0.05f))
		{
			SpeedY = currentMaxVelocity;
		}
		return SpeedY;

	}

	// Update is called once per frame
	void Update()
	{
		// input will be true if the player presses on the passed in key parameter
		// get keyinput from player object
		bool forwardPressed = Input.GetKey(KeyCode.W);
		bool leftPressed = Input.GetKey(KeyCode.A);
		bool rightPressed = Input.GetKey(KeyCode.D);
		bool runPressed = Input.GetKey(KeyCode.LeftShift);

		//set current maxVelocity
		float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity; // if runpressed true it will set velo to maxrun if not max walk (Terneray operator)

		// handles changes in velocity  
		changeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
		lockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);


		// Sets the parameter to the local variable values
		animator.SetFloat(VelocityZHash, velocityZ);
		animator.SetFloat(VelocityXHash, velocityX);
	}
}