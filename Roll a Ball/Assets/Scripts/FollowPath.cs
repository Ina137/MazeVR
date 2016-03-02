using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FollowPath : MonoBehaviour {

    private int positionOnPath;
    private GameObject[] points;
    public Transform current;
    public float speed = 1;
    public float rotationRate = 0.25f;
    public Text countText;
    public Text winText;
    private int count;

    // Use this for initialization
    void Start () {
        positionOnPath = 0;
        points = GameObject.FindGameObjectsWithTag("navPoint");
        count = 0;
        winText.text = "";
        UpdateTarget();
        SetCountText();
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "navPoint")
        {

            positionOnPath += 1;
            UpdateTarget();

        }

        if (coll.gameObject.CompareTag("Pick Up"))
        {
            coll.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }




    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }

    }

    // Update is called once per frame
    void Update () {

        float dist = Vector3.Distance(transform.position, current.position);
        transform.position = Vector3.MoveTowards(transform.position, current.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, current.rotation, Time.deltaTime * 1/dist * rotationRate);


    }

    void UpdateTarget()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].GetComponent<navPoint>().place == positionOnPath + 1)
            {
                current = points[i].transform;
            }

        }

    }
}
