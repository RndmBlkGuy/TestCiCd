using UnityEngine;


public abstract class Equipment : ScriptableObject
{
    [SerializeField]
    private string id;
    [SerializeField]
    private string name;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string description;
    [SerializeField]
    private int cost;
    [SerializeField]
    private int upgradeThreshold;

    public string EquipmentId{
        get{
            return id;
        }
    }
    public string EquipmentName{
        get{
            return name;
        }
    }
    public Sprite EquipmentIcon{
        get{
            return icon;
        }
    }

    public string EquipmentDescription{
        get{
            return description;
        }
    }
  public int EquipmentCost{
        get{
            return cost;
        }
    }    public int EquipmentThreshold{
        get{
            return upgradeThreshold;
        }
    }

    public abstract void AddEffect(PlayerData data);
    public abstract void RemoveEffect(PlayerData data);
}
