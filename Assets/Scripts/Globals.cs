﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Globals
{
    public static MarioOverworldPlayerController Mario;
    
    public static MusicManager MusicManager;
    public static SoundManager SoundManager;
    
    public static Dictionary<string, ArrayList> LoadCSV(TextAsset file) {
        
        Dictionary<string, ArrayList> dictionary = new Dictionary<string, ArrayList>();
        ArrayList list = new ArrayList();
        
        var content = file.text;
        var lines = content.Split("\n");

        for (int i=0; i < lines.Length; i++)
        {
            list = new ArrayList();
            var line = lines[i];
            if (string.IsNullOrEmpty(line)) continue;
            string[] values = line.Split(',');
            for (int j=1; j < values.Length; j++) {
                list.Add(values[j]);
            }

            values[0] = new string(values[0].Where(c => !char.IsControl(c)).ToArray());
            if (values[0] != "") dictionary.Add(values[0], list);
        }

        return dictionary;
    }
}