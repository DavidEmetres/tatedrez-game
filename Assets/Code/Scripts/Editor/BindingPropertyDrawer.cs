using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Assertions;
using Unity.VisualScripting;

struct BindableInfo
{
	public UnityEngine.Object Obj;
	public string ViewModelType;
	public string PropertyName;
	
	public BindableInfo(UnityEngine.Object obj, string viewModelType, string propertyName)
	{
		Obj = obj;
		ViewModelType = viewModelType;
		PropertyName = propertyName;
	}
}

[CustomPropertyDrawer(typeof(BindingProperty<>))]
public class BindingPropertyDrawer : PropertyDrawer
{
	private int _selectedIndex = -1;
	
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		Type genericType = fieldInfo.FieldType.GetGenericArguments()[0];
		Type bindableType = typeof(BindableProperty<>);

		SerializedProperty bindedGameObjectProp = property.FindPropertyRelative("_bindedGameObject");
		SerializedProperty bindedViewModelTypeProp = property.FindPropertyRelative("_bindedViewModelType");
		SerializedProperty bindedPropertyNameProp = property.FindPropertyRelative("_bindedPropertyName");

		Assert.IsFalse(bindedGameObjectProp == null || bindedViewModelTypeProp == null || bindedPropertyNameProp == null);

		List<BindableInfo> candidates = new List<BindableInfo>();
		candidates.Add(new BindableInfo(null, string.Empty, string.Empty));
		List<string> candidatesStr = new List<string>();
		candidatesStr.Add("None");

		// search upstairs in hierarchy for all suitable bindables
		int index = 0;
		GameObject targetGameObject = ((MonoBehaviour)property.serializedObject.targetObject).gameObject;
		while (targetGameObject != null)
		{
			// bindables are only look for in ViewModels
			ViewModel[] viewModels = targetGameObject.GetComponents<ViewModel>();
			for (int i = 0; i < viewModels.Length; i++)
			{
				ViewModel viewModel = viewModels[i];
				Type viewModelType = viewModel.GetType();
				FieldInfo[] fields = viewModelType.GetFields();
				for (int j = 0; j < fields.Length; ++j)
				{
					FieldInfo field = fields[j];
					if (field.FieldType.Name == bindableType.Name && field.FieldType.GetGenericArguments().Length > 0 && field.FieldType.GetGenericArguments()[0] == genericType)
					{
						candidates.Add(new BindableInfo(targetGameObject, viewModelType.Name, field.Name));
						candidatesStr.Add($"{targetGameObject.name} / {viewModelType} / {field.Name}");
						index++;

						// find current index on first time
						if (_selectedIndex == -1 && bindedGameObjectProp.objectReferenceValue == targetGameObject && bindedViewModelTypeProp.stringValue == viewModelType.Name && bindedPropertyNameProp.stringValue == field.Name)
						{
							_selectedIndex = index;
						}
					}
				}
			}
			
			targetGameObject = targetGameObject.transform.parent?.gameObject;
		}

		_selectedIndex = EditorGUI.Popup(position, label.text, _selectedIndex, candidatesStr.ToArray());

		if (_selectedIndex < 0 || _selectedIndex >= candidates.Count)
		{
			_selectedIndex = 0;
		}

		BindableInfo selectedCandidate = candidates[_selectedIndex];
		
		bindedGameObjectProp.objectReferenceValue = selectedCandidate.Obj;
		bindedViewModelTypeProp.stringValue = selectedCandidate.ViewModelType;
		bindedPropertyNameProp.stringValue = selectedCandidate.PropertyName;

		property.serializedObject.ApplyModifiedProperties();
	}
}