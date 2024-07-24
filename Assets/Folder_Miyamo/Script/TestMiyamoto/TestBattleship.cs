using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattleship : MonoBehaviour {
    public Transform _testTransform1;
    [MinMaxSlider(0, 360f)] public Vector2 _testTransform1Range;
    public Transform _testTransform2;
    [MinMaxSlider(-50, 50)] public Vector2 _testTransform2Range;
    public Transform _enemyTransform;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        // _testTransform1‚Ì‰ñ“]
        RotateTransformY(_testTransform1, _testTransform1Range, _enemyTransform);

        // _testTransform2‚Ì‰ñ“]
        RotateTransform2(_testTransform1, _testTransform2, _testTransform2Range, _enemyTransform);
    }

    private void RotateTransformY(Transform targetTransform, Vector2 rotationRange, Transform target) {
        Vector3 direction = target.position - targetTransform.position;
        direction.y = 0; // Y²‚Ì‰ñ“]‚¾‚¯‚ğl—¶‚·‚é‚½‚ß‚ÉAY¬•ª‚ğ0‚É‚·‚é

        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Œ»İ‚Ì‰ñ“]Šp“x‚ğY²‚É§ŒÀ
            float targetYRotation = targetRotation.eulerAngles.y;

            // ‰ñ“]”ÍˆÍ“à‚É§ŒÀ
            targetYRotation = Mathf.Clamp(targetYRotation, rotationRange.x, rotationRange.y);

            // V‚µ‚¢‰ñ“]Šp“x‚ğY²‚Éİ’è
            Quaternion limitedRotation = Quaternion.Euler(0, targetYRotation, 0);

            // ‹…–ÊüŒ`•âŠÔ‚ğg‚Á‚Ä‰ñ“]‚ğ™X‚Éƒ^[ƒQƒbƒg‚ÉŒü‚¯‚é
            targetTransform.rotation = Quaternion.Lerp(targetTransform.rotation, limitedRotation, 0.05f);
        }
    }

    private void RotateTransform2(Transform parentTransform, Transform targetTransform, Vector2 rotationRange, Transform enemyTransform) {
        Vector3 direction = enemyTransform.position - targetTransform.position;

        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Œ»İ‚ÌX²‰ñ“]Šp“x‚ğ§ŒÀ
            float targetXRotation = targetRotation.eulerAngles.x;
            targetXRotation = Mathf.Clamp(targetXRotation, rotationRange.x, rotationRange.y);

            // V‚µ‚¢‰ñ“]Šp“x‚ğİ’è
            Quaternion limitedRotation = Quaternion.Euler(targetXRotation, parentTransform.eulerAngles.y, 0);

            // ‹…–ÊüŒ`•âŠÔ‚ğg‚Á‚Ä‰ñ“]‚ğ™X‚Éƒ^[ƒQƒbƒg‚ÉŒü‚¯‚é
            targetTransform.rotation = Quaternion.Lerp(targetTransform.rotation, limitedRotation, 0.05f);
        }
    }
}
