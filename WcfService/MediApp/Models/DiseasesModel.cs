using EntityModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfControllers;

namespace MediApp.Models
{
    public class SurveyModel : IEnumerable
    {
        public string Question { get; set; }
        public bool Chosen { get; set; }

        private Question[] _question;

        public SurveyModel()
        {
            DbServices.PatientsContext db = new DbServices.PatientsContext(WcfConfig.WcfUri);
            const int size = 13;
            _question = new Question[size];
            for (int i = 0; i < size; i++)
            {
                _question[i] = new Question();
            }
            _question[0].Name = "Kichanie";
            _question[0].Content = "Czy kichasz znacznie częściej niż zdarzało Ci się wcześniej?";
            _question[1].Name = "Ból gardła";
            _question[1].Content = "Czy boli Cię gardło?";
            _question[2].Name = "Osłabienie";
            _question[2].Content = "Czy czujesz się osłabiony?";
            _question[3].Name = "Bóle kostno-stawowe";
            _question[3].Content = "Czy odczuwasz bóle kostno-stawowe?";
            _question[4].Name = "Poczucie rozbicia";
            _question[4].Content = "Czy czujesz się bardziej zmęczony i osłabiony niż zazwyczaj?";
            _question[5].Name = "Ból głowy";
            _question[5].Content = "Czy odczuwasz nasilony, długotrwały ból głowy?";
            _question[6].Name = "Dreszcze";
            _question[6].Content = "Czy masz dreszcze?";
            _question[7].Name = "Długotrwały katar";
            _question[7].Content = "Czy masz katar trwający dłużej niż 2 tygodnie?";
            _question[8].Name = "Suchy kaszel";
            _question[8].Content = "Czy masz suchy kaszel?";
            _question[9].Name = "Świszczący oddech";
            _question[9].Content = "Czy masz świszczący oddech?";
            _question[10].Name = "Napady duszności";
            _question[10].Content = "Czy masz napady duszności lub mocny ucisk w klatce piersiowej, w szczególności po wysiłku?";
            _question[11].Name = "Zaczerwienione gardło";
            _question[11].Content = "Czy masz zaczerwienione lub opuchnięte gardło?";
            _question[12].Name = "Spuchnięte gardło";
            _question[12].Content = "Czy masz spuchnięte gardło, problemy z przełykaniem i męczy Cię chrypka?";       
    }

        public SurveyModel(Question[] pArray)
        {
            _question = new Question[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                _question[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public QuestionsEnum GetEnumerator()
        {
            return new QuestionsEnum(_question);
        }

        public class QuestionsEnum : IEnumerator
        {
            public Question[] _question;
            int position = -1;

            public QuestionsEnum(Question[] list)
            {
                _question = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _question.Length);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }
            public Question Current
            {
                get
                {
                    try
                    {
                        return _question[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }

    public class DiseasesHistoryModel
    {
        public string Disease { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Medicine { get; set; }
    }
}