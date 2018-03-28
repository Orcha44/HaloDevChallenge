using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShapeProperties))]
public class ShapePropertiesEditor : Editor
{
    #region Public Fields

    // All properties are serialized for easier unity implementation
    public SerializedProperty
        shapeType,
        columns,
        margin,
        marginColumn,
        marginRow,
        plane,
        axis,
        radius,
        reverse,
        angle,
        angleValue,
        align;
    #endregion

    #region Private Fields

    private float currMargin; // so we can check if the margin is changed or only row/column margin
    private Shape currShape;
    private ShapeProperties shapeProperties;

    #endregion

    private void OnEnable()
    {
        // Setup all SerializedProperties with their correspondent value

        shapeType = serializedObject.FindProperty("shapeType");
        columns = serializedObject.FindProperty("columns");
        margin = serializedObject.FindProperty("margin");
        marginColumn = serializedObject.FindProperty("marginColumn");
        marginRow = serializedObject.FindProperty("marginRow");
        plane = serializedObject.FindProperty("plane");
        axis = serializedObject.FindProperty("axis");
        radius = serializedObject.FindProperty("radius");
        reverse = serializedObject.FindProperty("reverse");
        angle = serializedObject.FindProperty("angle");
        angleValue = serializedObject.FindProperty("angleValue");
        align = serializedObject.FindProperty("align");

        currMargin = margin.floatValue;        
    }

    public override void OnInspectorGUI()
    {
        shapeProperties = (ShapeProperties)target;

        serializedObject.Update();

        /* 
         * We check if the margin value was changed. if it is, the row & column 
         * values are defaulted to its value.
         */
        if (margin.floatValue != currMargin)
        {
            currMargin = margin.floatValue;
            marginColumn.floatValue = margin.floatValue;
            marginRow.floatValue = margin.floatValue;
        }

        EditorGUILayout.PropertyField(shapeType); // The shape field applies to all shapes so we show it anyway

        currShape = (Shape)shapeType.intValue;

        // Show the correspondent properties depending on the current chosen shape
        switch (currShape)
        {
            case Shape.Line:                
                EditorGUILayout.PropertyField(margin, new GUIContent("Margin"));
                EditorGUILayout.PropertyField(axis, new GUIContent("Axis"));
                EditorGUILayout.PropertyField(reverse, new GUIContent("Reverse"));
                break;
            case Shape.Box:
                EditorGUILayout.PropertyField(columns, new GUIContent("Columns"));
                EditorGUILayout.PropertyField(margin, new GUIContent("Margin"));
                EditorGUILayout.PropertyField(marginColumn, new GUIContent("Margin Column"));
                EditorGUILayout.PropertyField(marginRow, new GUIContent("Margin Row"));
                EditorGUILayout.PropertyField(plane, new GUIContent("Plane"));
                EditorGUILayout.PropertyField(align, new GUIContent("Align"));
                EditorGUILayout.PropertyField(reverse, new GUIContent("Reverse"));
                break;
            case Shape.Circle:
                EditorGUILayout.PropertyField(plane, new GUIContent("Plane"));
                EditorGUILayout.PropertyField(radius, new GUIContent("Radius"));
                EditorGUILayout.PropertyField(angle, new GUIContent("Angle"));
                if(angle.boolValue)
                    EditorGUILayout.PropertyField(angleValue, new GUIContent("Angle Value"));
                break;
        }


        EditorGUILayout.Space();

        // A simple button that activats the createWithProperties function
        if (GUILayout.Button("Initialize"))
        {
            shapeProperties.createWithProperties();
        }

        serializedObject.ApplyModifiedProperties();
    }
}