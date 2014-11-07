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

namespace FlexCore.closures
{
    public partial class Closure : UserControl, IObservable<EventDTO>
    {
        protected List<IObserver<EventDTO>> _observers;
        private int _itemID;

        public static readonly string PHYSICAL_PERSON = "Persona física";
        public static readonly string JURIDICAL_PERSON = "Persona jurídica";
        public static readonly string PHYSICAL_CLIENT = "Cliente físico";
        public static readonly string JURIDICAL_CLIENT = "Cliente jurídico";

        public Closure()
        {
            InitializeComponent();
        }

        public Closure(int pDay, int pMonth, int pYear, int pHour, int pMinutes, int pSeconds, string pState)
            :this(pDay+"/"+pMonth+"/"+pYear, pHour+":"+pMinutes+":"+pSeconds, pState)
        {

        }

        public Closure(string pDate, string pHour, string pState)
            :this()
        {
            dateText.Text = pDate;
            stateText.Text = pState;
            timeText.Text = pHour;
        }

        public int getPersonID() { return _itemID; }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Closure_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void Closure_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(245, 245, 245);
        }

        private void Closure_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        public IDisposable Subscribe(IObserver<EventDTO> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<EventDTO>(_observers, observer);
        }

        private void Person_Load(object sender, EventArgs e)
        {

        }

    }
}
