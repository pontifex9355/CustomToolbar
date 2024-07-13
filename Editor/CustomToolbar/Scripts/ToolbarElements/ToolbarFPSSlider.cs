using System;
using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

[Serializable]
internal class ToolbarFPSSlider : BaseToolbarElement
{
	private static GUIContent resetBtn;

	[SerializeField]
	private int minFPS = 1;

	[SerializeField]
	private int maxFPS = 120;

	private int selectedFramerate;

	public override string NameInList => "[Slider] FPS";

	public ToolbarFPSSlider(int minFPS = 1, int maxFPS = 120) : base(200)
	{
		this.minFPS = minFPS;
		this.maxFPS = maxFPS;
	}

	public override void Init()
	{
		selectedFramerate = 60;
		resetBtn = EditorGUIUtility.IconContent("Refresh");
		resetBtn.tooltip = "Reset FPS";
	}

	protected override void OnDrawInList(Rect position)
	{
		position.width = 70.0f;
		EditorGUI.LabelField(position, "Min FPS");

		position.x += position.width + FieldSizeSpace;
		position.width = 50.0f;
		minFPS = Mathf.RoundToInt(EditorGUI.IntField(position, "", minFPS));

		position.x += position.width + FieldSizeSpace;
		position.width = 70.0f;
		EditorGUI.LabelField(position, "Max FPS");

		position.x += position.width + FieldSizeSpace;
		position.width = 50.0f;
		maxFPS = Mathf.RoundToInt(EditorGUI.IntField(position, "", maxFPS));
	}

	protected override void OnDrawInToolbar()
	{
		EditorGUILayout.LabelField("FPS", GUILayout.Width(30));

		var framerate = selectedFramerate;
		if (GUILayout.Button(resetBtn, ToolbarStyles.commandButtonStyle))
		{
			framerate = 60;
			Debug.Log("Reset FPS to 60");
		}

		selectedFramerate = EditorGUILayout.IntSlider("", framerate, minFPS, maxFPS, GUILayout.Width(WidthInToolbar - 30.0f));
		if (EditorApplication.isPlaying && selectedFramerate != Application.targetFrameRate)
			Application.targetFrameRate = selectedFramerate;
	}
}