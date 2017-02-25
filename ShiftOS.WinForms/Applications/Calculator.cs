/*
 * MIT License
 * 
 * Copyright (c) 2017 Michael VanOverbeek and ShiftOS devs
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShiftOS.Engine;

namespace ShiftOS.WinForms.Applications
{
    [Launcher("Calculator", true, "al_calculator", "Accessories")]
    [RequiresUpgrade("calculator")]
    [WinOpen("calculator")]
    [DefaultIcon("iconCalculator")]
    public partial class Calculator : UserControl, IShiftOSWindow
    {
        public bool justopened = false;
        private int activeoperation = 0;
        private float operationnumber = 0;
        private float currentnumber = 0;

        public Calculator()
        {
            InitializeComponent();
        }

        private void prepareButtons()
        {
            buttonEquals.Visible = ShiftoriumFrontend.UpgradeInstalled("calc_equals_button");
            buttonPlus.Visible = ShiftoriumFrontend.UpgradeInstalled("calc_plus_button");
            buttonMinus.Visible = ShiftoriumFrontend.UpgradeInstalled("calc_minus_button");
            buttonMultiply.Visible = ShiftoriumFrontend.UpgradeInstalled("calc_multiply_button");
        }

        public void OnLoad()
        {
            prepareButtons();
        }

        public void OnSkinLoad()
        {
            
        }

        public bool OnUnload()
        {
            return true;
        }

        public void OnUpgrade()
        {
            prepareButtons();
        }

        private void numBox_TextChanged(object sender, EventArgs e)
        {
            currentnumber = float.Parse(numBox.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numBox.Text = numBox.Text + "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            numBox.Text = numBox.Text + "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            numBox.Text = numBox.Text + "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            numBox.Text = numBox.Text + "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            numBox.Text = numBox.Text + "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            numBox.Text = numBox.Text + "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            numBox.Text = numBox.Text + "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            numBox.Text = numBox.Text + "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            numBox.Text = numBox.Text + "9";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            numBox.Text = numBox.Text + "0";
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            if (operationnumber == 0 && activeoperation != 1)
            {
                operationnumber = currentnumber;
                activeoperation = 1;
            } else
            {

            }
        }
    }
}
