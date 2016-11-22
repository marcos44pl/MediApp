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
              
        [Required]
        public List<Response> responses { get; set; }

        public SurveyModel()
        {    }

        public SurveyModel(List<Response> pArray)
        {
           responses = new List<Response>();
           responses = pArray;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public AnswerEnum GetEnumerator()
        {
            return new AnswerEnum();
        }

        public class AnswerEnum : IEnumerator
        {
            public List<Response> res;
            int position = -1;

            public AnswerEnum()
            {
                res = new List<Response>();
                Response r = new Response
                {
                    Id = 0,
                    Question = new Question { Name = "Kichanie", Content = "Czy kichasz znacznie częściej niż zdarzało Ci się poprzednio?" }
                };
                Response r1 = new Response
                {
                    Id = 1,
                    Question = new Question { Name = "Ból gardła", Content = "Czy boli Cię gardło?" }
                };
                Response r2 = new Response
                {
                    Id = 2,
                    Question = new Question { Name = "Osłabienie", Content = "Czy czujesz się osłabiony?" }
                };
                Response r3 = new Response
                {
                    Id = 3, 
                    Question = new Question { Name = "Bóle kostno-stawowe", Content = "Czy odczuwasz bóle kostno-stawowe?" }
                };
                Response r4 = new Response
                { 
                    Id = 4,
                    Question = new Question { Name = "Poczucie rozbicia", Content = "Czy czujesz się bardziej zmęczony i osłabiony niż zazwyczaj?" }
                };
                Response r5 = new Response
                {
                    Id = 5,
                    Question = new Question { Name = "Ból głowy", Content = "Czy odczuwasz nasilony, długotrwały ból głowy?" }
                };
                Response r6 = new Response
                {
                    Id = 6,
                    Question = new Question { Name = "Dreszcze", Content = "Czy masz dreszcze?" }
                };
                Response r7 = new Response
                {
                    Id = 7,
                    Question = new Question { Name = "Długotrwały katar", Content = "Czy masz katar trwający dłużej niż 2 tygodnie?" }
                };
                Response r8 = new Response
                {
                    Id = 8,
                    Question = new Question { Name = "Suchy kaszel", Content = "Czy masz suchy kaszel?" }
                };
                Response r9 = new Response
                {
                    Id = 9,
                    Question = new Question { Name = "Świszczący oddech", Content = "Czy masz świszczący oddech?" }
                };
                Response r10 = new Response
                {
                    Id = 10,
                    Question = new Question { Name = "Zaczerwienione gardło", Content = "Czy masz zaczerwienione lub opuchnięte gardło?" }
                };
                Response r11 = new Response
                {
                    Id = 11,
                    Question = new Question { Name = "Spuchnięte gardło", Content = "Czy masz spuchnięte gardło, problemy z przełykaniemm i męczy Cię chrypka?" }
                };
                Response r12 = new Response
                {
                    Id = 12,
                    Question = new Question { Name = "Napady duszności", Content = "Czy masz napady duszności lub mocny ucisk w klatce piersiowej, w szczególności po wysiłku?" }
                };
                res.Add(r);
                res.Add(r1);
                res.Add(r2);
                res.Add(r3);
                res.Add(r4);
                res.Add(r5);
                res.Add(r6);
                res.Add(r7);
                res.Add(r8);
                res.Add(r9);
                res.Add(r10);
                res.Add(r11);
                res.Add(r12);
            }

            public AnswerEnum(List<Response> list)
            {
                res = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < res.Count);
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
                        return res[position];
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