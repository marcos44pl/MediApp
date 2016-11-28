﻿using System;
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
using System.Windows.Shapes;

namespace WpfMediApp
{
    /// <summary>
    /// Interaction logic for addingIllness.xaml
    /// </summary>
    public partial class addingIllness : Window
    {
        public addingIllness()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (TBIllness.Text.CompareTo("") != 0 && datePicker1.Text.CompareTo("") != 0)
            {
                if (illnesses.myIllnesses == null)
                {
                    illnesses.myIllnesses = new List<Illness>();
                }
                illnesses.myIllnesses.Add(new Illness(TBIllness.Text, datePicker1.DisplayDate));
                Close();
            }
        }
    }
}
