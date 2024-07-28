using UnityEditor;
using UnityEngine;

[System.Flags]
public enum Attributes
{
    None = 0,
    A = 1 << 0,  // 1
    B = 1 << 1,  // 2
    C = 1 << 2   // 4
}

public class EnemyStatus : MonoBehaviour
{
    public int health = 100;
    public Attributes attributes;
    public int attackPower = 10;
    public int defense = 5;

    private void Start()
    {
        //pick 2 random but different attributes
        int randomAttribute1 = Random.Range(1, 4);
        int randomAttribute2 = Random.Range(1, 4);
        while (randomAttribute1 == randomAttribute2)
        {
            randomAttribute2 = Random.Range(1, 4);
        }
        attributes = (Attributes)(1 << randomAttribute1 | 1 << randomAttribute2);
        
    }

    

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Attack(MonsterAI target)
    {
        int damage = CalculateDamage(target);
        target.TakeDamage(damage);
    }

    int CalculateDamage(MonsterAI target)
    {
        int baseDamage = Mathf.Max(attackPower - target.defense, 0);
        int sharedAttributes = (int)(attributes & target.attributes);
        int sharedCount = CountBits(sharedAttributes);

        if (sharedCount == 1)
        {
            baseDamage = Mathf.FloorToInt(baseDamage * 1.5f); // 50% more damage
        }
        else if (sharedCount == 2)
        {
            baseDamage *= 2; // 100% more damage
        }
        else if (sharedCount == 0)
        {
            baseDamage = Mathf.FloorToInt(baseDamage * 0.5f); // 50% less damage
        }

        return baseDamage;
    }

    int CountBits(int value)
    {
        int count = 0;
        while (value != 0)
        {
            count += value & 1;
            value >>= 1;
        }
        return count;
    }

    
}
