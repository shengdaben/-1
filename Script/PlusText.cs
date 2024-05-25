using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusText : MonoBehaviour
{
    public float moveSpeed;
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        //向上移动
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void LateUpdate()
    {
        //朝向摄影机
        transform.LookAt(Camera.main.transform);
    }
}
