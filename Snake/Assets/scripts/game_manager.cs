using UnityEngine;
using System.Collections;

public class game_manager : MonoBehaviour {

	public Vector3 min_screenpoints;
	public Vector3 max_screenpoints;

	public GameObject fruit;

	public int score;
	void Start () {
		score = 0;
	}
	

	void Update () {

	}


	void calculate_screen()
	{
		min_screenpoints = Camera.main.ScreenToWorldPoint (new Vector3(0,0,0));
		max_screenpoints = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height,0));
	}

	public void spawn_fruit()
	{
		Vector3 spawn_point = Vector3.zero;
		calculate_screen ();
		do{
			spawn_point = new Vector3 (Random.Range ((int)min_screenpoints.x +1, (int)max_screenpoints.x-1), 0, Random.Range ((int)min_screenpoints.z+1, (int)max_screenpoints.z-1));
		}while(Physics.Raycast(spawn_point+Vector3.up*10,Vector3.down));
		Instantiate (fruit, spawn_point, Quaternion.identity);

	}

	public int add_score(int sco)
	{
		score += sco;
		return score;
	}
}
