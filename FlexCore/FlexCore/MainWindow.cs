﻿using FlexCore.persons;
using FlexCore.closures;
using FlexCore.clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexCore
{
    public partial class MainWindow : Form
    {

        WelcomePane _welcomePane;
        UserControl _actualConent;

        public MainWindow()
        {
            InitializeComponent();
            _welcomePane = new WelcomePane();
            _welcomePane.Name = "welcomePane";
            setConentPanel(_welcomePane);
            setResizeable(false);
            this.Height = 540;
            this.CenterToScreen();
        }

        private void setConentPanel(UserControl pPanel)
        {
            _actualConent = pPanel;
            panelContent.Controls.Clear();
            pPanel.Dock = Dock = System.Windows.Forms.DockStyle.Top;
            pPanel.Location = new System.Drawing.Point(0, 37);
            panelContent.Controls.Add(pPanel);
        }

        private void setResizeable(bool pValue)
        {
            if (pValue)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.MaximizeBox = true;
                this.MinimizeBox = true;
                this.AutoSize = true;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.AutoSize = false;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void personsMenu_Click(object sender, EventArgs e)
        {
            PersonsContainer persons = new PersonsContainer();
            persons.Name = "personsMain";
            setConentPanel(persons);
            setResizeable(true);
        }

        private void mainItem_Click(object sender, EventArgs e)
        {
            setConentPanel(_welcomePane);
            this.Height = 540;
            if (this.WindowState != FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Normal;
                this.CenterToScreen();
            }
            setResizeable(false);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            ClosureMain closures = new ClosureMain();
            setConentPanel(closures);
            setResizeable(true);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            List<string> dbVersions = Settings.getDataBaseVersions();
            foreach (var version in dbVersions)
            {
                versionComboBox.Items.Add(version);
            }
            versionComboBox.SelectedItem = Settings.getCurrentVersion();
        }

        private void versionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedVersion = versionComboBox.SelectedItem.ToString();
            Settings.setCurrentDatabase(selectedVersion);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ClientsContainer clients = new ClientsContainer();
            setConentPanel(clients);
            setResizeable(true);
        }
    }
}
