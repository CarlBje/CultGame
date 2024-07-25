using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public List<GameObject> ingredientUIPrefabs;
    public Transform ingredientsParent;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Clear existing UI elements
        foreach (Transform child in ingredientsParent)
        {
            Destroy(child.gameObject);
        }

        // Create new UI elements
        foreach (var ingredient in inventory.ingredients)
        {
            GameObject UIElement = Instantiate(GetPrefabForIngredient(ingredient), ingredientsParent);
            UIElement.transform.Find("IngredientSprite").GetComponent<Image>().sprite = ingredient.ingredientSprite;
            UIElement.transform.Find("IngredientName").GetComponent<Text>().text = ingredient.ingredientName;
        }
    }

    private GameObject GetPrefabForIngredient(Ingredient ingredient)
    {
        foreach (var prefab in ingredientUIPrefabs)
        {
            if (prefab.name == ingredient.ingredientName)
            {
                return prefab;
            }
        }
        return ingredientUIPrefabs[0];
    }
}