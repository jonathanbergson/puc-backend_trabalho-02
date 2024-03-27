using System;
using UnityEngine;

public enum PrefabType
{
    Barrels01,
    Barrels02,
    Fountain01,
    Ground01,
    House00,
    House01,
    House02,
    House03,
    House04,
    Rocks01,
    Rocks02,
    Rocks03,
    Table01,
    Table02,
    Table03,
    Tree01,
}

public class PrefabSettings : MonoBehaviour
{
    public PrefabType type;
}

[Serializable]
public class PrefabsData
{
    public Vector3 position;
    public PrefabType type;

    public PrefabsData(PrefabSettings prefabSettings)
    {
        position = prefabSettings.transform.position;
        type = prefabSettings.type;
    }
}

[Serializable]
public struct SceneData
{
    public PrefabsData[] prefabs;
}
