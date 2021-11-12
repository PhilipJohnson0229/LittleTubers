using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopPanel;
    public int _currentSelectedItem;
    public int _currentItemCost;
    public string _itemName;

    [SerializeField]
    private Player _player;

    public void SelectItem(int _selection) 
    {
        Debug.Log("SelectItem() : " + _selection);

        switch (_selection) 
        {
            case 0:
                UIManager.instance.UpdateSelection(79);
                _currentSelectedItem = 0;
                _currentItemCost = 3;
                _itemName = "Flame Sword";
                break;
            case 1:
                UIManager.instance.UpdateSelection(-18);
                _currentSelectedItem = 1;
                _currentItemCost = 5;
                _itemName = "Boots of Light";
                break;
            case 2:
                UIManager.instance.UpdateSelection(-103);
                _currentSelectedItem = 2;
                _currentItemCost = 8;
                _itemName = "Keys to the castle";
                break;
        }
    }

    public void BuyItem() 
    {
        if (_player.GetCoinsCount() >= _currentItemCost)
        {
            if (_currentSelectedItem == 2)
            {
                GameManager.instance._hasKeyToCastle = true;
            }

            _player.SpendCoin(_currentItemCost);
            UIManager.instance.Notification("You just bought the " + _itemName);
            _shopPanel.SetActive(false);
        }
        else
        {
            UIManager.instance.Notification("Not enough gems, you fool");
            //close the shop for now
            _shopPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();

            if (_player != null)
            {
                UIManager.instance.UpdateCoins(_player.GetCoinsCount());
            }

            _shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _shopPanel.SetActive(false);
        }
    }
}
