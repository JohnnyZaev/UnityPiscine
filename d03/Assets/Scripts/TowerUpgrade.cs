using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrade : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text upgradeLabel;
    [SerializeField] private Button sellButton;
    [SerializeField] private TMP_Text sellLabel;
    [SerializeField] private GameObject defaultTile;

    private towerScript _currentTower;

    public void SetTower(towerScript towerScript)
    {
        _currentTower = towerScript;
        UpdateInfoAboutCurrentTower(gameManager.gm.playerEnergy);
    }

    public void UpdateInfoAboutCurrentTower(int currentEnergy)
    {
        Setup(_currentTower.upgrade, currentEnergy, upgradeLabel, upgradeButton, true);
        Setup(_currentTower.gameObject, currentEnergy, sellLabel, sellButton, false);
    }

    public void Upgrade()
    {
	    gameManager.gm.playerEnergy -= _currentTower.upgrade.GetComponent<towerScript>().energy;
        var tower = Instantiate(_currentTower.upgrade, _currentTower.transform.position, Quaternion.identity);
        Destroy(_currentTower.gameObject);
        SetTower(tower.GetComponent<towerScript>());
    }

    public void Sell()
    {
        gameManager.gm.playerEnergy += _currentTower.energy / 2;
        if (_currentTower.downgrade)
        {
            var tower = Instantiate(_currentTower.downgrade, _currentTower.transform.position, Quaternion.identity);
            Destroy(_currentTower.gameObject);
            SetTower(tower.GetComponent<towerScript>());
        }
        else
        {
            Instantiate(defaultTile, _currentTower.transform.position, Quaternion.identity);
            Destroy(_currentTower.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void Setup(GameObject target, int currentEnergy, TMP_Text label, Button button, bool buy)
    {
        if (target)
        {
            var towerScript = target.GetComponent<towerScript>();
            var energy = towerScript.energy;
            if (!buy)
                energy /= 2;
            label.text = energy.ToString();
            if (buy)
            {
                var isEnoughEnergy = currentEnergy >= energy;
                label.color = isEnoughEnergy
                    ? Color.black
                    : Color.red;
                button.interactable = isEnoughEnergy;
            }
            
            button.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);
        }
    }
}
