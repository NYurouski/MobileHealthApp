using System;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using oc.Droid.Renderers;
using oc.Source.Helpers.UIHelpers.ControlsHelper;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;


[assembly: ExportRenderer(typeof(ExtendedSwitch), typeof(ExtendedSwitchRenderer))]

namespace oc.Droid.Renderers
{
    /// <summary>
    /// Class ExtendedSwitchRenderer.
    /// </summary>

    public class ExtendedSwitchRenderer : SwitchRenderer
    {
        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>

        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {

               Control.ThumbDrawable.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.Multiply);
            
                Control.TrackDrawable.SetColorFilter(Android.Graphics.Color.Gray, PorterDuff.Mode.Overlay);

                Control.CheckedChange += ControlValueChanged;

            }
         
        }

       

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.CheckedChange -= ControlValueChanged;
              
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Handles the ValueChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ControlValueChanged(object sender, EventArgs e)
        {
            Control.TrackDrawable.SetColorFilter(Android.Graphics.Color.Gray, PorterDuff.Mode.Screen);
            if (Control.Checked)
                Control.TrackDrawable.SetColorFilter(Android.Graphics.Color.Green, PorterDuff.Mode.Screen);
           
        }
    }
}