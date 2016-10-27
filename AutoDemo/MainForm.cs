// Pawel Lal
// Natalia Kowalik
// Martyna Łuczkowska
// Marta Mazur

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mommosoft.ExpertSystem;
using System.Diagnostics;

namespace AutoDemo {
    public partial class MainForm : Form {
        private int yPos;
        private bool termometr = false;
        private bool goraczka = false;
        private Mommosoft.ExpertSystem.Environment _theEnv = new Mommosoft.ExpertSystem.Environment();
        public MainForm() {

            ConsoleTraceListener tlex = new ConsoleTraceListener();
             InitializeComponent();
             _theEnv.AddRouter(new DebugRouter());
            _theEnv.Load("autodemo.clp");
            _theEnv.Reset();
            
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            NextUIState();
        }

        private void SetQuestion(string text)
        {
            label1.Visible = true;
            label1.Text = text;
            label1.Location = new Point(122, 110);
            label1.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(label1);
        }

        private void OnClickButton(object sender, EventArgs e) {
            Button button = sender as Button;
            // Get the state-list.
            String evalStr = "(find-all-facts ((?f state-list)) TRUE)";
            using (FactAddressValue f = (FactAddressValue)((MultifieldValue)_theEnv.Eval(evalStr))[0]) {
                string currentID = f.GetFactSlot("current").ToString();

                if (button.Tag.Equals("Next"))
                {
                    if (GetCheckedChoiceButton() == null)
                    {
                        if (termometr)
                        {
                            if (goraczka)
                            {
                                _theEnv.AssertString("(next " + currentID + " " +
                                           "Yes" + ")");
                            }
                            else
                            {
                                _theEnv.AssertString("(next " + currentID + " " +
                                           "No" + ")");
                            }
                            termometr = false;
                        }
                        else
                            _theEnv.AssertString("(next " + currentID + ")");
                    }
                    else
                    {
                        switch ((string)GetCheckedChoiceButton().Tag)
                        {
                            case "Tak":
                                _theEnv.AssertString("(next " + currentID + " " +
                                           "Yes" + ")");
                                break;
                            case "Nie":
                                _theEnv.AssertString("(next " + currentID + " " +
                                           "No" + ")");
                                break;
                        }
                        
                    }

                    NextUIState();
                }
                else if (button.Tag.Equals("Restart"))
                {
                    _theEnv.Reset();
                    NextUIState();
                } else if (button.Tag.Equals("Prev")) {
                    _theEnv.AssertString("(prev " + currentID + ")");
                    NextUIState();
                }
            }
        }

        private void NextUIState() {
            messageLabel.Visible = false;
            nextButton.Visible = false;
            prevButton.Visible = false;
            label1.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            choicesPanel.Controls.Clear();
            _theEnv.Run();
           
            // Get the state-list.
            String evalStr = "(find-all-facts ((?f state-list)) TRUE)";
            using (FactAddressValue allFacts = (FactAddressValue)((MultifieldValue)_theEnv.Eval(evalStr))[0]) {
                string currentID = allFacts.GetFactSlot("current").ToString();
                evalStr = "(find-all-facts ((?f UI-state)) " +
                               "(eq ?f:id " + currentID + "))";
            }

            using (FactAddressValue evalFact = (FactAddressValue)((MultifieldValue)_theEnv.Eval(evalStr))[0]) {
                string state = evalFact.GetFactSlot("state").ToString();
                if (state.Equals("initial")) {
                    nextButton.Visible = true;
                    nextButton.Tag = "Next";
                    nextButton.Text = "Next";
                    prevButton.Visible = false;
                } else if (state.Equals("final")) {
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    prevButton.Visible = false;
                } else {
                    nextButton.Visible = true;
                    nextButton.Tag = "Next";
                    prevButton.Tag = "Prev";
                    prevButton.Visible = true;
                }

                messageLabel.Visible = true;
                messageLabel.Location = new Point(12, 9);
                messageLabel.Text = GetString((SymbolValue)evalFact.GetFactSlot("display"));
                Controls.Add(messageLabel);

                using (MultifieldValue validAnswers = (MultifieldValue)evalFact.GetFactSlot("valid-answers")) {
                    String selected = evalFact.GetFactSlot("response").ToString();
                    String question = evalFact.GetFactSlot("display").ToString();
                    switch (question)
                    {
                        case "WelcomeMessage":                          
                            SetQuestion("Program ma charakter wyłącznie \ninformacyjno-edukacyjny i wynik testu \nnie może być traktowany jak porada, \nkonsultacja lub diagnoza lekarza.");                            
                            break;
                        case "HighTemperatureQuestion":
                            label1.Visible = true;
                            label1.Text = "Korzystając z termometru wybierz aktualną temperaturę swojego ciała";
                            label1.Location = new Point(12, 47);
                            Controls.Add(label1);
                            pictureBox2.Image = new Bitmap("pasek.jpg");
                            pictureBox2.Location = new Point(216, 86);
                            pictureBox2.Width = 11;
                            pictureBox2.Height = 105;
                            pictureBox2.Visible = true;
                            Controls.Add(pictureBox2);
                            pictureBox1.Image = new Bitmap("termometrmaly.jpg");
                            pictureBox1.Location = new Point(102, 36);
                            pictureBox1.Width = 225;
                            pictureBox1.Height = 235;
                            pictureBox1.Visible = true;
                            Controls.Add(pictureBox1);
                            termometr = true; // zdefiniuj inny sposob przekazania wartosci formularza
                            break;
                        case "KichanieQuestion":
                            SetQuestion("Zaznacz 'Tak', jeśli kichasz znacznie \nczęściej niż zdarzało Ci się to dotychczas \n(kilka-kilkanaście razy dziennie).");
                            break;
                        case "PoczucieRozbiciaQuestion":
                            SetQuestion("Zaznacz 'Tak', jeśli czujesz się bardziej \nzmęczony i osłabiony niż zazwyczaj.");
                            break;
                        case "BoleKostnoStawoweQuestion":
                            SetQuestion("Zaznacz 'Tak', jeśli ból występuje i \nw ostatnim czasie nie wykonywałeś \nczynności wymagających dużego wysiłku \nfizycznego lub czujesz sztywność \nstawów po dłuzszym bezruchu.");
                            break;
                        case "BolGlowyQuestion":
                            SetQuestion("Zaznacz 'Tak', jeśli ból nie występował \nprzedtem, nasilił się w ostatnim czasie.");
                            break;
                        case "DreszczeQuestion":
                            SetQuestion("Zaznacz 'Tak', jeśli odczuwasz nieprzyjemny \nchłód oraz drżenie ciała pomimo \ntemperatury pokojowej pomieszczenia, \nw którym się znajdujesz.");
                            break;
                        case "DlugotrwalyKatarQuestion":
                            SetQuestion("Zaznacz 'Tak', jeśli katar trwa \ndłuzej niż 2 tygodnie.");
                            break;
                        case "DryCoughQuestion":
                            SetQuestion("Zaznacz 'Tak, jeśli podczas kaszlenia masz \nuczucie drażnienia w tchawicy, masz duszące \nataki kaszlu lub ustawicznie pokasłujesz. \nKaszel suchy jest męczący, brzmi jak \nposzczekiwanie i nie powoduje \nodkrztuszania wydzieliny.");
                            break;
                        case "SwiszczacyOddechQuestion":
                            SetQuestion("Zaznacz 'Tak' jeśli wydechowi towarzyszy \nciągły, wysoki dzwięk przypominający świst.");
                            break;
                        case "SuchyKaszelQuestion":
                            SetQuestion("Zaznacz 'Tak', jeśli masz napady dusznosci, \nczujesz ucisk w klatce piersiowej, a \nobjawy nasilaja się po wysilku fizycznym.");
                            break;
                        case "RozpulchnienieQuestion":
                            SetQuestion("Zaznacz 'Tak', jeśli z trudem mówisz, nie \nmożesz przełknąć śliny, męczy Cię chrypka. \nOpuchnięte gardło często jest przyczyną \ndotkliwego bólu podczas przełykania.");
                            break;
                        case "ZaczerwienienieQuestion":
                            SetQuestion("Zaznacz 'Tak' jeśli zauważyłeś/aś, że \nmasz przekrwioną błonę śluzową gardła \noraz migdałki. \nNierzadko są one również opuchnięte.");
                            break;
                    }
                    if (!termometr) // jesli pytanie nie dotyczylo temperatury - dodaj przyciski yes/no
                    {
                        int[] coord = { 3, 26 };
                        for (int i = 0; i < validAnswers.Count; i++)
                        {
                            RadioButton rb = new RadioButton();
                            switch ((SymbolValue)validAnswers[i]) {
                                case "No":
                                    rb.Text = "Nie";
                                    break;
                                case "Yes":
                                    rb.Text = "Tak";
                                    break;
                            }
                            rb.Tag = rb.Text;
                            rb.Visible = true;
                            rb.Location = new Point(3, coord[i]);
                            if (i == 0)
                                rb.Checked = true;
                            choicesPanel.Controls.Add(rb);
                        }
                    }

                }
                

            }
        }

        private void ShowChoices(bool visible) {
            foreach (Control control in choicesPanel.Controls) {
                control.Visible = visible;
            }
        }

        private RadioButton GetCheckedChoiceButton()
        {

            foreach (Control control in choicesPanel.Controls)
            {
                if (control is RadioButton)
                {
                    if (((RadioButton)control).Checked)
                    {
                        return (RadioButton)control;
                    }
                }
            }
            return null;
        }

        private string GetString(string name)
        {
            return SR.ResourceManager.GetString(name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (choicesPanel.Visible)
                choicesPanel.Visible = false;
            else
                choicesPanel.Visible = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                yPos = e.Y;
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            PictureBox p = pictureBox2;

            if (p != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    int wspolrzedna = e.Y;
                    wspolrzedna -= 50;
                    if (wspolrzedna < 105 && wspolrzedna > 0)
                    {
                        p.Height = wspolrzedna;
                        if (wspolrzedna <= 61)
                        {
                            goraczka = true;
                        }
                        else
                        {
                            goraczka = false;
                        }
                    }
                }
            }

        }



    }
}
