using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScrollTabSO : MonoBehaviour
{
    [Header("Paths")]
    public string presetsFolderPath = "Assets/Presets";

    [Header("UI References")]
    public RectTransform contentPanel;
    public ContainerPrefab containerPrefab;
    public FieldPrefab fieldPrefab;
    public TitlePrefab titlePrefab;
    public SavePresetBtnPrefab saveButtonPrefab;
    public LoadPresetBtnPrefab loadButtonPrefab;

    [Header("ScriptableObjects")]
    public List<ScriptableObject> scriptableObjects = new List<ScriptableObject>();

    private Dictionary<ScriptableObject, Dictionary<string, TMP_InputField>> fieldReferences = new Dictionary<ScriptableObject, Dictionary<string, TMP_InputField>>();

    private void Start()
    {
        if (!Directory.Exists(presetsFolderPath))
        {
            Directory.CreateDirectory(presetsFolderPath);
        }

        PopulateUI();
    }

    private void PopulateUI()
    {
        foreach (var so in scriptableObjects)
        {
            ContainerPrefab soContainer = Instantiate(containerPrefab, contentPanel);

            TitlePrefab title = Instantiate(titlePrefab, soContainer.transform);
            title.title.text = so.name;

            PopulateFields(so, soContainer.transform);

            SavePresetBtnPrefab saveButton = Instantiate(saveButtonPrefab, soContainer.transform);

            ContainerPrefab presetsContainer = Instantiate(containerPrefab, soContainer.transform);

            saveButton.button.onClick.AddListener(() => SavePreset(so, saveButton, presetsContainer.transform));

            PopulatePresetList(so, presetsContainer.transform);
        }
    }

    private void PopulateFields(ScriptableObject so, Transform soContainer)
    {
        Dictionary<string, TMP_InputField> fields = new Dictionary<string, TMP_InputField>();

        foreach (var field in so.GetType().GetFields())
        {
            if (field.FieldType == typeof(float) || field.FieldType == typeof(int))
            {
                FieldPrefab fieldObject = Instantiate(fieldPrefab, soContainer);
                fieldObject.title.text = field.Name;
                fieldObject.inputField.text = field.GetValue(so).ToString();

                fieldObject.inputField.onEndEdit.AddListener((value) =>
                {
                    if (float.TryParse(value, out float floatValue) && field.FieldType == typeof(float))
                    {
                        field.SetValue(so, floatValue);
                    }
                    else if (int.TryParse(value, out int intValue) && field.FieldType == typeof(int))
                    {
                        field.SetValue(so, intValue);
                    }
                });

                fields.Add(field.Name, fieldObject.inputField);
            }
        }

        fieldReferences.Add(so, fields);
    }

    private void PopulatePresetList(ScriptableObject so, Transform presetsContainer)
    {
        foreach (Transform child in presetsContainer)
        {
            Destroy(child.gameObject);
        }

        if (!Directory.Exists(presetsFolderPath))
        {
            Directory.CreateDirectory(presetsFolderPath);
        }

        string[] presetFiles = Directory.GetFiles(presetsFolderPath, "*.json");

        foreach (var presetFile in presetFiles)
        {
            string presetName = Path.GetFileNameWithoutExtension(presetFile);

            if (presetName.StartsWith(so.name))
            {
                LoadPresetBtnPrefab presetButton = Instantiate(loadButtonPrefab, presetsContainer);
                presetButton.title.text = presetName;

                presetButton.load.onClick.AddListener(() =>
                {
                    LoadPreset(presetFile, so);
                });

                presetButton.delete.onClick.AddListener(() =>
                {
                    DeletePreset(presetFile, presetButton);
                });
            }
        }
    }

    private void SavePreset(ScriptableObject so, SavePresetBtnPrefab saveButton, Transform transformPreset)
    {
        if (!Directory.Exists(presetsFolderPath))
        {
            Directory.CreateDirectory(presetsFolderPath);
        }

        string presetName = $"{so.name}_{saveButton.inputField.text}_Preset.json";
        string filePath = Path.Combine(presetsFolderPath, presetName);

        string json = JsonUtility.ToJson(so, true);
        File.WriteAllText(filePath, json);

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif

        PopulatePresetList(so, transformPreset);

        Debug.Log($"Preset saved to: {filePath}");
    }

    private void LoadPreset(string filePath, ScriptableObject so)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError($"Preset file not found: {filePath}");
            return;
        }

        string json = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(json, so);

        if (fieldReferences.ContainsKey(so))
        {
            foreach (var field in so.GetType().GetFields())
            {
                if (fieldReferences[so].ContainsKey(field.Name))
                {
                    var inputField = fieldReferences[so][field.Name];
                    inputField.text = field.GetValue(so).ToString();
                }
            }
        }
    }

    private void DeletePreset(string filePath, LoadPresetBtnPrefab presetButton)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            Destroy(presetButton.gameObject);
        }
        else
        {
            Debug.LogError($"Preset file not found for deletion: {filePath}");
        }
    }
}
