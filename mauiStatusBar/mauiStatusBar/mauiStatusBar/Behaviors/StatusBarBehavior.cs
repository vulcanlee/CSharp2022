using CommunityToolkit.Maui.Core.Platform;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace mauiStatusBar.Behaviors
{
    /// <summary>
    /// <see cref="PlatformBehavior{TView,TPlatformView}"/> that controls the Status bar color
    /// </summary>
    [UnsupportedOSPlatform("Windows"), UnsupportedOSPlatform("MacCatalyst"), UnsupportedOSPlatform("MacOS")]
    public class StatusBarBehavior : PlatformBehavior<ContentPage>
    {
        /// <summary>
        /// <see cref="BindableProperty"/> that manages the StatusBarColor property.
        /// </summary>
        public static readonly BindableProperty StatusBarColorProperty =
            BindableProperty.Create(nameof(StatusBarColor), typeof(Color), typeof(StatusBarBehavior), Colors.Transparent);


        /// <summary>
        /// <see cref="BindableProperty"/> that manages the StatusBarColor property.
        /// </summary>
        public static readonly BindableProperty StatusBarStyleProperty =
            BindableProperty.Create(nameof(StatusBarStyle), typeof(StatusBarStyle), typeof(StatusBarBehavior), StatusBarStyle.Default);

        /// <summary>
        /// Property that holds the value of the Status bar color. 
        /// </summary>
        public Color StatusBarColor
        {
            get => (Color)GetValue(StatusBarColorProperty);
            set => SetValue(StatusBarColorProperty, value);
        }

        /// <summary>
        /// Property that holds the value of the Status bar color. 
        /// </summary>
        public StatusBarStyle StatusBarStyle
        {
            get => (StatusBarStyle)GetValue(StatusBarStyleProperty);
            set => SetValue(StatusBarStyleProperty, value);
        }

#if !(WINDOWS || MACCATALYST)

        /// <inheritdoc /> 
#if IOS
	protected override void OnAttachedTo(ContentPage bindable, UIKit.UIView platformView)
#elif ANDROID
        protected override void OnAttachedTo(ContentPage bindable, Android.Views.View platformView)
#else
	protected override void OnAttachedTo(Page bindable, object platformView)
#endif
        {
            StatusBar.SetColor(StatusBarColor);
            StatusBar.SetStyle(StatusBarStyle);
        }


        /// <inheritdoc /> 
        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            base.OnPropertyChanged(propertyName);

            if (IsOneOf(propertyName, StatusBarColorProperty, Page.WidthProperty, Page.HeightProperty))
            {
                StatusBar.SetColor(StatusBarColor);
            }
            else if (propertyName == StatusBarStyleProperty.PropertyName)
            {
                StatusBar.SetStyle(StatusBarStyle);
            }
        }
#endif

        public bool IsOneOf(string propertyName, BindableProperty p0, BindableProperty p1, BindableProperty p2)
        {
            return propertyName == p0.PropertyName ||
                propertyName == p1.PropertyName ||
                propertyName == p2.PropertyName;
        }

    }
}
