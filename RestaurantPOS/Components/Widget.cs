﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Components
{
    public enum categories { Food, Alcohol, ColdDrinks, HotDrinks, Desserts }

    public partial class Widget : UserControl
    {
        private categories _category;
        private double _cost;

        public event EventHandler OnSelect = null;
        public Widget()
        {
            InitializeComponent();
        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {
            OnSelect?.Invoke(this, e);
        }

        public categories Category { get => _category; set => _category = value; }

        public string Title { get => lblTitle.Text; set => lblTitle.Text = value; }
        public double Cost { get => _cost; set  { _cost = value; lblCost.Text = _cost.ToString("C2"); } }
        public Image Icon { get => imgImage.Image; set => imgImage.Image = value; }




    }
}
