using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    [Range(0, 3)]
    public int EditorLevelOverride;

    private static int currertLevel;

    private void Start() {
        // This should maybe only be true if we started scene directly with editor
        Debug.Log("Scene id " + SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            SetLevel(EditorLevelOverride);
            Debug.Log("Level overridden");
        }
    }

    public void SetLevel(int level) {
        if (level < 0) {
            Debug.LogWarning("Invalid level " + level);
            level = 0;
        }

        else if (level >= GameLevels.Levels.Count) {
            Debug.LogWarning("Invalid level " + level);
            level = GameLevels.Levels.Count - 1;
        }

        currertLevel = level;
    }

    public static LevelDescriptor CurrentLevel => GameLevels.Levels[currertLevel];
}

public class LevelDescriptor {
    public int ExtraCats = 0;
    public int ExtraBowls = 0;
}

public static class GameLevels {
    public static List<LevelDescriptor> Levels = new List<LevelDescriptor>() {
        new LevelDescriptor() { ExtraCats = 0 },
        new LevelDescriptor() { ExtraCats = 1 },
        new LevelDescriptor() { ExtraCats = 2, ExtraBowls = 1 },
        new LevelDescriptor() { ExtraCats = 3, ExtraBowls = 2 }
    };
}