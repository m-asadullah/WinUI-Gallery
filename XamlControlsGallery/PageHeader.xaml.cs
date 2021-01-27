//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using AppUIBasics.Helper;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using System;

namespace AppUIBasics
{
    public sealed partial class PageHeader : UserControl
    {
        public Action CopyLinkAction { get; set; }
        public Action ToggleThemeAction { get; set; }

        public TeachingTip TeachingTip1 => ToggleThemeTeachingTip1;
        public TeachingTip TeachingTip2 => ToggleThemeTeachingTip2;
        public TeachingTip TeachingTip3 => ToggleThemeTeachingTip3;

        public object Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(PageHeader), new PropertyMetadata(null));

        public double BackgroundColorOpacity
        {
            get { return (double)GetValue(BackgroundColorOpacityProperty); }
            set { SetValue(BackgroundColorOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundColorOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundColorOpacityProperty =
            DependencyProperty.Register("BackgroundColorOpacity", typeof(double), typeof(PageHeader), new PropertyMetadata(0.0));


        public double AcrylicOpacity
        {
            get { return (double)GetValue(AcrylicOpacityProperty); }
            set { SetValue(AcrylicOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundColorOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AcrylicOpacityProperty =
            DependencyProperty.Register("AcrylicOpacity", typeof(double), typeof(PageHeader), new PropertyMetadata(0.3));


        public CommandBar TopCommandBar
        {
            get { return topCommandBar; }
        }

        public UIElement TitlePanel
        {
            get { return pageTitle; }
        }

        public PageHeader()
        {
            this.InitializeComponent();
            this.InitializeDropShadow(ShadowHost, TitleTextBlock.GetAlphaMask());
            this.ResetCopyLinkButton();
        }

        public void UpdateBackground(bool isFilteredPage)
        {
            VisualStateManager.GoToState(this, isFilteredPage ? "FilteredPage" : "NonFilteredPage", false);
        }

        private void OnCopyLinkButtonClick(object sender, RoutedEventArgs e)
        {
            this.CopyLinkAction?.Invoke();

            if (ProtocolActivationClipboardHelper.ShowCopyLinkTeachingTip)
            {
                this.CopyLinkButtonTeachingTip.IsOpen = true;
            }

            this.CopyLinkButton.Label = "Copied to Clipboard";
            this.CopyLinkButtonIcon.Symbol = Symbol.Accept;
        }

        public void OnThemeButtonClick(object sender, RoutedEventArgs e)
        {
            ToggleThemeAction?.Invoke();
        }

        public void ResetCopyLinkButton()
        {
            this.CopyLinkButtonTeachingTip.IsOpen = false;
            this.CopyLinkButton.Label = "Generate Link to Page";
            this.CopyLinkButtonIcon.Symbol = Symbol.Link;
        }

        private void OnCopyDontShowAgainButtonClick(TeachingTip sender, object args)
        {
            ProtocolActivationClipboardHelper.ShowCopyLinkTeachingTip = false;
            this.CopyLinkButtonTeachingTip.IsOpen = false;
        }

        private void ToggleThemeTeachingTip2_ActionButtonClick(Microsoft.UI.Xaml.Controls.TeachingTip sender, object args)
        {
            NavigationRootPage.Current.PageHeader.ToggleThemeAction?.Invoke();
        }

        /// <summary>
        /// This method will be called when a <see cref="ItemPage"/> gets unloaded. 
        /// Put any code in here that should be done when a <see cref="ItemPage"/> gets unloaded.
        /// </summary>
        /// <param name="sender">The sender (the ItemPage)</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> of the ItemPage that was unloaded.</param>
        public void Event_ItemPage_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
