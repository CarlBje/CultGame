using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public DropZoneUI dropZone1;
    public DropZoneUI dropZone2;
    public Transform summonPosition;
    public MonsterManager monsterManager;
    public MonsterControllerAudio audioController;

    public void CreateMonster()
    {
        Debug.Log("Monster Controller is attempting to create a monster.");

        var zoneIngredients = new Dictionary<DropZoneUI, List<GameObject>>
        {
            { dropZone1, new List<GameObject>(dropZone1.ingredients)},
            { dropZone2, new List<GameObject>(dropZone2.ingredients)},
        };

        GameObject monsterPrefab = monsterManager.GetMonster(zoneIngredients);
        if (monsterPrefab != null)
        {
            Debug.Log("Monster prefab found: " + monsterPrefab.name);
            audioController.PlaySuccessSound();
            Vector3 correctPosition = new Vector3(summonPosition.position.x, summonPosition.position.y, 0.0f);
            Instantiate(monsterPrefab, correctPosition, Quaternion.identity);
            ConsumeIngredients();
        }
        else
        {
            Debug.Log("No matching monster found.");
        }
    }

    public void ConsumeIngredients()
    {
        dropZone1.ClearIngredients();
        dropZone2.ClearIngredients();
    }
}
