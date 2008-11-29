/***************************************************************************
 *
 * $Author: Turley
 * 
 * "THE BEER-WARE LICENSE"
 * As long as you retain this notice you can do whatever you want with 
 * this stuff. If we meet some day, and you think this stuff is worth it,
 * you can buy me a beer in return.
 *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Controls
{
    public partial class ItemSearch : Form
    {
        private static bool alt;
        public ItemSearch(bool alternative)
        {
            InitializeComponent();
            alt = alternative;
        }

        private void Search_Graphic(object sender, EventArgs e)
        {
            int graphic;
            string convert;
            bool candone;
            if (textBoxGraphic.Text.Contains("0x"))
            {
                convert = textBoxGraphic.Text.Replace("0x", "");
                candone = int.TryParse(convert, System.Globalization.NumberStyles.HexNumber, null, out graphic);
            }
            else
                candone = int.TryParse(textBoxGraphic.Text, System.Globalization.NumberStyles.Integer, null, out graphic);
                
            if (candone)
            {
                bool res;
                if (alt)
                    res = ItemShowAlternative.SearchGraphic(graphic);
                else
                    res = ItemShow.SearchGraphic(graphic);
                if (!res)
                {
                    DialogResult result = MessageBox.Show("No item found","Result",MessageBoxButtons.OKCancel);
                    if (result == DialogResult.Cancel)
                        Close();
                }
            }

        }

        private void Search_ItemName(object sender, EventArgs e)
        {
            bool res;
            if (alt)
                res = ItemShowAlternative.SearchName(textBoxItemName.Text);
            else
                res = ItemShow.SearchName(textBoxItemName.Text);
            if (!res)
            {
                DialogResult result = MessageBox.Show("No item found", "Result", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                    Close();
            }
        }
    }
}