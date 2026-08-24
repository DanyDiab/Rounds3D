using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class ButtonPressUtil {
    private static readonly Dictionary<InputAction, double> pressTimes = new Dictionary<InputAction, double>();
    private static readonly Dictionary<InputAction, float> pressBuffers = new Dictionary<InputAction, float>();


    private static float pressBufferMS = 200f;

    public static bool Pressed(InputAction action) {
        return Pressed(action, pressBufferMS);
    }

    public static bool Pressed(InputAction action, float timeToNextPress){
        if (action == null) {
            Debug.LogWarningFormat("action {0} is null", action.name);    
            return false;
        }

        if (!action.IsPressed()) {
            return false;
        }

        double currentTime = Time.unscaledTimeAsDouble * 1000f;

        if (pressTimes.TryGetValue(action, out double lastTimePressed)) {

            pressBuffers.TryGetValue(action, out float btnPressBuffer);
            if (currentTime - lastTimePressed <= btnPressBuffer) {
                return false;
            }
        }

        pressBuffers[action] = timeToNextPress;
        pressTimes[action] = currentTime;

        return true;
    }

// MAKE SURE TO CALL THESE!! This will unregister a button to free up memory
    public static void UnRegisterButton(InputAction action){
        if(pressTimes.ContainsKey(action)) pressTimes.Remove(action);
        if(pressBuffers.ContainsKey(action)) pressBuffers.Remove(action);

    }

// clears dictionary
    public static void UnRegisterAll(){
        pressTimes.Clear();
        pressBuffers.Clear();
    }
}