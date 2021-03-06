using FlapiUnity;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// All level controller should inherit this class to manage the game.
/// A hand made level could directly use this script.
/// </summary>
public class LevelController : MonoBehaviour
{
	/// <summary>The actual game's state.</summary>
	protected GameState state = GameState.CALIBRATION;
	/// <summary>The player game object.</summary>
	public PlayerController player;
    /// <summary>The input controller</summary>
	protected OnlyExpirationInputController inputController;


	/// <summary>The exercice the patient should follow (in loop) and after which the level should be.</summary>
	protected DecreasingAutogenicDrainage exercice;
	//TODO Doc
	private float volumeMaxCalibrated;

    public OnlyExpirationInputController InputController
    {
		get {
			return inputController;
		}
	}
	
	public List<GameObject> listToEnableAtGame;

	/// <summary>
	/// Create the exercice and the input controller.
	/// Launches the player enter animation.
	/// </summary>
	public virtual void BuildAndStart ()
	{		

		//ioController = new FlapiIOController (audio);

        //To test the Breathing game core we will juste try it with the KeyBoard in a first step

		inputController = new KeyboardInputController (exercice,3.0f);
        //inputController = new PepInputController();

        exercice = new DecreasingAutogenicDrainage(3, 3, 3, 1.5f, 3.0f, 10.0f, 0.5f, inputController);

        StartCoroutine (WaitForGame ());
	}
	
	/// <summary>
	/// Called once per frame. Check the player progress and move it (after the input controller).
	/// Can warn of a transition (not implemented yet).
	/// </summary>
	void Update ()
	{
		if (state == GameState.GAME) {
			inputController.Update();
			exercice.CheckProgress(inputController, Time.deltaTime);
			player.Move(inputController,  exercice);
			Transition transition=exercice.GetTransition();
			if(transition!=Transition.NONE){
				Debug.Log(transition);
			}
		}
	}
	
	/// <summary>
	/// Convert a breathing's volume (from 0 to 1) into the world's Y axis.
	/// </summary>
	/// <returns>The corresponding world's Y value.</returns>
	/// <param name="respirationValue">The breathing's volume value (from 0 to 1).</param>
	protected float breathingVolumeToWorldHeight(float respirationValue){
		float worldHeight = respirationValue * (player.maxHeight - player.minHeight);
		worldHeight += player.minHeight;
		return worldHeight;
	}
	
	/// <summary>
	/// Convert an exercice "position" vector into a position in the game space.
	/// </summary>
	/// <returns>The corresponding position in the game space.</returns>
	/// <param name="vector">The exercice "position".</param>
	public Vector3 ExerciceToPlayer(Vector3 vector){
		vector.y = breathingVolumeToWorldHeight (vector.y);
		vector.x *= player.HorizontalSpeed;
		
		return vector;
	}

	/// <summary>
	/// Launch the player animation and "yield" when it's over.
	/// Can be launch as a coroutine.
	/// </summary>
	protected IEnumerator WaitForGame ()
	{
		while (state != GameState.GAME) {
			yield return null;
		}			
		
		foreach (GameObject gameObject in listToEnableAtGame) {
			gameObject.SetActive (true);
		}
	}
	
	/// <summary>Gets or sets the actual game's state.</summary>
	public GameState GameState{
		get{ return state;}
		set{ state=value;}
	}

	/// <summary>Gets the exercice the patient should follow (in loop) and after which the level should be.</summary>
	public Exercice Exercice{
		get { return exercice;}
	}

	public float VolumeMaxCalibrated {
		get{ return volumeMaxCalibrated;}
		set{ 
			volumeMaxCalibrated = value;
			exercice.VolumeMaxCalibrated=value;
		}
	}
	
}
