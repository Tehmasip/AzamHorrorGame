﻿using UnityEngine;
using System.Collections;

public class physicWalk : MonoBehaviour
{

	public static physicWalk instance;

	//
	public float speed = 7f;
	public float maxSprintSpeed = 7f;
	public float maxWalkSpeed = 4f;
	public float force = 8f;
	public float jumpSpeed = 5f;

	public float footsFrequency = 0.7f;

	//
	public bool grounded = false;
	public bool StopWalkSound = false;

	private float fallingForce = 0f;

	private new CapsuleCollider collider;

	private bool canJump = true;
	private float canJumpCounter = 0f;

	public AudioClip footstepSound;
	public AudioClip RunfootstepSound;

	public AudioClip fallSound;

	public AudioSource PlayeraudioSource;
	public AudioSource JumpAudioSource;



	void Start()
	{
		instance = this;

		collider = gameObject.GetComponent<CapsuleCollider>();
	}

	// Don't let the Physics Engine rotate this physics object so it doesn't fall over when running
	void Awake()
	{
		GetComponent<Rigidbody>().freezeRotation = true;

		speed = maxWalkSpeed;
	}

	public virtual float jump
	{
		get
		{
			if (ControlFreak2.CF2Input.GetButton("Jump"))
			{

				return 1f;
			}
			else
			{
				return 0f;
			}

		}
	}

	public virtual float horizontal
	{
		get
		{
			float v = ControlFreak2.CF2Input.GetAxis("Horizontal");
			//Debug.Log("walk Horizontal");
			//if (v>0||v<0)
			//         {
			//	if(!PlayeraudioSource.isPlaying)
			//             {
			//		PlayeraudioSource.clip = footstepSound;
			//		PlayeraudioSource.Play();
			//	}
			//}
			//         else
			//         {
			//	PlayeraudioSource.Stop();
			//}
			return v * force;
		}
	}
	public virtual float vertical
	{
		get
		{
			float v = ControlFreak2.CF2Input.GetAxis("Vertical");
			//Debug.Log("walk Vertical");
			//if (v > 0 || v < 0)
			//{
			//	if (!PlayeraudioSource.isPlaying)
			//	{
			//		PlayeraudioSource.clip = footstepSound;
			//		PlayeraudioSource.Play();
			//	}
			//}
			//else
			//{
			//	PlayeraudioSource.Stop();
			//}
			return v * force;
		}
	}

	float fr = 0f;
	void Update()
	{
		if (GetComponent<Rigidbody>().velocity.magnitude > 0f && grounded)
		{
			fr += Time.deltaTime;

			if (ControlFreak2.CF2Input.GetButton("Sprint"))
			{
				//Debug.Log("Sprint");
				PlayeraudioSource.clip = RunfootstepSound;
				if (!PlayeraudioSource.isPlaying && PlayeraudioSource.clip == RunfootstepSound && !StopWalkSound)
				{
					PlayeraudioSource.Play();
				}

				fr += Time.deltaTime * 0.5f;
			}
			else
			{
				PlayeraudioSource.clip = footstepSound;
			}

			while (fr >= footsFrequency)
			{
				fr = 0f;
				//Debug.Log("Walk");
				if (PlayeraudioSource.clip != RunfootstepSound)
				{
					PlayeraudioSource.clip = footstepSound;
					if (!PlayeraudioSource.isPlaying && PlayeraudioSource.clip == footstepSound && !StopWalkSound)
					{
						PlayeraudioSource.Play();
					}
				}

				//playFootstepSound();
			}
		}
		else
		{
			PlayeraudioSource.Stop();
			//Debug.Log("Not Walk");
		}

		if (GetComponent<Rigidbody>().IsSleeping() == true) GetComponent<Rigidbody>().WakeUp();

		if (ControlFreak2.CF2Input.GetButton("Sprint"))
		{
			speed = maxSprintSpeed;
		}
		else speed = maxWalkSpeed;
	}

	public void playFootstepSound()
	{
		GetComponent<AudioSource>().PlayOneShot(footstepSound);
	}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == StaticVariables.Obstacle)
        {
            PlayeraudioSource.Stop();
            StopWalkSound = true;
        }
    }

    private void OnCollisionExit(Collision collision)
	{
		if (collision.transform.tag == StaticVariables.Obstacle)
		{
			StopWalkSound = false;
		}
	}
	void FixedUpdate()
	{
		///Jump iteration
		if (!canJump)
		{
			canJumpCounter += Time.fixedDeltaTime;
			if (canJumpCounter >= 1f)
			{
				canJump = true;
				canJumpCounter = 0f;
			}
		}

		////Ground test
		RaycastHit hit;

		Vector3 tmpV = transform.position;
		tmpV.y += 0.1f;
		if (Physics.Raycast(tmpV, -Vector3.up, out hit, 0.3f))
		{
			if (hit.collider.tag == "GROUND")
			{
				grounded = true;
			}
			else
			{
				grounded = false;
			}
		}
		else
		{
			grounded = false;
		}

		////

		if (horizontal != 0f || vertical != 0f || jump != 0f || !grounded) GetComponent<Rigidbody>().drag = 2f;
		else
		{
			GetComponent<Rigidbody>().drag += 10f;

			if (GetComponent<Rigidbody>().drag >= 100f) GetComponent<Rigidbody>().drag = 100f;
		}

		if (GetComponent<Rigidbody>().velocity.magnitude < speed && grounded == true)
		{
			Vector3 forceV = Vector3.Cross(hit.normal, Vector3.Cross(transform.forward, hit.normal));
			forceV = forceV.normalized;

			if (vertical != 0f && horizontal != 0f) GetComponent<Rigidbody>().AddForce(((forceV * vertical) + (transform.right * horizontal)) * 0.8f);
			else GetComponent<Rigidbody>().AddForce((forceV * vertical) + (transform.right * horizontal));
		}

		if (jump != 0f && grounded && canJump)
		{
			canJump = false;

			Debug.Log("Jump");

			if (!JumpAudioSource.isPlaying)
			{
				JumpAudioSource.Play();
			}

			Vector3 tmp = Vector3.up * jumpSpeed + (transform.forward * vertical * 0.1f);
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + tmp;
		}

		if (!grounded)
		{

			fallingForce = fallingForce + Time.fixedDeltaTime * 5f;
			GetComponent<Rigidbody>().AddForce(-Vector3.up * 10f * fallingForce);
		}
		else
		{

			fallingForce -= (Time.fixedDeltaTime * 10f) + (fallingForce * 0.3f);
			if (fallingForce < 0f) fallingForce = 0f;
		}

	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "GROUND")
		{
			Debug.Log("ON Ground!!");


			if (other.relativeVelocity.y >= 2f)
			{
				SoundManager.instance.PlayEffect(AudioClipsSource.Instance.FallSound);
				physicWalk_MouseLook.instance.wobble(0f, other.relativeVelocity.y * 2f, 0f, other.relativeVelocity.y * 2f);

                //GetComponent<AudioSource>().PlayOneShot(fallSound);

                Vector3 tmpPosMod = Camera.main.transform.position;
				tmpPosMod.y -= other.relativeVelocity.y * 0.15f;
				physicWalk_MouseLook.instance._camPos.position = tmpPosMod;
			}
		}
	}
}