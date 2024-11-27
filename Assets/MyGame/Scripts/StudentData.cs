using UnityEngine;

[CreateAssetMenu(fileName = "StudentData", menuName = "StudentData", order = 1)]
public class StudentData : ScriptableObject
{
    public string studentName;
    public Color eyecolor;
    public Sprite studentImage;
    public AudioClip studentClip;
}
