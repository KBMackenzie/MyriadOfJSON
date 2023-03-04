using System;
using System.Collections;
using System.Linq;
using System.Text;
using DiskCardGame;

namespace MyriadOfJSON.Items.Actions;

// TODO!!!!!!!!!!!! IMPORTANT
// - Make dialogue event
public class ShowMessage : ActionBase
{
    public string? Message;
    public int? Duration;
    public Emotion? Emotion;
    public TextDisplayer.LetterAnimation? LetterAnimation;
    public bool? WaitForInput;

    public override IEnumerator Trigger()
    {
        yield return (WaitForInput ?? false)
            ? ShowUntilInput()
            : ShowThenClear();
    }

    private IEnumerator ShowUntilInput()
    {
        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(
            message: Message,
            emotion: Emotion ?? default,
            letterAnimation: LetterAnimation ?? default
        );
    }

    private IEnumerator ShowThenClear()
    {
        yield return Singleton<TextDisplayer>.Instance.ShowThenClear(
            message: Message,
            length: Duration ?? 2f,
            emotion: Emotion ?? default,
            letterAnimation: LetterAnimation ?? default
        );
    }
} 
