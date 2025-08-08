using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : MonoBehaviour
{
    public List<Transform> position;
    public float duration = 1f;

    private int _index = 0;

    void Start()
    {
        transform.position = position[0].transform.position;
        NextIndex();
        StartCoroutine(StartMovement());
    }

    private void NextIndex()
    {
        _index++;
        if (_index >= position.Count) _index = 0;
    }

    IEnumerator StartMovement()
    {
        float time = 0;

        while (true)
        {
            var currentPosition = transform.position;

            while (time < duration)
            {
                //andar durante o tempo do lerp
                transform.position = Vector3.Lerp(currentPosition, position[_index].transform.position,(time/duration));

                time += Time.deltaTime;
                yield return null;
            }
            NextIndex();
            time = 0;

            //isso faz com que espere 1 frame
            yield return null;
        }
    }
}
