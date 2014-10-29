using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlexCore.general;

namespace FlexCore.persons
{
    public partial class EditField : UserControl
    {


        public EditField()
        {
            InitializeComponent();
        }

        public EditField(string pTitle, string pValue)
            :this()
        {
            if (pTitle == "")
            {
                itemTitle.Visible = false;
            }
            else 
            {
                itemTitle.Text = pTitle;
            }
            editValue.Text = pValue;
        }

        public string getEditValue()
        {
            return editValue.Text;
        }

        public string getTitle()
        {
            return itemTitle.Text;
        }
    }
}
