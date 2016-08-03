using UnityEngine;
using System.Collections;

public class HeritageCalibrationController : CalibrationController
{
    //TODO This class in heritage was only related to the game ! 
    // Maybe make a generic class for player control when the core is more advanced
    //public PlayerController playerController;
    private LevelController levelController;

    private float smoothTime = 20.0f;
    private float yVelocity = 0.0f;
    private float xVelocity = 0.0f;

    private CalibrationState state = CalibrationState.WAITING;

    private InputController_I inputController;

    private float volume;
    private static float volumeMaxCalibrated;
    private float thresholdFactor = 0.5f;
    private float maxVolume = Breathing.SupposedPatientMaxVolume;

    private Animator characterAnimator;
    private Bar calibrationBar;


    public override void CalibrationStateWaiting()
    {
        base.CalibrationStateWaiting();
        characterAnimator.SetBool("isExpiring", true);
    }

    public override void CalibrationStateToGameAnimation()
    {
        base.CalibrationStateToGameAnimation();
        if (characterAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Equals("Player_Swim"))
        {
            levelController.GameState = GameState.GAME;
            levelController.VolumeMaxCalibrated = volumeMaxCalibrated;
            this.enabled = false;
        }
    }

    public override void CalibrationStateFailAnimation()
    {
        base.CalibrationStateFailAnimation();
        if (characterAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Equals("Player_Calibration_Idle"))
        {
            state = CalibrationState.WAITING;
            volume = 0.0f;
            calibrationBar.Update(volume);
        }
    }

    public override void CalibrationStateCharging()
    {
        base.CalibrationStateCharging();
        characterAnimator.SetBool("isExpiring", false);
        if (volume >= thresholdFactor * maxVolume)
        {
            characterAnimator.SetBool("isThresholdReached", true);
            state = CalibrationState.TO_GAME_ANIMATION;
            volumeMaxCalibrated = volume;
            inputController.SetCalibrationFactor(CalibrationController.StrengthCalibrationFactor);
            levelController.BuildAndStart();
            calibrationBar.gameObject.SetActive(false);
        }
        else
        {
            state = CalibrationState.FAIL_ANIMATION;
            characterAnimator.SetBool("isThresholdReached", false);
        }
        volume = 0.0f;
    }


    // Use this for initialization
    void Start()
    {
        //inputController = new FlapiInputController (exercice, GetComponent<AudioSource>());
        inputController = new KeyboardSimpleInputController(10.0f);
        //In new Unity Version  this KeyBoard is easily manage through UI and a simple code line
        //To replace in the final code
    }

    // Update is called once per frame
    void Update()
    {

        inputController.Update();

        DetectStateChanging();

        UpdateState();
    }
}
