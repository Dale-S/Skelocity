using UnityEngine;
using UnityEngine.UI;

public class B_Click : MonoBehaviour
{
    // Reference to the ButtonTracker script
    public ButtonTracker buttonTracker;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Button component
        Button button = GetComponent<Button>();

        // Add a listener for the button click event
        button.onClick.AddListener(OnButtonClick);
    }

    // The method that gets called when the button is clicked
    void OnButtonClick()
    {
        // Check if buttons can still be pressed
        if (buttonTracker.CanPressButtons())
        {
            // Notify the ButtonTracker that this button was clicked
            bool shouldDisable = buttonTracker.ButtonClicked(gameObject);

            // If the button has been clicked three times, disable it
            if (shouldDisable)
            {
                GetComponent<Button>().interactable = false;
            }
        }
    }
}