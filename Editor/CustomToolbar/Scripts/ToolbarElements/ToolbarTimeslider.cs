using System;
using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

[Serializable]
internal class ToolbarTimeslider : BaseToolbarElement
{
	private static GUIContent resetBtn;

	[SerializeField]
	private float minTime = 1;

	[SerializeField]
	private float maxTime = 120;

	public override string NameInList => "[Slider] Timescale";
	public override int SortingGroup => 1;

	public ToolbarTimeslider(float minTime = 0.0f, float maxTime = 10.0f) : base(200)
	{
		this.minTime = minTime;
		this.maxTime = maxTime;
	}

	public override void Init()
	{
		resetBtn = EditorGUIUtility.IconContent("Refresh");
		resetBtn.tooltip = "Reset Timescale";
	}

	protected override void OnDrawInList(Rect position)
	{
		position.width = 70.0f;
		EditorGUI.LabelField(position, "Min Time");

		position.x += position.width + FieldSizeSpace;
		position.width = 50.0f;
		minTime = EditorGUI.FloatField(position, "", minTime);

		position.x += position.width + FieldSizeSpace;
		position.width = 70.0f;
		EditorGUI.LabelField(position, "Max Time");

		position.x += position.width + FieldSizeSpace;
		position.width = 50.0f;
		maxTime = EditorGUI.FloatField(position, "", maxTime);
	}

	protected override void OnDrawInToolbar()
	{
		EditorGUILayout.LabelField("Time", GUILayout.Width(30));

		var timescale = Time.timeScale;
		if (GUILayout.Button(resetBtn, ToolbarStyles.commandButtonStyle))
		{
			timescale = 1;
			Debug.Log("Reset Timescale to 1");
		}

		Time.timeScale = EditorGUILayout.Slider("", timescale, minTime, maxTime, GUILayout.Width(WidthInToolbar - 30.0f));
	}
}