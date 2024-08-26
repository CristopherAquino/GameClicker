using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ApplyLayerToChildren : MonoBehaviour
{
    // The layer index to apply to all children
    [HideInInspector]
    public int layerIndex;

    void Start()
    {
        // Set the layer of the parent GameObject
        gameObject.layer = layerIndex;

        // Apply the layer to all children recursively
        ApplyLayerToAllChildren();
    }

    // Apply the layer to all children recursively
    public void ApplyLayerToAllChildren()
    {
        foreach (Transform child in transform)
        {
            // Set the layer of the current child
            child.gameObject.layer = layerIndex;

            // Recursively apply the layer to all children of the current child
            child.GetComponent<ApplyLayerToChildren>()?.ApplyLayerToAllChildren();
        }
    }
}

[CustomEditor(typeof(ApplyLayerToChildren))]
public class ApplyLayerToChildrenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ApplyLayerToChildren myScript = (ApplyLayerToChildren)target;

        // Display a dropdown menu to select the layer
        myScript.layerIndex = EditorGUILayout.LayerField("Layer", myScript.layerIndex);

        if (GUILayout.Button("Apply Layer to Children"))
        {
            // Apply the layer to all children
            myScript.ApplyLayerToAllChildren();
        }
    }
}
