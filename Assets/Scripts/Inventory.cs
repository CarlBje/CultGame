using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Ingredient> ingredients = new List<Ingredient>();

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
        Debug.Log("Inventory.Added ingredient: " + ingredient.ingredientName);
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        if (ingredients.Contains(ingredient))
        {
            ingredients.Remove(ingredient);
            Debug.Log("Inventory.Removed ingredient: " + ingredient.ingredientName);
        }
        else
        {
            Debug.Log("Inventory.Ingredient not found in Inventory: " + ingredient.ingredientName);
        }
    }
}
