using System;

/// <summary>
/// Implements this interface to manage others input specific to your game
/// </summary>
public interface InputController_I_Game_Spec
{
    /// <summary>
    /// THis method is specific to Heritage Breathing Game
    /// If necessary replace here all the needed methods to use your new breathing game specificity
    /// Determines whether the patient is holding the moving input or not.
    /// </summary>
    bool IsMoving();

}