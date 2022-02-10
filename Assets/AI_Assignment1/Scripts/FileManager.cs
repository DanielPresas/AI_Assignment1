using System;
using System.IO;
using System.Text;
using UnityEngine;

public static class FileManager {
    public struct LoadFileReturn {
        public bool success;
        public DoorProbabilities probabilities;
    }

    public static char separator = '\t';

    public static LoadFileReturn LoadFile(string filepath) {
        var ret = new LoadFileReturn {
            success = true,
            probabilities = new DoorProbabilities {
                yyy = 0, yyn = 0, yny = 0, ynn = 0,
                nyy = 0, nyn = 0, nny = 0, nnn = 0,
            },
        };

        if(!File.Exists(filepath)) {
            Debug.LogError($"Could not load file because it does not exist!\n\tPath: {filepath}");
            ret.success = false;
            return ret;
        }

        var fStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        var reader = new StreamReader(fStream, Encoding.UTF8);

        var next = reader.ReadLine();
        if(next.Length == 0 || next.ToLower() != $"hot{separator}noisy{separator}safe door{separator}percentage of doors") {
            Debug.LogError($"Could not load file because the proper file header is missing!\n\tPath: {filepath}");
            ret.success = false;
            return ret;
        }

        int line = 1;  // @Note(Daniel): Start at 1 to account for header
        while(!reader.EndOfStream) {
            next = reader.ReadLine(); line++;
            var splits = next.Split(separator);

            try {
                if(splits[0].ToLower() == "y" && splits[1].ToLower() == "y" && splits[2].ToLower() == "y") {
                    ret.probabilities.yyy = float.Parse(splits[3]);
                }
                else if(splits[0].ToLower() == "y" && splits[1].ToLower() == "y" && splits[2].ToLower() == "n") {
                    ret.probabilities.yyn = float.Parse(splits[3]);
                }
                else if(splits[0].ToLower() == "y" && splits[1].ToLower() == "n" && splits[2].ToLower() == "y") {
                    ret.probabilities.yny = float.Parse(splits[3]);
                }
                else if(splits[0].ToLower() == "y" && splits[1].ToLower() == "n" && splits[2].ToLower() == "n") {
                    ret.probabilities.ynn = float.Parse(splits[3]);
                }
                else if(splits[0].ToLower() == "n" && splits[1].ToLower() == "y" && splits[2].ToLower() == "y") {
                    ret.probabilities.nyy = float.Parse(splits[3]);
                }
                else if(splits[0].ToLower() == "n" && splits[1].ToLower() == "y" && splits[2].ToLower() == "n") {
                    ret.probabilities.nyn = float.Parse(splits[3]);
                }
                else if(splits[0].ToLower() == "n" && splits[1].ToLower() == "n" && splits[2].ToLower() == "y") {
                    ret.probabilities.nny = float.Parse(splits[3]);
                }
                else if(splits[0].ToLower() == "n" && splits[1].ToLower() == "n" && splits[2].ToLower() == "n") {
                    ret.probabilities.nnn = float.Parse(splits[3]);
                }
                else {
                    Logger.Error($"Could not load file because the given values are invalid!\n\tPath: {filepath} (Line {line})");
                    ret.success = false;
                    return ret;
                }
            }
            catch(Exception e) {
                Logger.ExceptionError(e, $"Could not load file because the given values are invalid!\n\tPath: {filepath} (Line {line})");
                ret.success = false;
                return ret;
            }
        }

        return ret;
    }
}
