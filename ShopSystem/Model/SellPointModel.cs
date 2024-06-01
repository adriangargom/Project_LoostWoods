using System.Xml;

public class SellPointModel
{
    public SellPointStatusEnum Status;
    public PowerUpsEnum PowerUp { get; private set; }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public int Price { get; private set; }
    public int Quantity;

    
    public SellPointModel()
    {
        Status = SellPointStatusEnum.Disabled;
    }


    public void SetSellPointAttributes(PowerUpsEnum selectedPowerUp, XmlNode node)
    {
        PowerUp = selectedPowerUp;

        Title = node["Title"].InnerText;
        Description = node["Description"].InnerText;
        Price = int.Parse(node["Price"].InnerText);
        Quantity = int.Parse(node["Quantity"].InnerText);
    }

    public override string ToString()
    {
        return $"Status: {Status}, " +
            $"PowerUp: {PowerUp}, " +
            $"Title: {Title}, " +
            $"Description: {Description}, " +
            $"Price: {Price}, " +
            $"Quantity: {Quantity}";
    }
}