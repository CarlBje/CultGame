using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<MonsterRecipe> recipes;

    public GameObject GetMonster(Dictionary<DropZoneUI, List<GameObject>> zoneIngredients)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.Matches(zoneIngredients))
            {
                return recipe.monsterPrefab;
            }
        }
        return null;
    }
}

[System.Serializable]
public class MonsterRecipe
{
    public List<ZoneIngredients> zoneIngredients;
    public GameObject monsterPrefab;

    public bool Matches(Dictionary<DropZoneUI, List<GameObject>> zoneIngredients)
    {
        foreach (var zone in zoneIngredients)
        {
            var recipeZone = this.zoneIngredients.Find(zi => zi.zone == zone.Key);
            if (recipeZone == null || !recipeZone.Matches(zone.Value))
            {
                return false;
            }
        }
        return true;
    }
}

[System.Serializable]
public class ZoneIngredients
{
    public DropZoneUI zone;
    public List<string> ingredientNames;

    public bool Matches(List<GameObject> ingredients)
    {
        if (ingredients.Count != ingredientNames.Count) return false;

        foreach (var Ingredient in ingredients)
        {
            if (!ingredientNames.Contains(Ingredient.name))
            {
                return false;
            }
        }
        return true;
    }
}
