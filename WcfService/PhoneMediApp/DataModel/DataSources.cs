using EntityModels;
using PhoneMediApp.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using SharedModels;
// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app 
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
// is first launched.

namespace PhoneMediApp.Data
{

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class DataSources
    {
        private static DataSources _dataSource = new DataSources();
        private bool isLoaded = false;
        private bool isLoadedH = false;

        private ObservableCollection<LifeFuncMeasure> _measures = new ObservableCollection<LifeFuncMeasure>();
        private ObservableCollection<IllnessModel> _history = new ObservableCollection<IllnessModel>();

        public ObservableCollection<LifeFuncMeasure> Measures
        {
            get { return this._measures; }
        }
        public ObservableCollection<IllnessModel> History
        {
            get { return this._history; }
        }

        public static async Task<IEnumerable<LifeFuncMeasure>> GetMeasureAsync()
        {
            await _dataSource.GetMeasureDataAsync();

            return _dataSource.Measures;
        }
        public static async Task<IEnumerable<IllnessModel>> GetHistoryAsync()
        {
            await _dataSource.GetHistoryDataAsync();

            return _dataSource._history;
        }
        public static void AddMeasure(LifeFuncMeasure measure)
        {
           _dataSource.Measures.Add(measure);
            PatientController.AddMeasure(measure);
        }

        public static async Task<LifeFuncMeasure> GetItemAsync(int id)
        {
            await _dataSource.GetMeasureDataAsync();
            try
            {
                var measure = _dataSource.Measures.Where(l => l.Id == id).Single();
                return measure;
            }
            catch(Exception e)
            {

            }
            return null;
        }
        public static async Task<IllnessModel> GetIllnessAsync(DateTime id)
        {
            await _dataSource.GetMeasureDataAsync();
            try
            {
                var illnesse = _dataSource.History.Where(i => i.Date == id).Single();
                return illnesse;
            }
            catch (Exception e)
            {

            }
            return null;
        }
        private async Task GetHistoryDataAsync()
        {
            if (isLoadedH)
                return;

            string pesel = UserController.getUserPesel();
            string uriRequest = ComunicationControllers.WcfConfig.getPatient(pesel);
            var controller = new RestController<Patient>();
            var patient = await controller.getObjects(uriRequest);
            var results = await PatientController.GetIllnesses(patient.First().Id);
            foreach (var it in results)
            {
                History.Add(it);
            }
            isLoadedH = true;
        }
        private async Task GetMeasureDataAsync()
        {
            if (isLoaded)
                return;

            string pesel = UserController.getUserPesel();
            string uriRequest = ComunicationControllers.WcfConfig.getPatientMeasure(pesel);
            RestController<LifeFuncMeasure> controller = new RestController<LifeFuncMeasure>();
            List<LifeFuncMeasure> list = await controller.getObjects(uriRequest);

            foreach (var it in list)
            {
                Measures.Add(it);
            }
            isLoaded = true;
        }
    }
}