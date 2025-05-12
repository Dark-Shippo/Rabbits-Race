using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/SpawnManagerScriptable", order = 1)]
public class SpawnManagerScriptable : ScriptableObject
{
    public string prefabName;

    public int numberOfPrefabsToCreate;
    public Vector3[] spawnPoints;
}
