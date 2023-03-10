using System;
using System.Collections;
using MyriadOfJSON.Helpers;
using MyriadOfJSON.Items.Data;
using DiskCardGame;

namespace MyriadOfJSON.Items.Actions;
using LetterAnimation = DiskCardGame.TextDisplayer.LetterAnimation;

// TODO!!!!!!!!!!!! IMPORTANT
// - Make dialogue event
public class ShowMessage : ActionBase
{
    public string Message { get; }
    public float Duration { get; }
    public Emotion Emotion { get; }
    public LetterAnimation LetterAnimation { get; }
    public bool WaitForInput { get; }

    public ShowMessage(ShowMessageData data)
    {
        Message = data.message ?? string.Empty;
        Duration = data.duration ?? 2f;
        Emotion = Enum.TryParse(data.emotion?.SentenceCase(), out Emotion e)
            ? e : Emotion.Neutral;
        LetterAnimation = Enum.TryParse(data.letterAnimation?.SentenceCase(), out LetterAnimation l)
            ? l : default;
        WaitForInput = data.waitForInput ?? false;
        SetOrder(data);
    }

    public override IEnumerator Trigger()
    {
        yield return WaitForInput 
            ? ShowUntilInput()
            : ShowThenClear();
    }

    private IEnumerator ShowUntilInput()
    {
        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(
                message: Message,
                emotion: Emotion, 
                letterAnimation: LetterAnimation 
            );
    }

    private IEnumerator ShowThenClear()
    {
        yield return Singleton<TextDisplayer>.Instance.ShowThenClear(
            message: Message,
            length: Duration,
            emotion: Emotion,
            letterAnimation: LetterAnimation
        );
    }
} 
