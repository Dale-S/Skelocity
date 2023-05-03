using System.Collections.Generic;
using UnityEngine;

public class ButtonTracker : MonoBehaviour
{
    // Dictionary to store the count of button presses
    private Dictionary<string, int> buttonPressCounts = new Dictionary<string, int>();

    // Public counter that goes down based on the number of times any button is pressed
    public int remainingClicks;

    // Check if buttons can still be pressed
    public bool CanPressButtons()
    {
        return remainingClicks > 0;
    }

    public bool ButtonClicked(GameObject button)
    {
        string buttonName = button.name;

        // If the button name is not in the dictionary, add it with an initial count of 0
        if (!buttonPressCounts.ContainsKey(buttonName))
        {
            buttonPressCounts[buttonName] = 0;
        }

        // Increment the count for the button
        buttonPressCounts[buttonName]++;

        // Decrease the remaining clicks counter
        remainingClicks--;

        // Log the button name and the number of times it has been pressed
        Debug.Log("Button clicked: " + buttonName + ", pressed " + buttonPressCounts[buttonName] + " times.");
        Debug.Log("Remaining clicks: " + remainingClicks);

        // Return true if the button should be disabled, i.e., it has been clicked three times
        return buttonPressCounts[buttonName] >= 3;
    }
}