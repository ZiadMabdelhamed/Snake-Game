using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class snake_move2 : MonoBehaviour {

	public List<Transform> body_parts = new List<Transform>();

	public float min_distance = 0.25f;

	public float speed = 1 ;

	public float rotation_speed = 50;

	public GameObject body_prefabe;

	private float dis;
	private Transform cur_bodypart;
	private Transform prev_bodypart;

	public int begain_size;

	void Start () 
	{
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

		if (Input.GetAxis ("Horizontal") != 0) 
		{
			body_parts [0].Rotate (Vector3.up * rotation_speed * Time.deltaTime * Input.GetAxis ("Horizontal"));
		}

		for (int i = 1; i < body_parts.Count; i++) 
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
			cur_bodypart.rotation = Quaternion.Lerp (cur_bodypart.rotation, prev_bodypart.rotation ,T);
		}
	}

	public void add_bodypart()
	{
		Transform newpart = (Instantiate (body_prefabe, body_parts [body_parts.Count - 1].position, body_parts [body_parts.Count - 1].rotation) as GameObject).transform;
		newpart.SetParent (transform);
		body_parts.Add (newpart);
	}
}
