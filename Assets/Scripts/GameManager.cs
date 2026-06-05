using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int hp = 100;
    private int gold = 0;
    private int floor = 1;
    private bool isGameOver = false;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI eventText;
    public TextMeshProUGUI floorText;
    public Button[] doorButtons;
    public GameObject finalScorePanel;
    public TextMeshProUGUI finalFloorText;
    public TextMeshProUGUI finalGoldText;


    public void OpenDoor(int doorType)
    {
        if (isGameOver)
        {
            return;
        }

        Debug.Log("Door type = " + doorType);

        HandleRandomEvent(doorType);

        if (hp <= 0)
        {
            isGameOver = true;
            hp = 0;

            finalFloorText.text = "Final Floor: " + floor;
            finalGoldText.text = "Gold Collected: " + gold;
            finalScorePanel.SetActive(true);

            eventText.text = "Game Over!";
            SetDoorsInteractable(false);
        }
        else
        {
            floor++;
        }
        UpdateUi();
    }

    private void HandleRandomEvent(int doorType)
    {
        int probability = Random.Range(0, 100);

        if (doorType == 0)
        {
            ResolveDoorEvent(probability, 50, 80, 10, 30, 15, 35, 10, 30);
        }
        else if (doorType == 1)
        {
            ResolveDoorEvent(probability, 33, 66, 15, 35, 10, 30, 15, 35);
        }
        else if (doorType == 2)
        {
            ResolveDoorEvent(probability, 40, 50, 25, 60, 5, 20, 20, 45);
        }
    }

    private void ResolveDoorEvent(
        int probability,
        int treasureLimit,
        int healLimit,
        int minReward,
        int maxReward,
        int minHeal,
        int maxHeal,
        int minDamage,
        int maxDamage
        )
    {
        if (probability < treasureLimit)
        {
            ApplyTreasure(minReward, maxReward);
        }
        else if (probability < healLimit)
        {
            ApplyHeal(minHeal, maxHeal);
        }
        else
        {
            ApplyMonster(minDamage, maxDamage);
        }
    }




    private void ApplyMonster(int minDamage, int maxDamage)
    {
        int damage = Random.Range(minDamage, maxDamage) + floor;
        hp -= damage;
        eventText.text = "Monster dealt " + damage + " damage!";
        Debug.Log($"MONSTER | Damage: {damage} | HP: {hp} | GOLD: {gold} | FLOOR: {floor}");
    }

    private void ApplyHeal(int minHeal, int maxHeal)
    {
        int healPoint = Random.Range(minHeal, maxHeal);
        hp += healPoint;
        if (hp > 100)
        {
            hp = 100;
        }
        eventText.text = "You healed " + healPoint + " HP!";
        Debug.Log($"HEAL | Heal: {healPoint} | HP: {hp} | GOLD: {gold} | FLOOR: {floor}");
    }

    private void ApplyTreasure(int minReward, int maxReward)
    {
        int reward = Random.Range(minReward, maxReward);
        gold += reward;
        eventText.text = "You found " + reward + " gold!";
        Debug.Log($"TREASURE | Reward: {reward} | HP: {hp} | GOLD: {gold} | FLOOR: {floor}");
    }

    private void UpdateUi()
    {
        floorText.text = "FLOOR: " + floor;
        hpText.text = "HP: " + hp;
        goldText.text = "GOLD: " + gold;
    }

    public void SetDoorsInteractable(bool value)
    {
        foreach (Button button in doorButtons)
        {
            button.interactable = value;
        }
    }

    public void RestartGame()
    {
        isGameOver = false;
        hp = 100;
        gold = 0;
        floor = 1;
        eventText.text = "Choose a door...";
        finalScorePanel.SetActive(false);
        SetDoorsInteractable(true);
        UpdateUi();
    }

    private void Start()
    {
        eventText.text = "Choose a door...";
        finalScorePanel.SetActive(false);
        SetDoorsInteractable(true);
        UpdateUi();
    }
}