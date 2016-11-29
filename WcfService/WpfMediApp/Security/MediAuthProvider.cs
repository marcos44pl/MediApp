using System;
using System.Linq;

namespace MediApp.Security
{

    public class MediAuthProvider : AuthProvider
    {
        private static string[] _operations;

        public MediAuthProvider(object[] parameters)
        {
            _operations = parameters.Cast<string>().ToArray();
        }


        public override bool CheckAccess(string operation)
        {
            if (String.IsNullOrEmpty(operation))
                return false;

            if (_operations != null && _operations.Length > 0)
            {
                // Match the requested operation with the ACL
                return _operations.Any(p => p.ToUpperInvariant() ==
                        operation.ToUpperInvariant());
            }
            return false;
        }


        public override bool CheckAccess(object commandParameter)
        {
            string operation = Convert.ToString(commandParameter);

            return CheckAccess(operation);
        }
    }

    public abstract class AuthProvider
    {
        private static AuthProvider _instance;

        /// <summary>
        /// This method determines whether the user is authorize to perform 
        /// the requested operation
        /// </summary>
        public abstract bool CheckAccess(string operation);

        public abstract bool CheckAccess(object commandParameter);

        public static void Initialize<TProvider>() where TProvider : AuthProvider, new()
        {
            _instance = new TProvider();
        }

        public static void Initialize<TProvider>(object[] parameters)
        {
            _instance = (AuthProvider)typeof(TProvider).GetConstructor(new Type[]
                { typeof(object[]) }).Invoke(new object[] { parameters });
        }

        public static AuthProvider Instance
        {
            get { return _instance; }
        }
    }
}