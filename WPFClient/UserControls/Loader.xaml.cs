// ***********************************************************************
// Assembly         : WPFClient
// Author           : demo
// Created          : 05-25-2017
//
// Last Modified By : demo
// Last Modified On : 05-25-2017
// ***********************************************************************
// <copyright file="Loader.xaml.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFClient.UserControls
{
    /// <summary>
    /// Loader user control is used for loading animation when waiting for maze/opponent.s
    /// </summary>
    public partial class Loader : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the loader class.
        /// </summary>
        public Loader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The message property
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(Loader), new PropertyMetadata("Loading"));
    }
}
