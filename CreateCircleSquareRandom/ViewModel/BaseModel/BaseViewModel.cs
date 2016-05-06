using System;
using System.ComponentModel;

namespace CreateCircleSquareRandom
{
	/// <summary>
	/// Base view model.
	/// </summary>
	public class BaseViewModel:INotifyPropertyChanged
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CreateCircleSquareRandom.BaseViewModel"/> class.
		/// </summary>
		public BaseViewModel ()
		{
		}
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Raises the property changed event.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string propertyName=null)
		{
			if (PropertyChanged != null) {
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}
	}
}

