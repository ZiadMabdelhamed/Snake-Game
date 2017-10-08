using UnityEngine;
using System.Collections;

// this class handle all menus and start or end the game 
public class button_manager : MonoBehaviour {
	// main menue
	public GameObject menu;

	// score & pause button 
	public GameObject inside_menu;

	// prefab for the lvl to be Instantiated in the game 
	public Transform lvl;

	// place to create the level
	public Transform lvl_place;

	// get the game object of the level to destroy it when the player go back to the menu or restart
	public GameObject cur_lvl;

	// to know if the player in the game or menu
	public current_screen the_state;

	// to get know if the game is going or not
	public pause_play_state pause_or_play;
	// game object to get pause & play button to switch between then when the player pause or continue the game 
	public GameObject pause_btn;
	public GameObject play_btn;

	// menu activated only when the player died to ask if he want to restart or back to the meun
	public GameObject dead_menu;


	void Start () 
	{
		// set start values of the menu members
		menu.SetActive (true);
		pause_btn.SetActive (true);
		play_btn.SetActive (false);
		inside_menu.SetActive (false);
		the_state = current_screen.menu;
		pause_or_play = pause_play_state.play; 
		dead_menu.SetActive (false);
	}
	

	void Update () 
	{
		// exit the game 
		if (Input.GetKeyDown (KeyCode.Escape) && the_state == current_screen.menu) 
		{
			
			Application.Quit ();
		} 

		// back to the meun 
		if (Input.GetKeyDown (KeyCode.Escape) && the_state == current_screen.game) 
		{
			back_to_menu ();
		}
	}

	// play function called from play button to start the game 
	public void play(bool plays)
	{
		if (plays) 
		{
			menu.SetActive (false);
			inside_menu.SetActive (true);
			the_state = current_screen.game;
			Transform level = (Transform)Instantiate (lvl, lvl_place.position, Quaternion.identity);
			cur_lvl = level.transform.gameObject;
		}
			
	}

	// called from the pause & play buttons 
	public void pause_play(bool pause)
	{
		if (pause == true && pause_or_play == pause_play_state.play) {
			pause_btn.SetActive (false);
			play_btn.SetActive (true);
			Time.timeScale = 0;
			pause_or_play = pause_play_state.pause;
		} 
		else if (pause == false && pause_or_play == pause_play_state.pause) 
		{
			play_btn.SetActive (false);
			pause_btn.SetActive (true);
			Time.timeScale = 1;
			pause_or_play = pause_play_state.play;
		}
	}

	// this function called when the player is died to continue or leave the game 
	public void dead_menu_option(bool active_dead_menu)
	{
		if (active_dead_menu) {
			dead_menu.SetActive (true);
			play_btn.SetActive (false);
			pause_btn.SetActive (false);
		} 
		else 
		{
			dead_menu.SetActive (false);
		}

	}

	// called from restart button to destroy the current level and instantiate anew one 
	public void restart ()
	{
		Destroy (cur_lvl);
		dead_menu.SetActive (false);
		Transform level = (Transform)Instantiate (lvl, lvl_place.position, Quaternion.identity);
		cur_lvl = level.transform.gameObject;
		pause_btn.SetActive (true);
	}

	// called from escape button & back to menu to get the player back to the main menu
	public void back_to_menu ()
	{
		Destroy (cur_lvl);
		inside_menu.SetActive (false);
		menu.SetActive (true);
		the_state = current_screen.menu;
		Time.timeScale = 1;
		play_btn.SetActive (false);
		pause_btn.SetActive (true);
		pause_or_play = pause_play_state.play;
		dead_menu.SetActive (false);
	}

}

// get the state of the screen 
public enum current_screen
{
	game,
	menu,

}

// to know if the game is paused or not 
public enum pause_play_state
{
	pause,
	play,

}
