using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnController : MonoBehaviour
{
    public TMP_Text HeightText;

    float height = 0;
    LineRenderer line;

    GameObject activePiece = null;
    LinkedList<PieceController> pieces = new LinkedList<PieceController>();

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.startColor = Color.black;
        line.endColor = Color.black;
        line.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject newPiece = null;

        if (Input.GetButtonUp("Fire2"))
            newPiece = Instantiate(GameObject.Find("Piece"));

        if (newPiece != null)
        {
            //newPiece.transform.localScale = new Vector3(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f));
            newPiece.transform.position = new Vector3(8, 0.6f, 0);
            newPiece.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

            if (activePiece != null)
                activePiece.GetComponent<PieceController>().isActive = false;

            newPiece.GetComponent<PieceController>().isActive = true;
            activePiece = newPiece;

            pieces.AddLast(newPiece.GetComponent<PieceController>());
        }

        if (activePiece != null)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
                activePiece.transform.position.y + 5.0f, Camera.main.transform.position.z);

            Debug.DrawRay(activePiece.transform.position, activePiece.transform.position.y * Vector3.down);
            line.SetPosition(0, activePiece.transform.position);
            line.SetPosition(1, new Vector3(activePiece.transform.position.x, 0, activePiece.transform.position.z));
            line.startColor = Color.black;
            line.endColor = Color.black;

        }

        float tempHeight = 0;
        foreach (PieceController p in pieces)
        {
            if (p.rb.velocity.magnitude < 0.2f && p.rb.position.y > tempHeight)
                tempHeight = p.rb.position.y;
        }
        height = tempHeight;

        HeightText.text = "Height: " + Mathf.CeilToInt(height);
    }
}
