[System.Serializable]
public struct Score
{
  public int Points;
  public string Name;

  public Score(int Points, string name)
  {
    this.Points = Points;
    this.Name = name;
  }
}
