using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EntityStats))]
public class SpawnDamageText : MonoBehaviour
{
    [SerializeField] private EntityStats _entityStats;
    [SerializeField] private GameObject _textPrefab;

    [SerializeField] private Color32 armorDamageColor = new Color(213f, 150f, 14f, 255f);
    [SerializeField] private Color32 healthDamageColor = new Color(184f, 77f, 8f, 255f);

    private void Awake()
    {
        if (_entityStats == null) GetComponent<EntityStats>();
    }

    private void OnEnable()
    {
        if (_entityStats == null) return;
        _entityStats.OnArmorDamageTaken += SpawnArmorDamageText;
        _entityStats.OnHealthDamageTaken += SpawnHealthDamageText;

    }

    private void SpawnArmorDamageText(float damage, bool didCrit)
    {
        InstantiateTextPrefab(damage, armorDamageColor, didCrit);
    }

    private void SpawnHealthDamageText(float damage, bool didCrit)
    {
        InstantiateTextPrefab(damage, healthDamageColor, didCrit);
    }

    private void InstantiateTextPrefab(float damage, Color32 damageColor, bool didCrit)
    {
        if(_entityStats == null) return;
        if(_textPrefab == null)
        {
            Debug.LogWarning("Enemy has no damage text prefab assigned!");
            return;
        }

        GameObject textGo = Instantiate(_textPrefab, transform.position, Quaternion.identity);
        textGo.transform.SetParent(null);
        TMP_Text tmpDamageText = textGo.GetComponent<TMP_Text>();

        
        tmpDamageText.text = damage.ToString();
        tmpDamageText.color = damageColor;
        if (didCrit) textGo.GetComponent<Animator>().SetBool("DidCrit", didCrit);
    }
}
