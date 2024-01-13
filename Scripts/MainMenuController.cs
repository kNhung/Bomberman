using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject nameOptionPanel;
    public GameObject highScoreOptionPanel;
    public GameObject levelOptionPanel;

    private int currentOptionIndex = 0;
    private GameObject[] optionPanels;

    private void Start()
    {
        optionPanels = new GameObject[] { nameOptionPanel, highScoreOptionPanel, levelOptionPanel };
        SetActivePanel(mainMenuPanel);
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput > 0)
        {
            MoveSelectionUp();
        }
        else if (verticalInput < 0)
        {
            MoveSelectionDown();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectOption();
        }
    }

    private void MoveSelectionUp()
    {
        currentOptionIndex = (currentOptionIndex - 1 + optionPanels.Length) % optionPanels.Length;
        UpdateOptionSelection();
    }

    private void MoveSelectionDown()
    {
        currentOptionIndex = (currentOptionIndex + 1) % optionPanels.Length;
        UpdateOptionSelection();
    }

    private void UpdateOptionSelection()
    {
        // Update visual feedback for option selection (e.g., highlight the selected option)
        // You can implement this based on your UI design.

        // For simplicity, we'll just print the selected option index.
        Debug.Log("Selected Option: " + currentOptionIndex);
    }

    private void SelectOption()
    {
        // Handle the selected option based on the currentOptionIndex
        switch (currentOptionIndex)
        {
            case 0:
                SetActivePanel(nameOptionPanel);
                break;
            case 1:
                SetActivePanel(highScoreOptionPanel);
                break;
            case 2:
                SetActivePanel(levelOptionPanel);
                break;
        }
    }

    private void SetActivePanel(GameObject activePanel)
    {
        mainMenuPanel.SetActive(false);
        nameOptionPanel.SetActive(false);
        highScoreOptionPanel.SetActive(false);
        levelOptionPanel.SetActive(false);

        activePanel.SetActive(true);
    }
}
