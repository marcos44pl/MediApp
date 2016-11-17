using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;
using WcfControllers;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
namespace PhoneMediApp.WcfRestControllers
{
    class UserController
    {
        static public async Task<bool> authenticateUser(string email,string password)
        {
            WcfRestController<User> controller = new WcfRestController<User>();
            var list = await controller.getObjects(WcfConfig.getUserUrl(email));

            if (list.Count == 0)
                return false;

            var pass = list.First().Pass;
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(password, BinaryStringEncoding.Utf8);
            HashAlgorithmProvider hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            IBuffer shaBuff = hap.HashData(buff);
            IBuffer passDb = CryptographicBuffer.CreateFromByteArray(pass);
            return CryptographicBuffer.Compare(passDb, shaBuff);
        }

        static public async Task<bool> createUser(User user, string password)
        {
            WcfRestController<User> controller = new WcfRestController<User>();
             var list = await controller.getObjects(WcfConfig.getUserUrl(user.Email));

            if (list.Count != 0)
                return false;

            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(password, BinaryStringEncoding.Utf8);
            HashAlgorithmProvider hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            IBuffer shaBuff = hap.HashData(buff);
            byte[] pass;
            CryptographicBuffer.CopyToByteArray(shaBuff, out pass);
            user.Pass = pass;
            await controller.insertObject(user, WcfConfig.TableUser);
            return true;
        }
    }
}
