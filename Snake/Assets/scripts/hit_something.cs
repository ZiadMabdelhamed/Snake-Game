using UnityEngine;
using System.Collections;

// this class manage all the action happend when the snake hit any game object in the scene (" fruit , body ,obstacales , border")
public class hit_something : MonoBehaviour {

	public GameObject snake;

	public GameObject game_manager;
	public GameObject M_camera;

	// object from game_manager && button_manager
	game_manager game_man;
	button_manager btn_manage;
	snake snake_cla;

	// get the all the score to Subtract this value from the score when the snake died to make score zero again
	int all_count ;

	void Start()
	{
		all_count = 0;

		game_man = game_manager.GetComponent<game_manager> ();
		M_camera = GameObject.FindWithTag ("MainCamera");

		// get the snake class to cll add function when the snake eat fruit 
		snake_cla = snake.GetComponent<snake> ();

		// spawn a new fruit after eating the last one
		game_man.spawn_fruit ();

	}

	void OnTriggerEnter(Collider other) {

		// layer 9 is a fruit layer to know if there is a fruit inside the trigger or a snake body 
			if (other.gameObject.layer == 9) 
			{
				// destroy fruit game object
				Destroy (other.gameObject);

				// call add body function inside the snake class
				snake_cla.Add_body ();

				// increase ths score by 10 
				game_man.add_score (10);
				
				// spawn a new fruit
				game_man.spawn_fruit ();
				
				// add the value to all_count 
				all_count += 10;

			}
			
		// layer 8 is snake body layer 
			if (other.gameObject.layer == 8) 
			{
				// disable the snake class to stop the movement of the snake  
				snake.GetComponent<snake> ().enabled = false;

			// active the dead menu contain ( Gameover statement & restart button & back to menu button  )
				btn_manage = M_camera.GetComponent<button_manager> ();
				btn_manage.dead_menu_option (true);
				game_man.add_score (-all_count);

			}
	}
}
