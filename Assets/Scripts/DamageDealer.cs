using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] int damagePlayer = 1;
    public int GetDamage()
    {
        return damage;
    }
    public int GetDamagePlayer()
    {
        return damagePlayer;
    }
    public void Hit()
    {
        Destroy(gameObject);
    }
}
