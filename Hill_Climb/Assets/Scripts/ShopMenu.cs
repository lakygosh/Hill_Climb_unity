using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Car;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{ 
    [SerializeField]
    private List<Button> buttons;

    [SerializeField] private Canvas shopCanvas;
    [SerializeField] private Canvas carBuyingCanvas;
    [SerializeField] private Image carBuyingImage;
    [SerializeField] private TextMeshProUGUI carBuyingText;
    [SerializeField] private Image carBuyingCoin;
    private Player _activePlayer;
    private int _lastSelected;
    [SerializeField]
    private TextMeshProUGUI coinsText;

    private Vector3 offset;

    private void Start()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener((() => ButtonClicked(button)));
            Button select = button.transform.Find("Select").GetComponent<Button>();
            select.onClick.AddListener(()=>ButtonClicked(button));
            
            if (PlayerManager.GetSelectedPlayer().playerData.UnlockedCars.
                Contains(button.transform.Find("Image").GetComponent<Image>().sprite.name))
            {
                button.transform.Find("PriceText").gameObject.SetActive(false);
                button.transform.Find("Coin").gameObject.SetActive(false);
                button.transform.Find("Select").gameObject.SetActive(true);
            }

            if (PlayerManager.GetSelectedPlayer().playerData.SelectedCar == button.transform.Find("Image").GetComponent<Image>().sprite.name)
            {
                _lastSelected=buttons.IndexOf(button);
                button.transform.Find("Select").GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
                button.Select();

            }
        }
        coinsText.text = PlayerManager.GetSelectedPlayer().playerData.Coins.ToString("f0");
    }

    private void ButtonClicked(Button clickedButton)
    {
        
        if (clickedButton != null)
        {
            if (clickedButton.transform.Find("Select").gameObject.activeSelf)
            {
                buttons[_lastSelected].transform.Find("Select").GetComponentInChildren<TextMeshProUGUI>().text = "Select";   
                _lastSelected=buttons.IndexOf(clickedButton);
                clickedButton.transform.Find("Select").GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
                PlayerManager.GetSelectedPlayer().playerData.SelectedCar = clickedButton.transform.Find("Image").GetComponent<Image>().sprite.name;
                StartCoroutine(PlayerManager.SaveSelectedCar(PlayerManager.GetSelectedPlayer()));
                return;
            }
            
            Image clickedImage = clickedButton.transform.Find("Image").GetComponent<Image>();
            TextMeshProUGUI clickedText = clickedButton.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();
            Image clickedCoinImage = clickedButton.transform.Find("Coin").GetComponent<Image>();
            
            
            if (clickedImage != null && clickedText != null && clickedCoinImage != null)
            {
                AlignObjects(clickedText, clickedCoinImage);
                carBuyingImage.sprite = clickedImage.sprite;
                carBuyingImage.enabled = true;
                carBuyingText.text = clickedText.text;
                carBuyingCoin.sprite = clickedCoinImage.sprite;
            }
            
            gameObject.SetActive(false);
            carBuyingCanvas.gameObject.SetActive(true);

        }
    }

    private void AlignObjects(TextMeshProUGUI clickedText, Image clickedCoinImage)
    {
        offset = new Vector3(clickedCoinImage.transform.position.x - clickedText.transform.position.x+40,0,0);

        carBuyingCoin.transform.position = carBuyingText.transform.position + offset;
    }


    public void FirstPage()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }
    
    public void SecondPage()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);   
    }

    public void Cancel()
    {
        SceneManager.UnloadSceneAsync("Shop");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    public void CancelBuying()
    {
        carBuyingCanvas.gameObject.SetActive(false);
        gameObject.SetActive(true);
        
    }
    
    public void BuyCar()
    {
        string numericPart = Regex.Match(carBuyingText.text, @"\d+").Value;
        int price = Int32.Parse(numericPart);
        if (PlayerManager.GetSelectedPlayer().playerData.Coins >= price)
        {
            carBuyingCanvas.gameObject.SetActive(false);
            gameObject.SetActive(true); 
            PlayerManager.GetSelectedPlayer().playerData.Coins -= price;
            StartCoroutine(PlayerManager.SaveCoins(PlayerManager.GetSelectedPlayer()));
            PlayerManager.GetSelectedPlayer().playerData.UnlockedCars.Add(carBuyingImage.sprite.name);
            StartCoroutine(PlayerManager.SaveUnlockedCars(PlayerManager.GetSelectedPlayer()));
            PlayerManager.GetSelectedPlayer().playerData.SelectedCar = carBuyingImage.sprite.name;
            StartCoroutine(PlayerManager.SaveSelectedCar(PlayerManager.GetSelectedPlayer()));
        }
        else
        {
            carBuyingCanvas.gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        foreach (Button button in buttons)
        {
            if (PlayerManager.GetSelectedPlayer().playerData.UnlockedCars.
                Contains(button.transform.Find("Image").GetComponent<Image>().sprite.name))
            {
                button.transform.Find("PriceText").gameObject.SetActive(false);
                button.transform.Find("Coin").gameObject.SetActive(false);
                button.transform.Find("Select").gameObject.SetActive(true);
            }
            if (PlayerManager.GetSelectedPlayer().playerData.SelectedCar == button.transform.Find("Image").GetComponent<Image>().sprite.name)
            {
                buttons[_lastSelected].transform.Find("Select").GetComponentInChildren<TextMeshProUGUI>().text = "Select";
                _lastSelected=buttons.IndexOf(button);
                button.transform.Find("Select").GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
                button.Select();
                PlayerManager.GetSelectedPlayer().playerData.SelectedCar = button.transform.Find("Image").GetComponent<Image>().sprite.name;
                StartCoroutine(PlayerManager.SaveSelectedCar(PlayerManager.GetSelectedPlayer()));
            }
        }
        coinsText.text = PlayerManager.GetSelectedPlayer().playerData.Coins.ToString("f0");
    }
}
