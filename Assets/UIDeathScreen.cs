using UnityEngine;

public class UIDeathScreen : MonoBehaviour
{

    private void Start()
    {
        EventManager.Death += ShowDeathScreen;
        gameObject.SetActive(false);
    }

    private void ShowDeathScreen()
    {
        // Display the death screen.
        gameObject.SetActive(true);
    }
}
