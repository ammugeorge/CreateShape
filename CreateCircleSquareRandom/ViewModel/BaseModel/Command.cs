using System;

namespace CreateCircleSquareRandom
{
	/// <summary>
	/// Base class for Commands
	/// </summary>
	public class Command
	{
		Action CommandExecute;

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateCircleSquareRandom.Command"/> class.
		/// </summary>
		/// <param name="commandExecute">Command execute.</param>
		public Command (Action commandExecute)
		{
			CommandExecute = commandExecute;
		}
		/// <summary>
		/// Execute this instance.
		/// </summary>
		public void Execute()
		{
			CommandExecute ();
		}

	}
}

