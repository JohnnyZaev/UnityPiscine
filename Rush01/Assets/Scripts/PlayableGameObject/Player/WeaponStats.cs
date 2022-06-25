using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float damage;
    public float attackSpeed;
    public float cost;
    public int id;
    public int custom;
    public enum WeaponType
    {
        Sword,
        Axe,
        Knife
    }

    public WeaponType weaponType;
}
