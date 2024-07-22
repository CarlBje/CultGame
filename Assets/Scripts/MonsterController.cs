using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public DropZoneUI dropZone1;
    public DropZoneUI dropZone2;
    public MonsterManager monsterManager;

    public void CreateMonster()
    {
        var zoneIngredients = new Dictionary<DropZoneUI, List<GameObject>>
        {
            { dropZone1, new List<GameObject>(dropZone1.ingredients)},
            { dropZone2, new List<GameObject>(dropZone2.ingredients)},
        };

        GameObject monsterPrefab = monsterManager.GetMonster(zoneIngredients);
        if (monsterPrefab != null)
        {
            Instantiate(monsterPrefab, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}
