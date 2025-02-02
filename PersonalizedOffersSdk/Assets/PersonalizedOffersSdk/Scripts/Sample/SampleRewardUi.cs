using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SampleRewardUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rewardAmount;
    [SerializeField] private Image _rewardIcon;

    public void PopulateReward(Sprite rewardIcon ,string amountText)
    {
        rewardIcon = _rewardIcon.sprite;
        _rewardAmount.text = amountText;
    }
}
