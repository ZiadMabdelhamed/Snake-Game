using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// handle the movement of the snake 
public class snake : MonoBehaviour {



	public List<Transform> body_list;
	public Transform snake_head;

	// to make a delay in the movement ( classic movement )
	public float step_time = 0.15f;
	public float next_step;

	// this help to prevent the snake from going back in the opposite direction
	public enum direction {Righ,Left,Up,Down};
	public direction Dir;

	// object from the swipe controller to controll the rotation
	public swipe swipe_obj;

	// make a rotation list to pick a random direction every time 
	int[] Rotation_list = {0,90,180,270};

	// the snake body parts 
	public GameObject snake_body;

	void Start () 
	{
		swipe_obj = transform.GetComponent<swipe> ();

		// pick a random direction to start game 
		int snake_rand_rotation = Rotation_list [Random.Range (0, Rotation_list.Length)];

		if(snake_rand_rotation == 0)
		{
			Dir = direction.Up;
			transform.eulerAngles = new Vector3(0,0,0);
		}

		if(snake_rand_rotation == 90)
		{
			Dir = direction.Righ;
			transform.eulerAngles = new Vector3(0,90,0);
		}

		if(snake_rand_rotation == 180)
		{
			Dir = direction.Down;
			transform.eulerAngles = new Vector3(0,180,0);
		}

		if(snake_rand_rotation == 270)
		{
			Dir = direction.Left;
			transform.eulerAngles = new Vector3(0,270,0);
		}

		snake_head = body_list [0];
	}
	

	void FixedUpdate () 
	{
		direction_snake ();

		// apply the delay in the movement  
		if (Time.time > next_step) 
		{
			move ();
		}
	}

	// move the smake by changing the body parts positions 
	void move()
	{	
		

		for (int i = body_list.Count - 1; i > -1; i--) 
		{
			Transform cur_body = body_list [i];

			if (i != 0) 
			{
				cur_body.position = body_list [i - 1].position;

			}
		}

		// move the head to drap the body
		snake_head.position += snake_head.forward * 1 ;

	
		next_step = Time.time + step_time;
	}

	// change the direction of the snake head using the swipe direction 
	void direction_snake ()
	{
		if (swipe_obj.swipe_dir == swipe.swipe_direction.up && Dir != direction.Up && Dir != direction.Down) 
		{
			snake_head.eulerAngles = new Vector3 (0, 0, 0);
			Dir = direction.Up;
			move ();
		}

		if (swipe_obj.swipe_dir == swipe.swipe_direction.down && Dir != direction.Up && Dir != direction.Down) 
		{
			snake_head.eulerAngles = new Vector3 (0, 180, 0);
			Dir = direction.Down;
			move ();
		}

		if (swipe_obj.swipe_dir == swipe.swipe_direction.right && Dir != direction.Righ && Dir != direction.Left) 
		{
			snake_head.eulerAngles = new Vector3 (0, 90, 0);
			Dir = direction.Righ;
			move ();
		}

		if (swipe_obj.swipe_dir == swipe.swipe_direction.left && Dir != direction.Righ && Dir != direction.Left) 
		{
			snake_head.eulerAngles = new Vector3 (0, -90, 0);
			Dir = direction.Left;
			move ();
		}
	}

	// increase one body part
	public void Add_body()
	{
		Transform last_bone = body_list [body_list.Count - 1];
		GameObject new_body = Instantiate (snake_body,last_bone.position - last_bone.forward,snake_body.transform.rotation) as GameObject;
		new_body.transform.parent = transform;
		body_list.Add (new_body.transform);
	}
		
}
