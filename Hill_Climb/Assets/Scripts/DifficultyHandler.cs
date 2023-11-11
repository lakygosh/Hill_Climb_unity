using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI  _difficultyText;
    [SerializeField]
    private Sprite easySprite;
    [SerializeField]
    private Sprite mediumSprite;
    [SerializeField]
    private Sprite hardSprite;
    // Start is called before the first frame update
    void Start()
    {
        var dropdown = GetComponent<TMP_Dropdown>();
        switch (Difficulty.SELECTED_DIFFICULTY)
        {
            case 0:
                dropdown.image.sprite = easySprite;
                _difficultyText.text = "Easy";
                break;
            case 1:
                dropdown.image.sprite = mediumSprite;
                _difficultyText.text = "Medium";
                break;
            case 2:
                dropdown.image.sprite = hardSprite;
                _difficultyText.text = "Hard";
                break;
        }
        dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dropdown);
        });
    }

    private void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                dropdown.image.sprite = easySprite;
                _difficultyText.text = "Easy";
                Difficulty.SELECTED_DIFFICULTY = Difficulty.EASY;
                break;
            case 1:
                dropdown.image.sprite = mediumSprite;
                _difficultyText.text = "Medium";
                Difficulty.SELECTED_DIFFICULTY = Difficulty.MEDIUM;
                break;
            case 2:
                dropdown.image.sprite = hardSprite;
                _difficultyText.text = "Hard";
                Difficulty.SELECTED_DIFFICULTY = Difficulty.HARD;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
