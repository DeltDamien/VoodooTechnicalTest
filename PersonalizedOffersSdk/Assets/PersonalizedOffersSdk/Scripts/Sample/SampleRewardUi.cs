using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SampleRewardUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rewardAmount;
    [SerializeField] private Image _rewardIcon;

    public void PopulateReward(Sprite rewardIcon ,string amountText)
    {
        _rewardIcon.sprite = rewardIcon;
        _rewardAmount.text = amountText;
    }
}
