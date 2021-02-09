using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

//[CustomPropertyDrawer(typeof(IdlerObjectUpgrade))]
public class IdlerObjectUpgradeUIE : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        var upgradeType = new PropertyField(property.FindPropertyRelative("type"));
        //if(upgradeType!=IdlerUpgradeType.Clicker)
        
        return base.CreatePropertyGUI(property);
    }


}
