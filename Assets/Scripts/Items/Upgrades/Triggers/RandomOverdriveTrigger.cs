using UnityEngine;

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Random", order = 100)]
public class RandomOverdriveTrigger : OverdriveTrigger
{
    [SerializeField] private float _cooldownTime;
    private float _currentCooldownTime;

    [SerializeField, Range(0, 100)] private float _triggerChance;
    

    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        bool isValid;

        if(_currentCooldownTime < 0)
        {
            if ((_triggerChance / 100f) > Random.Range(0f, 1f)) isValid = true;
            else isValid = false;
            _currentCooldownTime = _cooldownTime;
            
        } else 
        {
            _currentCooldownTime -= Time.deltaTime;
            isValid = false;
        }

        if (intervetd) return !isValid;
        else return isValid;
    }
}
