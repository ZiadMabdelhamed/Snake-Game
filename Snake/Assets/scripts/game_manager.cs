using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// handle instantiate the furits and the score 
public class game_manager : MonoBehaviour {

	// get he minimum & maximum point of the screen to create fruit in this range  
	public Vector3 min_screenpoints;
	public Vector3 max_screenpoints;

	// prefab for the fruit 
	public GameObject fruit;


	public int score;

	// to show the score 
	public Text score_count;

	void Start () {
		score = 0;
		// find the score game object by tag 
		score_count = GameObject.FindWithTag ("score_counter").GetComponent<Text> ();
	}
	

	void Update () {

	}

	// calculate the maximum & minimum point acording to the screen resolution
	void calculate_screen()
	{
		min_screenpoints = Camera.main.ScreenToWorldPoint (new Vector3(0,0,0));
		max_screenpoints = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height*0.87f,0));
	}

	public void spawn_fruit()
	{
		Vector3 spawn_point = Vector3.zero;
		calculate_screen ();
		// to put the fruit in empty place by making a ray to check if there is a collider or not 
		do{
			spawn_point = new Vector3 (Random.Range ((int)min_screenpoints.x +1, (int)max_screenpoints.x-1), 0, Random.Range ((int)min_screenpoints.z+1, (int)max_screenpoints.z-1));
		}while(Physics.Raycast(spawn_point+Vector3.up*10,Vector3.down));

		// create fruite 
		// i made all the game object inside one parent to destroy all at the same time
		GameObject fruits = (GameObject)Instantiate (fruit, spawn_point, Quaternion.identity);
		fruits.transform.parent = gameObject.transform;
	}

	// this function called from the snake_head to increase the score when a fruit get in the trigger 
	public int add_score(int sco)
	{
		score += sco;
		score_count.text = score.ToString();
		return score;
	}
}
