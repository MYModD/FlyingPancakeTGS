using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectErase : MonoBehaviour
{
    private float _delay = 1.0f;

    private void OnEnable()
    {
        StartCoroutine(DeactivateAfterDelay());
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(_delay);
        gameObject.SetActive(false);
    }
}
