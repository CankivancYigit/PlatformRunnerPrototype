public readonly struct OpponentReachedFinishEvent
{
	public readonly Opponent Opponent;
	public OpponentReachedFinishEvent(Opponent opponent)
	{
		Opponent = opponent;
	}
}
