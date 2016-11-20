using EntityModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WcfControllers;

namespace MediApp.Models
{
    public class SurveyModel : IEnumerable
    {
        public const int surveySize = 13;

        
        //public Response[] Answer = new Response[surveySize];

        [Required]
        public Output _output;

        public SurveyModel()
        {
            DbServices.PatientsContext db = new DbServices.PatientsContext(WcfConfig.WcfUri);
            _output = new Output();
            _output.Answer = new Response[surveySize];

            for (int i = 0; i < surveySize; i++)
            {
                _output.Answer[i] = new Response();
                _output.Answer[i].Id = i;
                _output.Answer[i].Question = new Question();
                //Answer[i].Question = _output[i];
            }
           _output.Answer[0].Question.Name = "Kichanie";
           _output.Answer[0].Question.Content = "Czy kichasz znacznie częściej niż zdarzało Ci się wcześniej?";
           _output.Answer[1].Question.Name = "Ból gardła";
           _output.Answer[1].Question.Content = "Czy boli Cię gardło?";
           _output.Answer[2].Question.Name = "Osłabienie";
           _output.Answer[2].Question.Content = "Czy czujesz się osłabiony?";
           _output.Answer[3].Question.Name = "Bóle kostno-stawowe";
           _output.Answer[3].Question.Content = "Czy odczuwasz bóle kostno-stawowe?";
           _output.Answer[4].Question.Name = "Poczucie rozbicia";
           _output.Answer[4].Question.Content = "Czy czujesz się bardziej zmęczony i osłabiony niż zazwyczaj?";
           _output.Answer[5].Question.Name = "Ból głowy";
           _output.Answer[5].Question.Content = "Czy odczuwasz nasilony, długotrwały ból głowy?";
           _output.Answer[6].Question.Name = "Dreszcze";
           _output.Answer[6].Question.Content = "Czy masz dreszcze?";
           _output.Answer[7].Question.Name = "Długotrwały katar";
           _output.Answer[7].Question.Content = "Czy masz katar trwający dłużej niż 2 tygodnie?";
           _output.Answer[8].Question.Name = "Suchy kaszel";
           _output.Answer[8].Question.Content = "Czy masz suchy kaszel?";
           _output.Answer[9].Question.Name = "Świszczący oddech";
           _output.Answer[9].Question.Content = "Czy masz świszczący oddech?";
           _output.Answer[10].Question.Name = "Napady duszności";
           _output.Answer[10].Question.Content = "Czy masz napady duszności lub mocny ucisk w klatce piersiowej, w szczególności po wysiłku?";
           _output.Answer[11].Question.Name = "Zaczerwienione gardło";
           _output.Answer[11].Question.Content = "Czy masz zaczerwienione lub opuchnięte gardło?";
           _output.Answer[12].Question.Name = "Spuchnięte gardło";
           _output.Answer[12].Question.Content = "Czy masz spuchnięte gardło, problemy z przełykaniem i męczy Cię chrypka?";       

    }

        public SurveyModel(Response[] pArray)
        {
           _output.Answer = new Response[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                _output.Answer[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public AnswerEnum GetEnumerator()
        {
            return new AnswerEnum(_output.Answer);
        }

        public class AnswerEnum : IEnumerator
        {
            public Output _output = new Output();
            int position = -1;

            public AnswerEnum(Response[] list)
            {
                _output.Answer = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _output.Answer.Length);
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
            public Response Current
            {
                get
                {
                    try
                    {
                        return _output.Answer[position];
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