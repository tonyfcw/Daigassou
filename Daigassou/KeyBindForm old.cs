﻿using System;
using System.Windows.Forms;

namespace Daigassou
{
    public partial class KeyBindFormOld : Form
    {
        private const int NUMBER_OF_KEY = 37;

        private TextBox[] keyBoxs;

        public KeyBindFormOld()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            keyBoxs = new TextBox[NUMBER_OF_KEY]
            {
                textBox1,
                textBox2,
                textBox3,
                textBox4,
                textBox5,
                textBox6,
                textBox7,
                textBox8,
                textBox9,
                textBox10,
                textBox11,
                textBox12,
                textBox13,
                textBox14,
                textBox15,
                textBox16,
                textBox17,
                textBox18,
                textBox19,
                textBox20,
                textBox21,
                textBox22,
                textBox23,
                textBox24,
                textBox25,
                textBox26,
                textBox27,
                textBox28,
                textBox29,
                textBox30,
                textBox31,
                textBox32,
                textBox33,
                textBox34,
                textBox35,
                textBox36,
                textBox37
            };
        }


        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            var tmpBox = (TextBox) sender;
            if (tmpBox == null) throw new ArgumentNullException(nameof(tmpBox));
            tmpBox.Text = e.KeyCode.ToString();
            KeyBinding.SetKeyToNote_22(Array.IndexOf(keyBoxs, tmpBox) + 48, e.KeyCode);
        }

        private void KeyBindForm_Load(object sender, EventArgs e)
        {
            KeyBinding.LoadConfig();
            for (var i = 0; i < NUMBER_OF_KEY; i++) keyBoxs[i].Text = KeyBinding.GetNoteToKey(i + 48).ToString();
        }

        private void KeyBindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }


        private void btnApply_Click(object sender, EventArgs e)
        {
            KeyBinding.SaveConfig();
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这个按钮是美工加上去的，实际上我并不想加这个功能。");
        }
    }
}