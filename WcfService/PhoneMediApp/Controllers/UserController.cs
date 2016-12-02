using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;
using ComunicationControllers;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.Storage;

namespace PhoneMediApp.Controllers
{
    class UserController
    {
        static public async Task<bool> authenticateUser(string email,string password)
        {
            RestController<User> controller = new RestController<User>();
            var list = await controller.getObjects(WcfConfig.getUserUrl(email));

            if (list.Count == 0)
                return false;

            User user = list.First();
            var pass = user.Pass;
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(password, BinaryStringEncoding.Utf8);
            HashAlgorithmProvider hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            IBuffer shaBuff = hap.HashData(buff);
            IBuffer passDb = CryptographicBuffer.CreateFromByteArray(pass);
            bool result = CryptographicBuffer.Compare(passDb, shaBuff);

            if(result)
            {
                logUser(user);
            }

            return result;
        }

        static public async Task<bool> createUser(User user, string password)
        {
            RestController<User> userController = new RestController<User>();
             var list = await userController.getObjects(WcfConfig.getUserUrl(user.Email));

            if (list.Count != 0)
                return false;

            RestController<Role> roleController = new RestController<Role>();
            RestController<Patient> patientController = new RestController<Patient> ();

            List<Role> roles = await roleController.getObjects(WcfConfig.getRole(RolesKind.PATIENT));
            user.Roles = roles;

            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(password, BinaryStringEncoding.Utf8);
            HashAlgorithmProvider hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            IBuffer shaBuff = hap.HashData(buff);
            byte[] pass;
            CryptographicBuffer.CopyToByteArray(shaBuff, out pass);
            user.Pass = pass;
            await userController.insertObject(user, WcfConfig.TableUser);
            await patientController.insertObject(new Patient { Pesel = user.Pesel }, WcfConfig.TablePatient);
            return true;
        }
        static public void logUser(User user)
        {
           ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["name"]     = user.FstName;
            localSettings.Values["surname"]  = user.Surname;
            localSettings.Values["email"]    = user.Email;
            localSettings.Values["pesel"]    = user.Pesel;
            localSettings.Values["islogged"] = true;
        }
        static public void logoffUser()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["islogged"] = false;
        }
        static public bool isUserLogged()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            bool? value = localSettings.Values["islogged"] as bool?;

            if (value == null || (value == false))
                return false;

            return true;
        }
        static public string getUserPesel()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string pesel = localSettings.Values["pesel"] as string;
            return pesel;
        }

    }
}
