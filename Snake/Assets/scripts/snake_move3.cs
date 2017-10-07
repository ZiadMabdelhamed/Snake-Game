using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class snake_move3 : MonoBehaviour {

	public List<Transform> body_parts = new List<Transform>();

	public float min_distance = 0.25f;

	public float speed = 1 ;

	public float rotation_speed = 50;

	public GameObject body_prefabe;

	private float dis;
	private Transform cur_bodypart;
	private Transform prev_bodypart;

	public int begain_size;



	public enum direction {Righ,Left,Up,Down};
	public direction Dir;

	public swipe swipe_obj;

	//public List<int> Rotation_list = new List<int> {0,90,180,270};

	int[] Rotation_list = {0,90,180,270};



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
	

		for (int i = 0; i < begain_size - 1; i++) 
		{
			add_bodypart ();	
		}
	}
	

	void Update () {
		move ();

		if (Input.GetKey(KeyCode.Q)) 
		{
			add_bodypart ();
		}
	}

	public void move()
	{
		float curspeed = speed;

		body_parts [0].Translate (body_parts [0].forward * curspeed * Time.smoothDeltaTime, Space.World);

//		if (Input.GetAxis ("Horizontal") != 0) 
//		{
			//body_parts [0].Rotate (Vector3.up * rotation_speed * Time.deltaTime * Input.GetAxis ("Horizontal"));
//			body_parts [0].transform.eulerAngles = new Vector3(0,90,0);
//		}
		if (swipe_obj.swipe_dir == swipe.swipe_direction.up && Dir != direction.Up && Dir != direction.Down) 
		{
			body_parts [0].eulerAngles = new Vector3 (0, 0, 0);
			Dir = direction.Up;
			//move ();
		}

		if (swipe_obj.swipe_dir == swipe.swipe_direction.down && Dir != direction.Up && Dir != direction.Down) 
		{
			body_parts [0].eulerAngles = new Vector3 (0, 180, 0);
			Dir = direction.Down;
			//move ();
		}

		if (swipe_obj.swipe_dir == swipe.swipe_direction.right && Dir != direction.Righ && Dir != direction.Left) 
		{
			body_parts [0].eulerAngles = new Vector3 (0, 90, 0);
			Dir = direction.Righ;
			//move ();
		}

		if (swipe_obj.swipe_dir == swipe.swipe_direction.left && Dir != direction.Righ && Dir != direction.Left) 
		{
			body_parts [0].eulerAngles = new Vector3 (0, -90, 0);
			Dir = direction.Left;
			//move ();
		}


		for (int i = body_parts.Count -1 ; i > 0 ; i--) 
		{
			cur_bodypart = body_parts [i];
			prev_bodypart = body_parts [i - 1];

			dis = Vector3.Distance (prev_bodypart.position,cur_bodypart.position);

			Vector3 newpos = prev_bodypart.position;
			newpos.y = body_parts [0].position.y;

			float T = Time.deltaTime * dis / min_distance * curspeed;

			if (T > 0.5f) 
			{
				T = 0.5f;
			}

			cur_bodypart.position = Vector3.Lerp (cur_bodypart.position, newpos ,T);
			//cur_bodypart.rotation = Quaternion.Lerp (cur_bodypart.rotation, prev_bodypart.rotation, T) ;
		}
	}

	public void add_bodypart()
	{
		Transform newpart = (Instantiate (body_prefabe, body_parts [body_parts.Count - 1].position, body_parts [body_parts.Count - 1].rotation) as GameObject).transform;
		newpart.SetParent (transform);
		body_parts.Add (newpart);
	}
}
