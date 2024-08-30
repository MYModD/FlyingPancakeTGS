using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimit : MonoBehaviour
{

    [Header("��������")]
    public float _limitTime = 60f;

    [SerializeField,Header("�ł�����readonly�ɂ�����")]
    public bool _isStart = false;


    private float _firstMaxtime = default;
    // ��Œ���


    // �Q�[�����X�^�[�g����Ƃ����s����
    public void LimitTimerStart() {
        _isStart = true;
        _firstMaxtime = _limitTime;
    }

    // Update is called once per frame
    void Update()
    {
        // isStart��true�̂Ƃ�
        if (_isStart) {

            _limitTime -= Time.deltaTime;
            if (_limitTime <= 0f) {
                End3rdGame();
            }
        
        }
    }


    /// <summary>
    /// �^�C�}�[��0�ɂȂ����������́A1�ʂɂȂ����G�����j���ꂽ����s����
    /// </summary>
    public  void End3rdGame() {

        _isStart = false;
        float cashTime = _limitTime;
        if (cashTime <= 0) {
            cashTime = 0;
        }

        Debug.Log($"3rd�}�b�v�̃^�C�}�[��  {cashTime}  �ł���!!");

        string floatTostring = ChangeTimeText(cashTime);
        Debug.LogWarning(floatTostring);

        GameObject scoreMangerObject = GameObject.Find("ScoreManager");
        scoreMangerObject.GetComponent<ScoreManager>().InputToBeTheTopScore(cashTime, _firstMaxtime,floatTostring);


    }




    #region �֗����\�b�hBy�����Ƃ�

    private string ChangeTimeText(float secondTime) {
        // �萔��`
        const int SECONDS_IN_MINUTE = 60;
        const int MILLISECONDS_IN_SECOND = 1000;
        const int TWO_DIGIT_THRESHOLD = 10;
        const int THREE_DIGIT_THRESHOLD = 100;

        // ���Ԃ�60�b�����̏ꍇ�A���̂܂܏����_�ȉ�3���܂ł̕b����Ԃ�
        if (secondTime < SECONDS_IN_MINUTE) {
            return secondTime.ToString("F3");
        }

        // ���̐����������v�Z
        int minuteTime = (int)(secondTime / SECONDS_IN_MINUTE);

        // �c��̕b�����v�Z
        float remainingSeconds = secondTime % SECONDS_IN_MINUTE;
        int seconds = (int)remainingSeconds;

        // �c��̃~���b���v�Z
        int milliseconds = (int)((remainingSeconds - seconds) * MILLISECONDS_IN_SECOND);

        // �b��2���\���Ƀt�H�[�}�b�g
        string secondText = seconds < TWO_DIGIT_THRESHOLD ? "0" + seconds.ToString() : seconds.ToString();

        // �~���b��3���\���Ƀt�H�[�}�b�g
        string millisecondText = milliseconds < THREE_DIGIT_THRESHOLD
            ? milliseconds < TWO_DIGIT_THRESHOLD
                ? "00" + milliseconds.ToString()
                : "0" + milliseconds.ToString()
            : milliseconds.ToString();

        // �t�H�[�}�b�g���ꂽ���ԕ������Ԃ�
        return minuteTime.ToString() + ":" + secondText + ":" + millisecondText;
    }


    #endregion
}
