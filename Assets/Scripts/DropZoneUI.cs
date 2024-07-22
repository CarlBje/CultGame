using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZoneUI : MonoBehaviour
{
    public List<GameObject> ingredients = new List<GameObject>();

    public void AddIngredient(GameObject ingredient)
    {
        if (!ingredients.Contains(ingredient))
        {
            ingredients.Add(ingredient);
            ingredient.transform.SetParent(transform);
            Debug.Log("Ingredient added to drop zone: " + ingredient.name);
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
