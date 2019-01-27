using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScoreGuiScript : MonoBehaviour {
    public GUIStyle Style;

    [Range(0, 1000)]
    public int X;
    [Range(0, 1000)]
    public int Y;

    [Range(-50, 50)]
    public int Spacing;

    void OnGUI() {
         Vector2 pos = new Vector2(X, Y);
         pos = drawText(pos, "Score: " + (long)ScoreScript.Score);
         pos = drawText(pos, "Score max: " + (long)ScoreScript.ScoreMax);
         pos = drawText(pos, "Stars: " + (long)(ScoreScript.StarsEarned));
         //pos = drawText(pos, "Damaged objects: " + ScoreScript.NumDamagedGoals);
         //pos = drawText(pos, "Household health: " + ScoreScript.Health);
         //pos = drawText(pos, "Household health max: " + ScoreScript.HealthMax);
         //pos = drawText(pos, "Destroyed items: " + ScoreScript.NumDestroyedObjects);
    }

    // Draw text and increment Y position
    private Vector2 drawText(Vector2 pos, string text) {
        Vector2 size = new Vector2(1000, 300);

        Rect r = new Rect(pos, size);
        GUI.Label(r, text, Style);

        float height = Style.CalcHeight(new GUIContent(text), size.x);

        return new Vector2(pos.x, pos.y + height + Spacing);
    }
}
