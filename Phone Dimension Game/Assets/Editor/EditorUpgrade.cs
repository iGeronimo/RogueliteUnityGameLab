using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;



[CustomEditor(typeof(UpgradeManager))]
public class EditorUpgrade : Editor
{
    // This will be the serialized "copy" of YourOtherClass.YourList
    private SerializedProperty UpgradeList;


    private ReorderableList YourReorderableList;

    private void OnEnable()
    {
        // Step 1 "link" the SerializedProperties to the properties of YourOtherClass
        UpgradeList = serializedObject.FindProperty("upgrades");

        // Step 2 setup the ReorderableList
        YourReorderableList = new ReorderableList(serializedObject, UpgradeList)
        {
            // Can your objects be dragged an their positions changed within the List?
            draggable = true,

            // Can you add elements by pressing the "+" button?
            displayAdd = true,

            // Can you remove Elements by pressing the "-" button?
            displayRemove = true,

            // Make a header for the list
            drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "UPGRADES");
            },

            // Now to the interesting part: Here you setup how elements look like
            drawElementCallback = (rect, index, active, focused) =>
            {
                // Get the currently to be drawn element from YourList
                var element = UpgradeList.GetArrayElementAtIndex(index);

                // Get the elements Properties into SerializedProperties
                var Enum = element.FindPropertyRelative("upgradeType");
                var Attack = element.FindPropertyRelative("Attack");
                var Defense = element.FindPropertyRelative("Defense");
                var Crit = element.FindPropertyRelative("Crit");

                // Draw the Enum field
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), Enum);
                // start the next property in the next line
                rect.y += EditorGUIUtility.singleLineHeight;

                // only show Name field if selected "first"
                if ((Upgrade.UpgradeType)Enum.intValue == Upgrade.UpgradeType.ATTACK)
                {
                    EditorGUILayout.ObjectField(Attack, typeof(Attack), new GUIContent("Attack"));
                    //EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), Attack);
                    // start the next property in the next line
                    rect.y += EditorGUIUtility.singleLineHeight;
                }

                // Draw the Step field
                //EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), Defense);
                // start the next property in the next line
                rect.y += EditorGUIUtility.singleLineHeight;

                // only show Step field if selected "seconds"
                if ((Upgrade.UpgradeType)Enum.intValue == Upgrade.UpgradeType.SHIELD)
                {
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), Defense);
                }

                // only show Step field if selected "seconds"
                if ((Upgrade.UpgradeType)Enum.intValue == Upgrade.UpgradeType.STAT)
                {
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), Crit);
                }
            },

            // And since we have more than one line (default) you'll have to configure 
            // how tall your elements are. Luckyly in your example it will always be exactly
            // 3 Lines in each case. If not you would have to change this.
            // In some cases it becomes also more readable if you use one more Line as spacer between the elements
            elementHeight = EditorGUIUtility.singleLineHeight * 3,

            //alternatively if you have different heights you would use e.g.
            //elementHeightCallback = index =>
            //{
            //    var element = YourList.GetArrayElementAtIndex(index);
            //    var Enum = element.FindPropertyRelative("Enum");

            //    switch ((YourClass.YourEnum)Enum.intValue)
            //    {
            //        case YourClass.YourEnum.first:
            //            return EditorGUIUtility.singleLineHeight * 3;

            //        case YourClass.YourEnum.second:
            //            return EditorGUIUtility.singleLineHeight * 5;

            //            default:
            //                return EditorGUIUtility.singleLineHeight;
            //    }
            //}

            // optional: Set default Values when adding a new element
            // (otherwise the values of the last list item will be copied)
            onAddCallback = list =>
            {
                // The new index will be the current List size ()before adding
                var index = list.serializedProperty.arraySize;

                // Since this method overwrites the usual adding, we have to do it manually:
                // Simply counting up the array size will automatically add an element
                list.serializedProperty.arraySize++;
                list.index = index;
                var element = list.serializedProperty.GetArrayElementAtIndex(index);

                // again link the properties of the element in SerializedProperties
                var Enum = element.FindPropertyRelative("Enum");
                var Attack = element.FindPropertyRelative("Attack");
                var Defense = element.FindPropertyRelative("Defense");
                var Crit = element.FindPropertyRelative("Crit");

                // and set default values
                Enum.intValue = (int)Upgrade.UpgradeType.ATTACK;
                Attack.objectReferenceValue = null;
                Defense.intValue = 0;
                Crit.floatValue = 0.1f;
            }
        };
    }

    public override void OnInspectorGUI()
    {
        // copy the values of the real Class to the linked SerializedProperties
        serializedObject.Update();

        // print the reorderable list
        YourReorderableList.DoLayoutList();

        // apply the changed SerializedProperties values to the real class
        serializedObject.ApplyModifiedProperties();
    }
}












/*
public SerializedProperty
        upgrade_Prop,
        Attack,
        valForShield,
        valForStat,
        controllable_Prop;


    void OnEnable()
    {
        // Setup the SerializedProperties
        upgrade_Prop = serializedObject.FindProperty("upgradeType");
        Attack = serializedObject.FindProperty("Attack");
        valForShield = serializedObject.FindProperty("defense");
        valForStat = serializedObject.FindProperty("crit");
        controllable_Prop = serializedObject.FindProperty("controllable");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(upgrade_Prop);

        Upgrade.UpgradeType st = (Upgrade.UpgradeType)upgrade_Prop.enumValueIndex;

        switch (st)
        {
            case Upgrade.UpgradeType.ATTACK:
                EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                EditorGUILayout.IntSlider(valForShield, 0, 10, new GUIContent("Defense"));
                EditorGUILayout.ObjectField(Attack, typeof(Attack), new GUIContent("Attack"));
                break;

            case Upgrade.UpgradeType.SHIELD:
                EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));

                break;

            case Upgrade.UpgradeType.STAT:
                EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                EditorGUILayout.IntSlider(valForStat, 0, 100, new GUIContent("Crit Chance"));
                break;

        }

        if(Attack != null)
        {

        }

        serializedObject.ApplyModifiedProperties();
    }


}*/
