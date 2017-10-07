using UnityEngine;
using System.Collections;

public class swipe : MonoBehaviour {

	public float max_time;
	public float min_swip_dist;

	float start_time;
	float end_time;

	Vector3 start_pos;
	Vector3 end_pos;

	float swipe_distance;
	float swipe_time;

	public enum swipe_direction {right,left,up,down,none};
	public swipe_direction swipe_dir;

	void Start () {
		swipe_dir = swipe_direction.none;
	}
	

	void Update () 
	{
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) //began
			{
				start_time = Time.time;
				start_pos = touch.position;
			}

			else if(touch.phase == TouchPhase.Ended)//ended
			{
				end_time = Time.time;
				end_pos = touch.position;

				swipe_distance = (end_pos - start_pos).magnitude;
				swipe_time = end_time - start_time;

				if (swipe_time < max_time && swipe_distance > min_swip_dist) 
				{
					swipes();
				}
			}
		}	
	}

	public void swipes()
	{
		Vector2 distance = end_pos - start_pos;
		if (Mathf.Abs (distance.x) > Mathf.Abs (distance.y)) 
		{
			//Debug.Log("horizontal.swip");
			if (distance.x > 0) 
			{
				//Debug.Log("right.swip");
				swipe_dir = swipe_direction.right;
			}
			if (distance.x < 0) 
			{
				//Debug.Log("left.swip");
				swipe_dir = swipe_direction.left;
			}
		}

		if (Mathf.Abs (distance.x) < Mathf.Abs (distance.y)) 
		{
			///Debug.Log("vertical.swip");
			if (distance.y > 0) 
			{
				//Debug.Log("up.swip");
				swipe_dir = swipe_direction.up;
			}
			if (distance.y < 0) 
			{
				//Debug.Log("down.swip");
				swipe_dir = swipe_direction.down;
			}
		}
	}
}
