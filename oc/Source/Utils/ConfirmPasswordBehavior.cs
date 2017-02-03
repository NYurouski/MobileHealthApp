using System;
using Xamarin.Forms;

namespace oc
{
	public class ConfirmPasswordBehavior : Behavior<Entry>
	{
		static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(ConfirmPasswordBehavior), false);
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

		public static readonly BindableProperty CompareToEntryProperty = BindableProperty.Create("CompareToEntry", typeof(Entry), typeof(ConfirmPasswordBehavior), null);

		public Entry CompareToEntry
		{
			get { return (Entry)GetValue(CompareToEntryProperty); }
			set { SetValue(CompareToEntryProperty, value); }
		}
		public bool IsValid
		{
			get { return (bool)GetValue(IsValidProperty); }
			private set { SetValue(IsValidPropertyKey, value); }
		}
		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged += HandleTextChanged;
			base.OnAttachedTo(bindable);
		}
		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= HandleTextChanged;
			base.OnDetachingFrom(bindable);
		}

		void HandleTextChanged(object sender, TextChangedEventArgs e)
		{
			var password = CompareToEntry.Text;
			var confirmPassword = e.NewTextValue;
			IsValid = password.Equals(confirmPassword);
		}
	}
}

