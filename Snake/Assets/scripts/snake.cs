using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class snake : MonoBehaviour {



	public List<Transform> body_list;
	public Transform snake_head;

	public float step_time = 0.15f;
	public float next_step;

	public enum direction {Righ,Left,Up,Down};
	public direction Dir;

	public swipe swipe_obj;

	//public List<int> Rotation_list = new List<int> {0,90,180,270};

	int[] Rotation_list = {0,90,180,270};

	//public int[] Rotation_list = new int[] {0,90,180,270};

	public GameObject snake_body;

	void Start () 
	{
		swipe_obj = transform.GetComponent<swipe> ();

		int snake_rand_rotation = Rotation_list [Random.Range (0, Rotation_list.Length)];
		//print (snake_rand_rotation);
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
	

	void Update () 
	{
		direction_snake ();

		if (Time.time > next_step) 
		{
			move ();
		}
	}


	void move()
	{	
		

		for (int i = body_list.Count - 1; i > -1; i--) 
		{
			Transform cur_body = body_list [i];

			if (i != 0) 
			{
				cur_body.position = body_list [i - 1].position;
				//cur_body.position = Vector3.Slerp (cur_body.position, body_list [i - 1].position ,0.03f);
				//cur_body.transform.position = Vector3.Lerp (cur_body.transform.position, body_list [i - 1].transform.position ,0.9f);
			}
		}

		//snake_head.position += snake_head.forward * speed * Time.deltaTime ;
		snake_head.position += snake_head.forward * 1 ;
		//snake_head.Translate (snake_head.forward * 1 , Space.World);
		//snake_head.transform.position = Vector3.Lerp (snake_head.transform.position, snake_head.transform.position + snake_head.transform.forward * 1 ,1f);

	
		next_step = Time.time + step_time;
	}

	void direction_snake ()
	{
		if (swipe_obj.swipe_dir == swipe.swipe_direction.up && Dir != direction.Up && Dir != direction.Down) 
		{
			snake_head.eulerAngles = new Vector3 (0, 0, 0);
			Dir = direction.Up;
			//move ();
		}

		if (swipe_obj.swipe_dir == swipe.swipe_direction.down && Dir != direction.Up && Dir != direction.Down) 
		{
			snake_head.eulerAngles = new Vector3 (0, 180, 0);
			Dir = direction.Down;
			//move ();
		}

		if (swipe_obj.swipe_dir == swipe.swipe_direction.right && Dir != direction.Righ && Dir != direction.Left) 
		{
			snake_head.eulerAngles = new Vector3 (0, 90, 0);
			Dir = direction.Righ;
			//move ();
		}

		if (swipe_obj.swipe_dir == swipe.swipe_direction.left && Dir != direction.Righ && Dir != direction.Left) 
		{
			snake_head.eulerAngles = new Vector3 (0, -90, 0);
			Dir = direction.Left;
			//move ();
		}
	}

	public void Add_body()
	{
		Transform last_bone = body_list [body_list.Count - 1];
		GameObject new_body = Instantiate (snake_body,last_bone.position - last_bone.forward,snake_body.transform.rotation) as GameObject;
		new_body.transform.parent = transform;
		body_list.Add (new_body.transform);
	}
		
}
