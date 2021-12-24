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
    public string StageName;
    public ChapterType ChapterType;
    public int Karma;
}
