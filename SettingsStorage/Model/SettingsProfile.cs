using System;

[Serializable]
public class SettingsProfile
{
    public int Volume = 1;
    public bool IsMuted = false;
    public int QualityValue = 2;
    public int FpsQuantityValue = 1;

    [NonSerialized]
    public int RoundsQuantityValue = 2;

    public bool ValidateData()
    {
        if(Volume < 0 || Volume > 100)
            return false;

        if(QualityValue < 0 || QualityValue > 2)
            return  false;

        if(FpsQuantityValue < 0 || FpsQuantityValue > 3)
            return false;

        return true;
    }
}