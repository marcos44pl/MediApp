using EntityModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediApp.Models
{
    public class SurveyModel : IEnumerable
    {
        public string Question { get; set; }
        public bool Chosen { get; set; }
        private Question[] _question;

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