using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class CharacterStatsController : MonoBehaviour
{

    public TMP_Text scoreText;
    public int score;
    public TMP_Text currentSpeedText;
     public float speed;
    public TMP_Text currentRateText;
    public float rate;
    public TMP_Text currentDamageText;
    public int damage;
    private const string FirstRunKey = "IsFirstRun";
    public List<Equipment> ownedEquipment = new List<Equipment>();
    public List<Equipment> equippedEquipment = new List<Equipment>();
    public GameObject equipListView; // Reference to the UI panel or container for displaying equipment
    public GameObject equipOwnedListView; // Reference to the UI panel or container for displaying equipment
    public GameObject equippedListView; // Reference to the UI panel or container for displaying equipment
    public List<Equipment> availableEquipment = new List<Equipment>(); // List of all available equipment
    public GameObject equipmentPrefab; // Prefab used for displaying each equipment piece in the UI
    public GameObject equipmentOwnedPrefab; // Prefab used for displaying each equipment piece in the UI
    public GameObject equippedPrefab;
    public ScreenFader screenFader; // Assign in the inspector


    // Start is called before the first frame update
    void Start()
    {
        if(CheckFirstRun()){
            NewStats();
        }
        else{
            SetupStats();
        }

        DisplayAvailableEquipment();
        DisplayEquippedEquipment();

    }

    // Update is called once per frame


    public void BuyEquipment(Equipment equipment)
    {
        if (!ownedEquipment.Contains(equipment))
        {
            Debug.Log("Adding the following equipment" + equipment.EquipmentName);
            ownedEquipment.Add(equipment);
            // Subtract cost from player's currency, etc.
        }
    }

    public void EquipEquipment(Equipment equipment)
    {
        Debug.Log("Equip event Triggered");
        Debug.Log("Equipment Name : " + equipment.EquipmentName);
        if (ownedEquipment.Contains(equipment) && !equippedEquipment.Contains(equipment)&& equippedEquipment.Capacity-1 <5 && !ContainsWeaponEquipment(equipment))
        {
            Debug.Log("Adding equipment: " + equipment.EquipmentName);
            equippedEquipment.Add(equipment);
            equipment.AddEffect(GetPlayerData()); // Apply the equipment effects
        }
        else if(equippedEquipment.Capacity >=5){
            Debug.Log("Max number if equipment has been reached");
        }

        DisplayEquippedEquipment();
    }

    public bool ContainsWeaponEquipment(Equipment equipment)
    {
        foreach (var currentEquipment in equippedEquipment){

        
        if(currentEquipment is WeaponEquipment && equipment.GetType() == typeof(WeaponEquipment)){
            return true;
        }
        else if(currentEquipment is SpeedEquipment && equipment.GetType() == typeof(SpeedEquipment)){
            return true;
        }
        }
    
    return false; // No WeaponEquipment found in the list
}


    public void UnequipEquipment(Equipment equipment)
    {
        if (equippedEquipment.Contains(equipment))
        {
            Debug.Log("Removing equipment: " + equipment.EquipmentName);
            equippedEquipment.Remove(equipment);
            equipment.RemoveEffect(GetPlayerData());
        }
        DisplayEquippedEquipment();
    }

    // Call this method to apply all equipped equipment effects (e.g., on game start or respawn)
    public void ApplyEquippedEquipment()
    {
        foreach (Equipment equipment in equippedEquipment)
        {
            equipment.AddEffect(GetPlayerData());
        }
    }

    public void DisplayAvailableEquipment()
    {
        // Ensure the EquipListView is cleared before adding new items
        foreach (Transform child in equipListView.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a new UI element for each available equipment
        foreach (Equipment equipment in availableEquipment)
        {
            GameObject equipmentUIObject = Instantiate(equipmentPrefab, equipListView.transform);
            // Assuming the prefab has a script attached that can display equipment details
            EquipmentUI equipmentUI = equipmentUIObject.GetComponent<EquipmentUI>();
            if (equipmentUI != null)
            {
                equipmentUI.Setup(equipment);
            }
        }
    }
    public void DisplayOwnedEquipment()
    {
        // Ensure the EquipListView is cleared before adding new items
        foreach (Transform child in equipOwnedListView.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a new UI element for each available equipment
        foreach (Equipment equipment in ownedEquipment)
        {
            GameObject equipmentUIObject = Instantiate(equipmentOwnedPrefab, equipOwnedListView.transform);
            // Assuming the prefab has a script attached that can display equipment details
            EquipmentOwnedUI equipmentUI = equipmentUIObject.GetComponent<EquipmentOwnedUI>();
            EquipmentEquipButton button = equipmentUIObject.GetComponent<EquipmentEquipButton>();
            if (equipmentUI != null)
            {
                equipmentUI.Setup(equipment);
                button.AddEquipment(equipment);
            }
        }
    }

    public void DisplayEquippedEquipment()
    {
        // Ensure the EquipListView is cleared before adding new items
        foreach (Transform child in equippedListView.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a new UI element for each available equipment
        foreach (Equipment equipment in equippedEquipment)
        {
            GameObject equipmentUIObject = Instantiate(equippedPrefab, equippedListView.transform);
            // Assuming the prefab has a script attached that can display equipment details
            EquipmentOwnedUI equipmentUI = equipmentUIObject.GetComponent<EquipmentOwnedUI>();
            EquipmentEquipButton button = equipmentUIObject.GetComponent<EquipmentEquipButton>();
            if (equipmentUI != null)
            {
                equipmentUI.Setup(equipment);
                button.AddEquipment(equipment);
            }
        }
    }

    void NewStats(){
        speed = 2f;
        UpdateSpeedText();
        score = 0;
        UpdateScoreText();
        damage = 1;
        UpdateDamageText();
        rate = 3f;
        UpdateRateText();
    }
    void SetupStats(){
        LoadData();
        //speed = GetPlayerSpeed();
        UpdateSpeedText();
        //score = GetScore();
        UpdateScoreText();
        //damage = GetDamage();
        UpdateDamageText();
        //rate = GetRate();
        UpdateRateText();
    }

    bool CheckFirstRun()
    {
        // Check if the game has been run before
        if (PlayerPrefs.GetInt(FirstRunKey, 1) == 1)
        {
            Debug.Log("This is the first run of the game.");

            // Perform any first-run initializations here
            
            // Set the first run key to indicate that the game has been run
            PlayerPrefs.SetInt(FirstRunKey, 0);

            // It's important to save PlayerPrefs after making changes
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            Debug.Log("The game has been run before.");
            return false;
        }
    }

    void UpdateScoreText(){
        scoreText.text = "Score: " + score;
    }
    void UpdateSpeedText(){
        currentSpeedText.text = "" + speed;
    }
    void UpdateDamageText(){
        currentDamageText.text = "" + damage;
    }
    void UpdateRateText(){
        currentRateText.text = "" + rate;
    }

    public PlayerData GetPlayerData()
    {
        return  DataManager.LoadPlayerData();
    }
    public float GetPlayerSpeed()
    {
        return  DataManager.LoadPlayerData().playerSpeed;
    }

    public int GetScore()
    {
        return DataManager.LoadPlayerData().playerScore;
    }
    public int GetDamage(){
        return DataManager.LoadPlayerData().bulletDamage;
    }
    public float GetRate(){
        return DataManager.LoadPlayerData().bulletRate;
    }

    void UpdateDamage(int newDamage)
    {
        PlayerData data = DataManager.LoadPlayerData();
        data.bulletDamage = newDamage; // Update the score
        Debug.Log("New Damage for Bullet: " + data.bulletDamage);
        DataManager.SavePlayerData(data); // Save the updated data
    }

    void UpdateScore(int score)
    {
        PlayerData data = DataManager.LoadPlayerData();
        data.playerScore = score; // Update the score
        Debug.Log("New Score for Player: " + data.playerScore);
        DataManager.SavePlayerData(data); // Save the updated data
    }

    public void AddSpeed(){
        if(score != 0 && (score -10) >= 0){
            score -= 10;
            speed += 0.1f;
            speed = (float)Math.Round(speed, 1);
            UpdateScoreText();
            UpdateSpeedText();
        }
    }

    public void AddRate(){
        if(score != 0 && (score -10) >= 0){
            if(rate > 0.1){
                score -= 10;
                rate -= 0.1f;
                rate = (float)Math.Round(rate, 1);
                UpdateScoreText();
                UpdateRateText();}
            }
    }

    public void AddDamage(){
        if(score != 0 && (score -10) >= 0){
            score -= 10;
            damage += 1;
            UpdateScoreText();
            UpdateDamageText();
        }
    }

    public void LoadGamePlay()
    {
        SaveData();
        UpdateDamage(damage);
        //UpdateRate(rate);
        UpdateScore(score);
        //UpdateSpeed(speed);
        // Call the FadeOut function and then load the CharacterMenu scene.
        StartCoroutine(screenFader.Fade(ScreenFader.FadeDirection.In));
        // Wait for the fade to finish before loading the next scene.
        Invoke("LoadGamePlayScene", screenFader.fadeSpeed);  
    }
    void LoadGamePlayScene()
    {
        SceneManager.LoadScene("GamePlay");
    }

    // Part of the CharacterStatsController class
public void SaveData()
{
    ApplyEquippedEquipment();
    PlayerData data = new PlayerData
    {
        playerScore = this.score,
        playerSpeed = this.speed,
        bulletDamage = this.damage,
        bulletRate = this.rate,
        ownedItemIDs = GetOwnedEquipmentIDs(),
        equippedItemIDs = GetEquippedEquipmentIDs(),
    };

    DataManager.SavePlayerData(data);
}

private List<string> GetOwnedEquipmentIDs()
{
    List<string> ids = new List<string>();
    foreach (Equipment item in ownedEquipment)
    {
        ids.Add(item.EquipmentId); // Ensure your Equipment class or struct has an 'id' field
    }
    return ids;
}

private List<string> GetEquippedEquipmentIDs()
{
    List<string> ids = new List<string>();
    foreach (Equipment item in equippedEquipment)
    {
        ids.Add(item.EquipmentId); // Ensure your Equipment class or struct has an 'id' field
    }
    return ids;
}

// Part of the CharacterStatsController class
public void LoadData()
{
    PlayerData data = DataManager.LoadPlayerData();
    
    // Assuming you have methods or logic to apply these stats
    score = data.playerScore;
    speed = data.playerSpeed;
    damage = data.bulletDamage;
    rate = data.bulletRate;

    // Now, reconstruct the ownedEquipment list from IDs
    ownedEquipment.Clear();
    foreach (string id in data.ownedItemIDs)
    {
        Equipment equipment = FindEquipmentByID(id, availableEquipment);
        if (equipment != null)
        {
            ownedEquipment.Add(equipment);
        }
    }

    equippedEquipment.Clear();
    foreach (string id in data.equippedItemIDs)
    {
        Equipment equipment = FindEquipmentByID(id, ownedEquipment);
        if (equipment != null)
        {
            equippedEquipment.Add(equipment);
        }
    }

    // Apply the loaded data as needed
}

private Equipment FindEquipmentByID(string id, List<Equipment> equipmentList)
{
    foreach(Equipment equipment in equipmentList){
        if(equipment.EquipmentId == id){
            return equipment;
        }
    }
    return null;
}


}
