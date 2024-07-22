using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZoneUI : MonoBehaviour
{
    public List<GameObject> ingredients = new List<GameObject>();
    public MonsterController monsterController;

    private void Start()
    {
        if (monsterController == null)
        {
            monsterController = FindObjectOfType<MonsterController>();
            if (monsterController == null)
            {
                Debug.LogError("Monster Controller not found in the scene.");
            }
        }
    }

    public void AddIngredient(GameObject ingredient)
    {
        if (!ingredients.Contains(ingredient))
        {
            ingredients.Add(ingredient);
            ingredient.transform.SetParent(transform);
            Debug.Log("Ingredient added to drop zone: " + ingredient.name);
            monsterController.CreateMonster();
        }
        else
        {
            Debug.Log("Ingredient already in drop zone: " + ingredient.name);
        }
    }

    public void RemoveIngredient(GameObject ingredient)
    {
        if (ingredients.Contains(ingredient))
        {
            ingredients.Remove(ingredient);
            Debug.Log("Ingredient removed from drop zone: " + ingredient.name);
        }
    }
}
