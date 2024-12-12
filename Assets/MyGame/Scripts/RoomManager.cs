using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private TableStudentData tableLayout;
    [SerializeField] private StudentData[] students; // ScriptableObject-Array mit Sprite-Referenzen
    [SerializeField] private GameObject tablePrefab;
    [SerializeField] private GameObject chairPrefab;
    [SerializeField] private GameObject Human; // Human-Objekt
    [SerializeField] private GameObject Laptop;

    private int currentStudentIndex = 0; // Index für den aktuellen Schüler

    // Start is called before the first frame update
    void Start()
    {
        for (int row = 0; row < tableLayout.rows; row++)
        {
            for (int col = 0; col < tableLayout.columns; col++)
            {
                Vector3 tablePosition = new Vector3(col * tableLayout.tableSpacing, 0, row * tableLayout.tableSpacing);
                GameObject table = Instantiate(tablePrefab, tablePosition, Quaternion.identity, transform);

                Transform pos1 = table.transform.Find("pos1");
                Transform pos2 = table.transform.Find("pos2");
                Transform pos3 = table.transform.Find("pos3");
                Transform pos4 = table.transform.Find("pos4");
                Transform pos5 = table.transform.Find("pos5");
                Transform pos6 = table.transform.Find("pos6");

                if (pos1)
                {
                    Instantiate(chairPrefab, pos1.position, pos1.rotation, table.transform);
                }

                if (pos2)
                {
                    Instantiate(chairPrefab, pos2.position, pos2.rotation, table.transform);
                }

                if (pos3)
                {
                    Quaternion adjustedRotation = pos3.rotation * Quaternion.Euler(0, 90, 0);
                    GameObject human = Instantiate(Human, pos3.position, adjustedRotation, table.transform);
                    AssignStudentSprite(human); // Setze das Sprite für den Schüler
                }

                if (pos4)
                {
                    Quaternion adjustedRotation = pos4.rotation * Quaternion.Euler(0, 90, 0);
                    GameObject human = Instantiate(Human, pos4.position, adjustedRotation, table.transform);
                    AssignStudentSprite(human); // Setze das Sprite für den Schüler
                }

                if (pos5)
                {
                    Quaternion adjustedRotation = pos5.rotation * Quaternion.Euler(0, -90, 0);
                    Instantiate(Laptop, pos5.position, adjustedRotation, table.transform);
                }

                if (pos6)
                {
                    Quaternion adjustRotation = pos6.rotation * Quaternion.Euler(0, -90, 0);
                    Instantiate(Laptop, pos6.position, adjustRotation, table.transform);
                }
            }
        }
    }

    private void AssignStudentSprite(GameObject human)
    {
        // Hole den aktuellen Schülernamen
        StudentData currentStudent = students[currentStudentIndex];

        // Erhöhe den Index für den nächsten Aufruf
        currentStudentIndex = (currentStudentIndex + 1) % students.Length; // Zyklisch

        if (currentStudent != null && currentStudent.studentImage != null)
        {
            // Finde die Komponente "Man_Head_Mesh" im Human-Objekt
            SkinnedMeshRenderer headMeshRenderer = human.transform.Find("Man_Head_Mesh")?.GetComponent<SkinnedMeshRenderer>();

            if (headMeshRenderer != null)
            {
                // Erstelle ein neues Material und setze die Textur basierend auf dem Sprite
                Material headMaterial = new Material(Shader.Find("Standard"));
                headMaterial.mainTexture = currentStudent.studentImage.texture;

                // Wende das Material auf die "Man_Head_Mesh"-Komponente an
                headMeshRenderer.material = headMaterial;
            }
            else
            {
                Debug.LogWarning("Man_Head_Mesh nicht gefunden im Human-Objekt");
            }

            // Setze den Namen des Objekts entsprechend des Schülers
            human.name = currentStudent.name;
        }
        else
        {
            Debug.LogWarning("Kein StudentData oder Sprite für Index " + currentStudentIndex);
            human.name = "Schüler"; // Fallback-Name
        }
    }
}
