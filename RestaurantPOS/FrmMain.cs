using RestaurantPOS.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace RestaurantPOS
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Opacity = 1;
            docker.WindowState = Bunifu.UI.WinForms.BunifuFormDock.FormWindowStates.Maximized;
        }



        public void AddItem(string name, double cost, categories category, string icon)
        {
            var w = new Widget()
            {
                Title = name,
                Cost = cost,
                Category = category,
                Icon = Image.FromFile("icons/" + icon),
                Tag = category
            };
            pnl.Controls.Add(w);

            w.OnSelect += (ss, ee) =>
            {
                var wdg = (Widget)ss;
                foreach (DataGridViewRow item in grid.Rows)
                {
                    if (item.Cells[0].Value.ToString() == wdg.lblTitle.Text)
                    {
                        int quantity = int.Parse(item.Cells[1].Value.ToString()) + 1;
                        item.Cells[1].Value = quantity;

                        string costText = wdg.lblCost.Text.Replace("$", "").Trim();

                        // Remove all non-numeric characters except the decimal point
                        costText = Regex.Replace(costText, @"[^\d.]", "");

                   

                        if (double.TryParse(costText, NumberStyles.Any, CultureInfo.InvariantCulture, out double itemCost))
                        {
                            item.Cells[2].Value = (quantity * itemCost).ToString("C2", CultureInfo.InvariantCulture);
                         
                        }
                        else
                        {
                            MessageBox.Show($"Failed to parse value: '{costText}'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
                        }

                        CalculateTotal();
                        return;
                    }
                }

                grid.Rows.Add(new object[] { wdg.lblTitle.Text, 1, wdg.lblCost.Text });
                CalculateTotal();
            };
        }







        void CalculateTotal()
{
    double tot = 0;
    foreach (DataGridViewRow item in grid.Rows)
    {
        string costText = item.Cells[2].Value.ToString().Replace("₹", "").Trim(); // Replace the rupee symbol if it exists

        // Remove all non-numeric characters except the decimal point
        costText = Regex.Replace(costText, @"[^\d.]", "");



        if (double.TryParse(costText, NumberStyles.Any, CultureInfo.InvariantCulture, out double itemCost))
        {
            tot += itemCost;
        }
        else
        {
            // Display the problematic string
            MessageBox.Show($"Failed to parse value: '{costText}'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      
        }
    }

    lblTot.Text = tot.ToString("C2", new CultureInfo("en-IN"));

}






    private void FrmMain_Shown(object sender, EventArgs e)
        {

            //Fectch items from db here
            //food
            AddItem("Original Burger", 7.75, categories.Food, "burger.png");
            AddItem("Ranch Burger", 7.75, categories.Food, "burger.png");
            AddItem("Pizza", 10.78, categories.Food, "pizza.png");
            AddItem("Cheicken", 6.78, categories.Food, "chicken.png");
            AddItem("Fries", 5.78, categories.Food, "fries.png");
            AddItem("Chichen WIngs", 1.78, categories.Food, "wings.png");

            //alcohol
            AddItem("Beer", 10.78, categories.Alcohol, "beer.png");
            AddItem("Vodka", 11.78, categories.Alcohol, "drinks.png");
            AddItem("Whiskey", 51.78, categories.Alcohol, "drinks.png");
            AddItem("Brandy", 55.78, categories.Alcohol, "drinks.png");
            AddItem("Vermouth", 10.78, categories.Alcohol, "drinks.png");
            AddItem("Cognac", 9.78, categories.Alcohol, "drinks.png");
            AddItem("wine", 8.78, categories.Alcohol, "drinks.png");
            AddItem("Rum", 8.78, categories.Alcohol, "drinks.png");

            //cold drinks
            AddItem("Coca-Cola", 1.78, categories.ColdDrinks, "juice.png");
            AddItem("Pepsi", 1.78, categories.ColdDrinks, "juice.png");
            AddItem("Diet Coke", 1.78, categories.ColdDrinks, "juice.png");
            AddItem("Dr Pepper", 1.78, categories.ColdDrinks, "juice.png");
            AddItem("Mountain Dew", 1.78, categories.ColdDrinks, "juice.png");
            AddItem("Sprite", 1.78, categories.ColdDrinks, "juice.png");
            AddItem("Diet Pepsi", 1.78, categories.ColdDrinks, "juice.png");
            AddItem("Coke Zero", 1.78, categories.ColdDrinks, "juice.png");
            AddItem("Fresh Juice", 1.78, categories.ColdDrinks, "juice.png");
            AddItem("Water", 1.78, categories.ColdDrinks, "juice.png");

            //hot drinks
            AddItem("Fresh ginger tea", 1.78, categories.HotDrinks, "tea.png");
            AddItem("Fruit tea", 1.78, categories.HotDrinks, "tea.png");
            AddItem("Fresh mint tea", 1.78, categories.HotDrinks, "tea.png");
            AddItem("Coffee", 1.78, categories.HotDrinks, "tea.png");
            AddItem("Hot chocolate", 1.78, categories.HotDrinks, "tea.png");
            AddItem("Hot lemon", 1.78, categories.HotDrinks, "tea.png");
            AddItem("Green tea", 1.78, categories.HotDrinks, "tea.png");
            AddItem("", 1.78, categories.HotDrinks, "tea.png");
            AddItem("Chai", 1.78, categories.HotDrinks, "tea.png");

            //desserts
            AddItem("Pasteis de Nata", 1.78, categories.Desserts, "dessert.png");
            AddItem("Tiramisu ", 1.78, categories.Desserts, "dessert.png");
            AddItem("Gulab Jamun", 1.78, categories.Desserts, "dessert.png");
            AddItem("S'mores", 1.78, categories.Desserts, "dessert.png");
            AddItem("Churros ", 1.78, categories.Desserts, "dessert.png");
            AddItem("Lamingtons ", 1.78, categories.Desserts, "dessert.png");

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        { }
        string path = "All Items";
        private void menu_OnItemSelected(object sender, string _path, EventArgs e)
        {
            path = _path;
            foreach (var item in pnl.Controls)
            {
                var wdg = (Widget)item;
                wdg.Visible = wdg.lblTitle.Text.ToLower().Contains(txtSearch.Text.Trim().ToLower()) 
                    &&
                    (wdg.Tag.ToString() == path.Replace(" ","") || path.Replace(" ", "") == "AllItems");
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || txtSearch.Text.Trim().Length == 0)
            {
                foreach (var item in pnl.Controls)
                {
                    var wdg = (Widget)item;
                    wdg.Visible = wdg.lblTitle.Text.ToLower().Contains(txtSearch.Text.Trim().ToLower()) 
                        &&
                      ( wdg.Tag.ToString() == path.Replace(" ", "") || path.Replace(" ", "") == "AllItems");
                        
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Clear all rows in the DataGridView
            grid.Rows.Clear();

            // Reset the total label
            lblTot.Text = "₹0.00";

        }

    }
}
