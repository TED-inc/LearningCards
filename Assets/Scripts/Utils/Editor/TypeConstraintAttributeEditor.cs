using UnityEngine;
using UnityEditor;

namespace TEDinc.Utils.Editor
{
    [CanEditMultipleObjects]
    [CustomPropertyDrawer(typeof(TypeConstraintAttribute))]
    public class TypeConstraintAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            TypeConstraintAttribute target = attribute as TypeConstraintAttribute;

            CheckForCorrectInputetObject(property, target);
            if (position.Contains(Event.current.mousePosition))
                CheckForCorrectDraggableObject(target);

            property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(MonoBehaviour), true);
        }



        private void CheckForCorrectDraggableObject(TypeConstraintAttribute target)
        {
            if (DragAndDrop.objectReferences.Length > 0)
            {
                UnityEngine.Object draggedObject = DragAndDrop.objectReferences[0];

                if (draggedObject == null)
                    DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                else if (!SystemUtils.IsSubclassOrHaveInterface(draggedObject.GetType(), target.type))
                {
                    if (SystemUtils.GetComponentWhichSubclassOrHaveInterface(draggedObject as GameObject, target.type) == null)
                        DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                }
            }
        }

        private void CheckForCorrectInputetObject(SerializedProperty property, TypeConstraintAttribute target)
        {
            if (property.objectReferenceValue != null)
                if (!SystemUtils.IsSubclassOrHaveInterface(property.objectReferenceValue.GetType(), target.type))
                {
                    MonoBehaviour monoBehaviour = property.objectReferenceValue as MonoBehaviour;

                    if (monoBehaviour != null)
                        property.objectReferenceValue = SystemUtils.GetComponentWhichSubclassOrHaveInterface(monoBehaviour.gameObject, target.type);
                }
        }
    }
}