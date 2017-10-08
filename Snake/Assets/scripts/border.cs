using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this class handle the game border and obsticales
public class border : MonoBehaviour {

	// four points for screen boundaries
	public Vector3 point_1;
	public Vector3 point_2;
	public Vector3 point_3;
	public Vector3 point_4;

	// get border gameobject
	public GameObject border_prefab;

	//public GameObject up_tunnel;
	//public GameObject down_tunnel;

	// list for screen border points
	public List<Vector3> border_points = new List<Vector3>();
	// obsticales game object 
	public List<GameObject> obsticales_prefabs = new List<GameObject>();


	void Start () {
		/// create border in the run time 
		create_border ();
		// set the obstacles in there position acording to screen resolution
		create_obstacles ();
		//create_tunnel ();
	}
	


	// get the coordinates acording to the camera to fit the screen 
	void calculate_screen()
	{
		
		point_1 = Camera.main.ScreenToWorldPoint (new Vector3(0,0,0));
		point_2 = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,0,0));
		point_3 = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height*0.87f,0));
		point_4 = Camera.main.ScreenToWorldPoint (new Vector3(0,Screen.height*0.87f,0));

	}

	void create_border()
	{
		
		calculate_screen ();

		// get coordinates for the point in x & z 
		Vector3 spawn_point1 = new Vector3 (point_1.x, 0, point_1.z);
		Vector3 spawn_point2 = new Vector3 (point_2.x, 0, point_2.z);
		Vector3 spawn_point3 = new Vector3 (point_3.x, 0, point_3.z);
		Vector3 spawn_point4 = new Vector3 (point_4.x, 0, point_4.z);

		// insert points to list
		border_points.InsertRange (border_points.Count, new Vector3[] {spawn_point1, spawn_point2, spawn_point3,spawn_point4});


		for(int i=0;i<border_points.Count;i++)
		{
			if ((i + 1) == border_points.Count) 
			{
				// set the last border between point 4 & pint 1
				get_border (border_points [i],border_points [0]);

			} 
			else
			{
				// set all border except last one
				get_border (border_points [i],border_points [i + 1]);

			}
		}
		///  set the middle one
		//Vector3 mid_1 = (spawn_point3 + spawn_point2)/2;
		//Vector3 mid_2 = (spawn_point1 + spawn_point4)/2;

		//get_border (mid_1,mid_2);

	}

	void create_obstacles()
	{
		calculate_screen ();

		Vector3 spawn_point = Vector3.zero;
		int value =2;
		for (int i = 0; i < obsticales_prefabs.Count; i++) 
		{

			// get positions for the obsticales to fit with screen resolution
			spawn_point = new Vector3 ((int)point_3.x-value-2, 0, (int)point_3.z - value-5);

			if (i == obsticales_prefabs.Count - 1) 
			{
				/// make a different position for the last obsticales  
				spawn_point = new Vector3 ((int)point_1.x+6, 0, (int)point_3.z -5);
			}

			// create obsticales && set parent 
			GameObject obstacle = (GameObject)Instantiate (obsticales_prefabs[i], spawn_point, Quaternion.identity);
			obstacle.transform.parent = gameObject.transform;
			value += 10;
		}
	}

	/*void create_tunnel()
	{
		Vector3 up_tunnel_pos = new Vector3 ((int)point_3.x*0.9f, 0, (int)point_3.z*0.9f);
		Vector3 down_tunnel_pos = new Vector3 ((int)point_1.x ,0,(int)point_1.z);

		GameObject T1 = (GameObject)Instantiate (up_tunnel, up_tunnel_pos, Quaternion.identity);
		T1.transform.parent = gameObject.transform;
		GameObject T2 = (GameObject)Instantiate (down_tunnel, down_tunnel_pos, Quaternion.identity);
		T2.transform.parent = gameObject.transform;

	}*/

	// this funtion take 2 postions which is the coordinates of the screen & create a game object between this 2 position 
	// & scale  this game object to make aline 
	void get_border(Vector3 first_pos,Vector3 second_pos)
	{
		float dis = Vector3.Distance (first_pos, second_pos);
		GameObject border_line = (GameObject)Instantiate (border_prefab, first_pos, Quaternion.identity);
		border_line.transform.parent = gameObject.transform;
		border_line.transform.position = Vector3.Lerp (first_pos, second_pos, 0.5f);
		border_line.transform.LookAt (first_pos);
		border_line.transform.localScale = new Vector3 (1f, 1f, Vector3.Distance (first_pos,  second_pos));

	}

}


