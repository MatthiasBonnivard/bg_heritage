using UnityEngine;
using System.Collections;

//When creating a new Breathing Game extend this class to have a Calibration before begining the game activity

// care of all calibration (not game) stuff so this class only focus on transition and refresh view.
public class CalibrationController : MonoBehaviour {

    //TODO This class in heritage was only related to the game ! 
    // Maybe make a generic class for player control when the core is more advanced
	//public PlayerController playerController;

	private float smoothTime;
	private float yVelocity;
	private float xVelocity;

	private CalibrationState state=CalibrationState.WAITING;

	private InputController_I inputController;
	
	private float volume;
	private static float volumeMaxCalibrated;
	private float thresholdFactor=0.5f;
	private float maxVolume=Breathing.SupposedPatientMaxVolume;

    //This a bar for calibration that was used in Heritage but it is possible to implement
    //any other display for calibration
	public Bar calibrationBar;

	// Use this for initialization
    // Need to implement the input controller here. Use keyBoard control for simulation
	void Start () {}
	
	// Update is called once per frame
	void Update () {

		inputController.Update ();

		DetectStateChanging ();

		UpdateState ();
	}
	
	protected void DetectStateChanging(){
		
		switch (state) {
		case CalibrationState.WAITING:
			if (inputController.GetInputState()==BreathingState.EXPIRATION){
				state=CalibrationState.CHARGING;
                //Herited method content to override
                CalibrationStateWaiting();
				Update();
			}
			break;
		case CalibrationState.CHARGING:
			if(inputController.GetInputState()!=BreathingState.EXPIRATION){
                //Herited method content to override
                CalibrationStateCharging();
			}
			break;
		case CalibrationState.TO_GAME_ANIMATION:
            //Herited method content to override
            CalibrationStateToGameAnimation();
			break;
		case CalibrationState.FAIL_ANIMATION:
            //Herited method content to override
            CalibrationStateFailAnimation();
			break;
		}
	}

    //Method to implement on the daughter class of a specific Breathing Game
    //These 4 class correspond to the different states of the calibration and shall be calle

    public virtual void CalibrationStateWaiting()
    {
        //FILL HERE IN THE HERITED CLASS
    }
    public virtual void CalibrationStateCharging()
    {
        //FILL HERE IN THE HERITED CLASS
    }

    public virtual void CalibrationStateToGameAnimation()
    {
        //FILL HERE IN THE HERITED CLASS
    }

    public virtual void CalibrationStateFailAnimation()
    {

    }

    //Calibration calculation Methods

	protected void UpdateState(){
		switch (state) {
		case CalibrationState.WAITING:
			break;
		case CalibrationState.CHARGING:
			volume+=inputController.GetStrength()*Time.deltaTime*Breathing.StrengthToVolumeFactor;
			calibrationBar.Update(volume/maxVolume);
			break;
		}
	}

	public void Move(GameObject objectToMove, Vector3 destination, ref Vector3 velocity, float smoothTime){
		float vX = velocity.x;
		float vY = velocity.y;
		float vZ = velocity.z;
		float x = Mathf.SmoothDamp (objectToMove.transform.position.x, destination.x, ref vX, smoothTime * Time.deltaTime);
		float y = Mathf.SmoothDamp (objectToMove.transform.position.y, destination.y, ref vY, smoothTime * Time.deltaTime);
		float z = Mathf.SmoothDamp (objectToMove.transform.position.z, destination.z, ref vZ, smoothTime * Time.deltaTime);
		objectToMove.transform.position = new Vector3 (x, y, z);
		velocity = new Vector3 (vX, vY, vZ);
	}
	
	public static float VolumeMaxCalibrated{
		get{ return volumeMaxCalibrated;}
	}
	
	public static float StrengthCalibrationFactor{
		get{return Breathing.SupposedPatientMaxVolume/volumeMaxCalibrated;}
	}
}
