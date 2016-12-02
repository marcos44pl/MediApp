using EntityModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ComunicationControllers;

namespace MediApp.Models
{
    public class SurveyModel : IEnumerable
    {
       static DbServices.PatientsContext db = new DbServices.PatientsContext(WcfConfig.WcfUri);

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
                int i = 0;
                res = new List<Response>();
                foreach (DbServices.Question q in db.TableQuestion)
                {
                    res.Add(new Response { Id = i, Question = new Question { Id = i, Name = q.Name, Content = q.Content } });
                    i++;
                }
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