

using TMPro;
using UnityEngine;

public class UIShopPowerUpManager : MonoBehaviour, IObserver
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private StoreSellPoint _storeSellPoint;
    [SerializeField] private GameObject _sellPointInfoFrame;
    [SerializeField] private TextMeshProUGUI _titleLabel, _descriptionLabel, _priceLabel, _quantityLabel;


    void Start() 
    {
        _playerController = FindObjectOfType<PlayerController>();
        _storeSellPoint = GetComponentInParent<StoreSellPoint>();
        _storeSellPoint.Attach(this);

        LeanTween.scale(gameObject, Vector3.zero, .5f);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    
    public void ObserverUpdate()
    {
        bool isSellPointActive = _storeSellPoint.SellPointData.Status == SellPointStatusEnum.Available;
        _sellPointInfoFrame.SetActive(isSellPointActive);
    
        if(isSellPointActive)
            LeanTween.scale(gameObject, Vector3.one, .5f);
        else
            LeanTween.scale(gameObject, Vector3.zero, .5f);

        CustomizeSellPoint();
    }

    public void CustomizeSellPoint()
    {
        SellPointModel sellPointData = _storeSellPoint.SellPointData;

        _titleLabel.text = sellPointData.Title;
        _descriptionLabel.text = sellPointData.Description;

        PlayerStatsManager playerStatsManager = _playerController.BaseStatsManager as PlayerStatsManager;
        bool sufficientCredits = playerStatsManager.ActualStats[StatsEnum.Credits] >= sellPointData.Price;
        _priceLabel.text = string.Format("Price: {0}", sellPointData.Price);
        _priceLabel.color = sufficientCredits ? Color.green : Color.red;

        _quantityLabel.text = string.Format("Quant: {0}", sellPointData.Quantity);
        _quantityLabel.color = sellPointData.Quantity > 0 ? Color.green : Color.red;
    }
}