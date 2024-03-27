using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class SaveController : MonoBehaviour
{
    private const string FileName = "sceneData.json";
    private const string LocalPath = "Assets/Data/";
    private const string ServerPath  = "https://raw.githubusercontent.com/jonathanbergson/puc-backend_trabalho-02/main/Assets/Data/";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(LoadFromServer());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Clear();
        }
    }

    private static void Save()
    {
        var prefabs = FindObjectsOfType<PrefabSettings>();
        var sceneData = new SceneData {
            prefabs = prefabs.Select(item => new PrefabsData(item)).ToArray()
        };

        var json = JsonUtility.ToJson(sceneData);
        File.WriteAllText(LocalPath + FileName, json);

        Debug.Log("Scene saved");
    }

    private static void Load()
    {
        var json = File.ReadAllText( LocalPath + FileName);
        var sceneData = JsonUtility.FromJson<SceneData>(json);

        Clear();
        InstantiateScene(sceneData);

        Debug.Log("Scene loaded");
    }

    private static IEnumerator LoadFromServer()
    {
        var request = UnityWebRequest.Get(ServerPath + FileName);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var json = request.downloadHandler.text;
            var sceneData = JsonUtility.FromJson<SceneData>(json);

            Clear();
            InstantiateScene(sceneData);

            Debug.Log("Scene loaded from server");
        }
        else
        {
            Debug.Log(request.error);
        }
    }

    private static void InstantiateScene(SceneData sceneData)
    {
        foreach (var prefab in sceneData.prefabs)
        {
            switch (prefab.type)
            {
                case PrefabType.Barrels01:
                    Instantiate(Resources.Load("Barrels01"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.Barrels02:
                    Instantiate(Resources.Load("Barrels02"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.Fountain01:
                    Instantiate(Resources.Load("Fountain01"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.House00:
                    Instantiate(Resources.Load("House00"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.House01:
                    Instantiate(Resources.Load("House01"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.House02:
                    Instantiate(Resources.Load("House02"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.House03:
                    Instantiate(Resources.Load("House03"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.House04:
                    Instantiate(Resources.Load("House04"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.Rocks01:
                    Instantiate(Resources.Load("Rocks01"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.Rocks02:
                    Instantiate(Resources.Load("Rocks02"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.Rocks03:
                    Instantiate(Resources.Load("Rocks03"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.Table01:
                    Instantiate(Resources.Load("Table01"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.Table02:
                    Instantiate(Resources.Load("Table02"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.Table03:
                    Instantiate(Resources.Load("Table03"), prefab.position, Quaternion.identity);
                    break;
                case PrefabType.Tree01:
                    Instantiate(Resources.Load("Tree01"), prefab.position, Quaternion.identity);
                    break;
                default:
                    Debug.Log("Unknown type: " + prefab.type);
                    break;
            }
        }
    }

    private static void Clear()
    {
        var prefabs = FindObjectsOfType<PrefabSettings>();
        foreach (var prefab in prefabs)
        {
            Destroy(prefab.gameObject);
        }

        Debug.Log("Scene cleared");
    }
}
