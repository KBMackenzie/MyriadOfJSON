namespace MiscellaneousJSON.Parser.Functions;

/* Delegate for PlayableCard modifications, as I really need the 'ref' parameter for it. */
public delegate void PlayableCardAction<T>(ref DiskCardGame.PlayableCard card, T param);
