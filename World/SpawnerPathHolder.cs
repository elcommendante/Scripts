using UnityEngine;

[ExecuteInEditMode]
public class SpawnerPathHolder : MonoBehaviour
{
    private Vector3 spawnInitialPosition;
    private Quaternion spawnInitialRotation;
    
    void Start()
    {
        spawnInitialPosition = transform.position;
        spawnInitialRotation = transform.rotation;
    }
    void Update()
    {
        transform.position = spawnInitialPosition;
        transform.rotation = spawnInitialRotation;
    }
    // Update is called once per frame
    // void Update()
    // {
    //     transform.localScale = new Vector3(FixeScale / parent.parent.transform.localScale.x, FixeScale / parent.transform.localScale.y, FixeScale / parent.transform.localScale.z);
    // }
}