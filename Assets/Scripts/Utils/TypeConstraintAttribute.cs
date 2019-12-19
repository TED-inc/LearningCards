using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using TEDinc.LearningCards;

namespace TEDinc.Utils
{ 
    public class TypeConstraintAttribute : PropertyAttribute
    {
        private Type type;

        public TypeConstraintAttribute(System.Type type)
        {
            this.type = type;
        }

        public Type Type
        {
            get { return type; }
        }
    }

    [CustomPropertyDrawer(typeof(TypeConstraintAttribute))]
    public class TypeConstraintDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            TypeConstraintAttribute target = attribute as TypeConstraintAttribute;

            if (property.objectReferenceValue != null)
                if (!IsSubclassOrHaveInterface(property.objectReferenceValue.GetType(), target.Type))
                {
                    MonoBehaviour monoBehaviour = property.objectReferenceValue as MonoBehaviour;
                    bool scriptHasBeenFind = false;
                    if (monoBehaviour != null)
                    {
                        GameObject gameObject = monoBehaviour.gameObject;

                        if (gameObject != null)
                            foreach (Component component in gameObject.GetComponents(typeof(MonoBehaviour)))
                                if (IsSubclassOrHaveInterface(component.GetType(), target.Type))
                                {
                                    property.objectReferenceValue = component;
                                    scriptHasBeenFind = true;
                                }
                    }

                    if (!scriptHasBeenFind)
                        property.objectReferenceValue = null;
                }

            if (DragAndDrop.objectReferences.Length > 0)
            {
                GameObject draggedObject = DragAndDrop.objectReferences[0] as GameObject;
        
                if (draggedObject == null || (draggedObject != null && draggedObject.GetComponent(target.Type) == null))
                    DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
            }
        
            if (property.objectReferenceValue != null)
            {
                GameObject gameObject = property.objectReferenceValue as GameObject;
                if (gameObject != null && gameObject.GetComponent(target.Type) == null)
                    property.objectReferenceValue = null;
            }
        
            property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(MonoBehaviour), true);
        }

        private bool IsSubclassOrHaveInterface(Type parentType,Type childType)
        {
            return parentType.GetInterface(childType.Name) != null
                    || parentType.IsSubclassOf(childType) == true;
        }
    }
}