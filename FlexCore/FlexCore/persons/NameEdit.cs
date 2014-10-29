using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexCore.persons
{
    public partial class NameEdit : Form
    {

        public static readonly int PHYSICAL = 0;
        public static readonly int JURIDICAL = 1;

        private int _personID;
        private int _type;
        private EditField _name;
        private EditField _lastName1;
        private EditField _lastName2;

        public NameEdit()
        {
            InitializeComponent();
        }

        public NameEdit(int pPersonID, int pType, string pName, string pLastName1=null, string pLastName2=null)
            :this()
        {
            _personID = pPersonID;
            _type = pType;
            _name = new EditField("Nombre:", pName);
            _name.Name = "name";
            itemList.Controls.Add(_name);
            if (pType == PHYSICAL)
            {
                initializePhysical(pLastName1, pLastName2);
            }
        }

        private void initializePhysical(string pLastName1, string pLastName2)
        {
            _lastName1 = new EditField("Primer apellido:", pLastName1);
            _lastName2 = new EditField("Segundo apellido:", pLastName2);
            _lastName1.Name = "firstLastName";
            _lastName2.Name = "secondLastName";
            itemList.Controls.Add(_lastName1);
            itemList.Controls.Add(_lastName2);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
