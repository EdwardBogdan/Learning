using Core;
using CustomUnityUtils.TimeUtils;
using DialogSystem;
using Localization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIManager : MonoBehaviour
{
    [SerializeField] private float _textSpeed;
    [SerializeField] private Cooldown _CDSkipp;
    [SerializeField] private Animator _animator;
    [SerializeField] private Text _text;
    [SerializeField] private Text _continueText;
    [SerializeField] private Image _leftFace;
    [SerializeField] private Image _rightFace;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _sfxSource;

    private static DialogUIManager _instance;

    private static DialogUIManager I => _instance == null?  CreateUI() : _instance;

    private static readonly int animatorKey_Side = Animator.StringToHash("Side");
    private static readonly int animatorKey_Open = Animator.StringToHash("Open");
    private static readonly int animatorKey_Close = Animator.StringToHash("Close");

    private Action OnContinue;

    private Coroutine _routine;

    private DialogData _currentDialog;

    private int phraseIndex;

    private string currentPhrase;

    public void Continue()
    {
        OnContinue?.Invoke();
    }
    internal static void StartDialog(DialogData data)
    {
        I.phraseIndex = 0;
        I._currentDialog = data;
        I.SetPhrase(data.Dialog[_instance.phraseIndex]);
        I.SetOpen();
        I._CDSkipp.Reset();
    }
    private static DialogUIManager CreateUI()
    {
        return _instance = Instantiate(SystemCore.DialogPrefab, SystemCore.OverlayCanvas.transform);
    }

    private void SetPhrase(PhraseData data)
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
        }

        if (data.Side)
        {
            _leftFace.sprite = data.Face;
        }
        else
        {
            _rightFace.sprite = data.Face;
        }

        SetSide(data.Side);

        _routine = StartCoroutine(TypeDialogtext());

        _continueText.gameObject.SetActive(false);

        OnContinue = OnSkip;
    }
    private void SetSide(bool side)
    {
        _animator.SetBool(animatorKey_Side, side);
    }
    private void SetOpen()
    {
        _animator.SetTrigger(animatorKey_Open);
    }
    private void SetClose()
    {
        _animator.SetTrigger(animatorKey_Close);
    }
    private void NextPhrase()
    {
        if (!_CDSkipp.IsReady) return;

        _CDSkipp.Reset();

        _currentDialog.Dialog[phraseIndex].Event?.Invoke();

        phraseIndex++;

        if (phraseIndex >= _currentDialog.Dialog.Length)
        {
            SetClose();
            return;
        }

        OnContinue = OnSkip;

        SetPhrase(_currentDialog.Dialog[phraseIndex]);
    }
    private void OnSkip()
    {
        if (!_CDSkipp.IsReady) return;

        _CDSkipp.Reset();
        StopCoroutine(_routine);

        I._text.text = currentPhrase;

        OnContinue = NextPhrase;

        _continueText.gameObject.SetActive(true);
    }

    private IEnumerator TypeDialogtext()
    {
        _text.text = string.Empty;

        string key = _currentDialog.Dialog[phraseIndex].PhraseKey;
        currentPhrase = LocalizationCore.GetText(LocaleGroup.Dialog, key);

        foreach (var letter in currentPhrase)
        {
            _text.text += letter;
            _sfxSource.PlayOneShot(_clip);
            yield return new WaitForSeconds(_textSpeed);
        }

        _routine = null;

        OnContinue = NextPhrase;

        _continueText.gameObject.SetActive(true);
    }
}
