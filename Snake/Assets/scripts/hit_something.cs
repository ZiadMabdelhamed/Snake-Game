using UnityEngine;
using System.Collections;

public class hit_something : MonoBehaviour {

	public GameObject snake;

	public GameObject game_manager;

	game_manager game_man;
	snake snake_cla;

	void Start()
	{
		game_man = game_manager.GetComponent<game_manager> ();
		snake_cla = snake.GetComponent<snake> ();
		game_man.spawn_fruit ();
	}

	void OnTriggerEnter(Collider other) {
			if (other.gameObject.layer == 9) 
			{
				Destroy (other.gameObject);
				snake_cla.Add_body ();
				game_man.add_score (10);
				game_man.spawn_fruit ();

			}

			if (other.gameObject.layer == 8) 
			{
				snake.GetComponent<snake> ().enabled = false;
			}
	}
}