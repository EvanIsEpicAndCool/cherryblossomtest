namespace cherryblossomtest.Core
{
	public abstract class PlayerTicker : IOrderedLoadable
	{
		public float Priority => 1;

		public abstract bool Active(Player Player);

		public abstract int TickFrequency { get; }

		public void Load()
		{
			CherryPlayer.spawners.Add(this);
		}

		public void Unload()
		{
			CherryPlayer.spawners?.Remove(this);
		}

		public virtual void Tick(Player Player) { }
	}
}