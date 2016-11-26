using EntityModels;
using System;
using System.Collections;
using System.Collections.Generic;
using WcfControllers;

namespace MediApp.Models
{
    public class MedicPartialModel : IEnumerable
    {
        public bool IsMedic { get; set; }
        public List<Patient> patients { get; set; }

        public MedicPartialModel()
        { }

        public MedicPartialModel(List<Patient> pArray)
        {
            patients = new List<Patient>();
            patients = pArray;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PatientsEnum GetEnumerator()
        {
            return new PatientsEnum();
        }

        public class PatientsEnum : IEnumerator
        {
            public List <Patient> _patients;
            int position = -1;

            public PatientsEnum(List<Patient> list)
            {
                _patients = list;
            }

            public PatientsEnum()
            {
                _patients = new List<Patient>();
                Patient patient = new Patient { Height=155, Pesel = "98456711229", Sex="kobieta" };
                Patient patient2 = new Patient { Height = 183, Pesel = "98456700221", Sex = "mężczyzna" };
                _patients.Add(patient);
                _patients.Add(patient2);
            }

            public bool MoveNext()
            {
                position++;
                return (position < _patients.Count);
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

            public Patient Current
            {
                get
                {
                    try
                    {
                        return _patients[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}