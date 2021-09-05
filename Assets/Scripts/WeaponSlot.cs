using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    private GameObject weapon;

    [SerializeField] private List<GameObject> weaponPrefabs;

    private int currentWeaponIndex = 0;

    private void Start()
    {
        EquipWeapon();
    }

    private void EquipWeapon()
    {
        if (weaponPrefabs.Count > currentWeaponIndex)
            weapon = Instantiate(weaponPrefabs[currentWeaponIndex], transform);
        else
            Debug.LogWarning($"Could not equip weapon. Value of {nameof(currentWeaponIndex)} was out of bounds.)");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SwitchWeapon();
    }

    private void SwitchWeapon()
    {
        Destroy(weapon);

        if (++currentWeaponIndex >= weaponPrefabs.Count)
            currentWeaponIndex = 0;
        
        EquipWeapon();
    }
}
