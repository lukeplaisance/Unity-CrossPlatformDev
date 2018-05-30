using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EditorCharacterCreatorWindow : UnityEditor.EditorWindow
{
    public class StatButton
    {
        public Stat stat;
        public Rect buttonRect = new UnityEngine.Rect(x: 75, y: 75, width: 150, height: 300);

        public void Draw()
        {
            if (stat == null)
            {
                stat = new Stat()
                {
                    Name = "Strength",
                    Value = 5,
                    Description = "shows how strong you are"
                };
            }

            var contentRect = new Rect(buttonRect);
            contentRect.position = new Vector2(buttonRect.position.x, buttonRect.position.y + 15);
            GUI.Box(buttonRect, "stat");

            GUILayout.BeginArea(contentRect);

            GUILayout.Toggle(isSelected, "isSelected");
            buttonRect.width = GUILayout.HorizontalSlider(buttonRect.width, 150, 300);
            buttonRect.height = GUILayout.HorizontalSlider(buttonRect.height, 150, 300);

            GUILayout.EndArea();
        }
        public bool isSelected = false;
        public void PollEvents()
        {
            switch (Event.current.type)
            {
                case EventType.MouseDown:
                    if (Event.current.button == 0)
                    {
                        if (buttonRect.Contains(Event.current.mousePosition))
                        {
                            isSelected = true;
                            GUI.changed = true;
                        }
                        else
                        {
                            isSelected = false;
                            GUI.changed = true;
                        }
                    }
                    break;
                case EventType.MouseDrag:
                    if (Event.current.button == 0)
                    {
                        if(isSelected)
                        {
                            buttonRect.position += Event.current.delta;
                            Event.current.Use();
                        }
                    }
                    break;
            }
        }
    }
    public System.Collections.Generic.List<StatButton> statButtons = new System.Collections.Generic.List<StatButton>();
    [UnityEditor.MenuItem("Tools/Button Test")]
    public static void Init()
    {
        var window = ScriptableObject.CreateInstance<EditorCharacterCreatorWindow>();
        window.Show();
    }

    public void CreateButton()
    {
        statButtons.Add(new StatButton());
    }

    private void OnGUI()
    {
        foreach(var sb in statButtons)
        {
            sb.Draw();
            sb.PollEvents();
        }
        Repaint();

        switch (Event.current.type)
        {
            case EventType.MouseDown:
                if (Event.current.button == 1)
                {
                    var gm = new UnityEditor.GenericMenu();
                    gm.AddItem(new GUIContent("Create Stat"), false, CreateButton);
                    gm.AddItem(new GUIContent("Print info"), false, () => { Debug.Log("Info Printed"); });
                    gm.ShowAsContext();
                }
                break;
        }
    }
}
