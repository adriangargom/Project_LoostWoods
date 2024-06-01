using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

public abstract class BaseStatsManager : BaseObservable
{
    public readonly Dictionary<StatsEnum, float> BaseStats = new();
    public readonly Dictionary<StatsEnum, float> ActualStats = new();
    protected Dictionary<Type, BasePowerUp> ActivePowerUps = new();

    protected readonly string BaseStatsResourceName;
    protected readonly string BaseStatsResourceTag;
    protected const string BaseStatsCodeResourceTag = "Code";
    protected const string BaseStatsValueResourceTag = "BaseValue";



    protected BaseStatsManager(string baseStatsResourceName, string baseStatsResourceTag)
    {
        BaseStatsResourceName = baseStatsResourceName;
        BaseStatsResourceTag = baseStatsResourceTag;

        LoadBaseStats();
    }



    // Reads all the nodes that contains the base stats attributes from the entity BaseStats
    // resource file and sets the attributes into the BaseStatsManager dictionaries
    private void LoadBaseStats()
    {
        XmlNodeList nodeList = XmlLoader.LoadXmlResource(BaseStatsResourceName, BaseStatsResourceTag);

        foreach (XmlNode node in nodeList)
        {
            SetStatAttribute(node);
        }
    }

    // Sets the attribute based on the provided XmlNode, if the attributes exist and
    // the format of those attributes is correct 
    private void SetStatAttribute(XmlNode node)
    {
        if(node[BaseStatsCodeResourceTag] == null) return;
        if(node[BaseStatsValueResourceTag] == null) return;
        
        string code = node[BaseStatsCodeResourceTag].InnerText;
        string baseValue = node[BaseStatsValueResourceTag].InnerText;

        if(Enum.TryParse(typeof(StatsEnum), code, out var statEnumCode))
        {
            if(int.TryParse(baseValue, out int value))
            {
                BaseStats.Add((StatsEnum) statEnumCode, value);
                ActualStats.Add((StatsEnum) statEnumCode, value);
            }
        }
    }


    
    // Sets all the powerUps of the stats manager
    public void SetPowerUps(List<BasePowerUp> powerUps)
    {
        foreach (BasePowerUp powerUp in powerUps)
        {
            ActivePowerUps.Add(powerUp.GetType(), powerUp);
        }
    }

    // Upgrades a active powerUp from the "_activePowerUps" list based on the provided 
    // generic type "T", if the powerUp exists inside of the dictionary
    public void UpgradePowerUp<T>() where T: BasePowerUp
    {
        if(!ActivePowerUps.Keys.Contains(typeof(T)))
            throw new PowerUpNotFoundException("The power up dosn't exists inside of the activePowerUps");

        ActivePowerUps[typeof(T)].UpdatePowerUp();
    }

    public void UpgradeAllPowerUps()
    {
        foreach (Type powerUp in ActivePowerUps.Keys)
        {
            ActivePowerUps[powerUp].UpdatePowerUp();
        }
    }
}