using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattleship : MonoBehaviour {
    [Foldout("‘å–C")]
    [SerializeField, Header("‘å–C01")]
    private Transform _largeTurret01;
    [Foldout("‘å–C")]
    [SerializeField, Header("‘å–C01‚Ì‰ñ“]”ÍˆÍ")]
    [MinMaxSlider(-360, 360f)] private Vector2 _largeTurret01Range;

    [Foldout("‘å–C")]
    [SerializeField, Header("‘å–C02")]
    private Transform _largeTurret02;
    [Foldout("‘å–C")]
    [SerializeField, Header("‘å–C02‚Ì‰ñ“]”ÍˆÍ")]
    [MinMaxSlider(-50, 50)] private Vector2 _largeTurret02Range;

    [Foldout("“G")]
    [SerializeField, Header("“G‚Ìƒgƒ‰ƒ“ƒXƒtƒH[ƒ€")]
    private Transform _enemyTransform;

    // Update is called once per frame
    void FixedUpdate() {
        // _largeTurret01‚Ì‰ñ“]
        RotateTurretY(_largeTurret01, _largeTurret01Range, _enemyTransform);

        // _largeTurret02‚Ì‰ñ“]
        RotateTurretX(_largeTurret01, _largeTurret02, _largeTurret02Range, _enemyTransform);
    }

    private void RotateTurretY(Transform turretTransform, Vector2 rotationRange, Transform target) {
        Vector3 direction = target.position - turretTransform.position;
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
            turretTransform.rotation = Quaternion.Lerp(turretTransform.rotation, limitedRotation, 0.05f);
        }
    }

    private void RotateTurretX(Transform parentTransform, Transform turretTransform, Vector2 rotationRange, Transform target) {
        Vector3 direction = target.position - parentTransform.position;

        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Œ»İ‚ÌX²‰ñ“]Šp“x‚ğ§ŒÀ
            float targetXRotation = targetRotation.eulerAngles.x;
            if (targetXRotation > 180)
                targetXRotation -= 360; // X²‰ñ“]Šp“x‚Ì³‹K‰»
            targetXRotation = Mathf.Clamp(targetXRotation, rotationRange.x, rotationRange.y);

            // V‚µ‚¢‰ñ“]Šp“x‚ğİ’è
            Quaternion limitedRotation = Quaternion.Euler(targetXRotation, parentTransform.eulerAngles.y, 0);

            // ‹…–ÊüŒ`•âŠÔ‚ğg‚Á‚Ä‰ñ“]‚ğ™X‚Éƒ^[ƒQƒbƒg‚ÉŒü‚¯‚é
            turretTransform.rotation = Quaternion.Lerp(turretTransform.rotation, limitedRotation, 0.05f);
        }
    }
}
