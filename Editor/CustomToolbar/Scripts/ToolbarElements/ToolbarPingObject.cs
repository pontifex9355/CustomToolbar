#nullable enable
using System;
using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;
using Object = UnityEngine.Object;

[Serializable]
internal class ToolbarPingObject : BaseToolbarElement
{
	private static GUIContent? pingObjectBtn;

	[SerializeField]
	private string objectPath;
	
	public override string NameInList => "[Button] Ping Object";

	public ToolbarPingObject(string objectPath = "Assets")
	{
		this.objectPath = objectPath;
	}

	public override void Init()
	{
		pingObjectBtn = EditorGUIUtility.IconContent("FolderOpened Icon");
		pingObjectBtn.tooltip = "Ping";
	}

	protected override void OnDrawInList(Rect position)
	{
		position.width = 70f;
		EditorGUI.LabelField(position, "Folder Path");

		position.x += position.width + FieldSizeSpace;
		position.width = 150;
		objectPath = EditorGUI.TextField(position, objectPath);
	}

	protected override void OnDrawInToolbar()
	{
		if (GUILayout.Button(pingObjectBtn, ToolbarStyles.commandButtonStyle))
		{
			var obj = AssetDatabase.LoadAssetAtPath<Object>(objectPath);

			if (obj != null)
				EditorGUIUtility.PingObject(obj);
			else
				Debug.LogError("Object not found: " + objectPath);
		}
	}
}