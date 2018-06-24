using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelStat {

	public bool hasCrystals = false;
    public bool hasAllFruits = false;
    public bool levelPassed = false;
    public List<int> collectedFruits = new List<int>();
    
    static public LevelStat Deserialize(string str) {
        string code = PlayerPrefs.GetString(str, null);
        return JsonUtility.FromJson<LevelStat>(code);
    }
}
