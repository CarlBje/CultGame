using UnityEngine;
using System.Collections;

public class MonsterAI : MonoBehaviour
{
    public int health = 100;
    public int attackPower = 10;
    public int defense = 5;
    public bool isFighting = false;
    public Attributes attributes;

    private void Start()
    {
        int randomAttribute1 = Random.Range(1, 4);
        int randomAttribute2 = Random.Range(1, 4);
        while (randomAttribute1 == randomAttribute2)
        {
            randomAttribute2 = Random.Range(1, 4);
        }
        attributes = (Attributes)(1 << randomAttribute1 | 1 << randomAttribute2);

    }



    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            StartCombat(other.gameObject);
        }
    }

    void StartCombat(GameObject enemyObject)
    {
        EnemyStatus enemy = enemyObject.GetComponent<EnemyStatus>();
        if (enemy != null && !isFighting)
        {
            isFighting = true;
            StartCoroutine(CombatCoroutine(enemy));
        }
    }

    IEnumerator CombatCoroutine(EnemyStatus enemy)
    {
        while (health > 0 && enemy.health > 0)
        {
            Attack(enemy);
            yield return new WaitForSeconds(1); // Wait for 1 second between attacks
            if (enemy.health > 0)
            {
                enemy.Attack(this);
                yield return new WaitForSeconds(1);
            }
        }
        isFighting = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Attack(EnemyStatus enemy)
    {
        int damage = CalculateDamage(enemy);
        enemy.TakeDamage(damage);
    }

    int CalculateDamage(EnemyStatus enemy)
    {
        int baseDamage = Mathf.Max(attackPower - enemy.defense, 0);
        int sharedAttributes = (int)(attributes & enemy.attributes);
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
