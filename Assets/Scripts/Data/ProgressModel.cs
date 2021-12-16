using System;
using System.Collections.Generic;

[Serializable]
public class ProgressModel
{
    public List<ChapterModel> ChaptersList = new List<ChapterModel>();
    public int CurrentLevel;
}

[Serializable]
public class ChapterModel
{
    public int ChapterIndex;
    public int StageIndex;
    public ChapterType ChapterType;
    public float Karma;
}
