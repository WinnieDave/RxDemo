using GalaSoft.MvvmLight;
using RxDemo.Model;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Command;

namespace RxDemo.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private ObservableCollection<string> _urls=new ObservableCollection<string>();
        private string _url;
        private ObservableCollection<Response> _responses=new ObservableCollection<Response>();
        private IDisposable _onNextSubscription;
        private IDisposable _OnCompletedSubscription;
        public ObservableCollection<string> Urls
        {
            get { return _urls; }
            set { Set(ref _urls, value); }
        }
        public ObservableCollection<Response> Responses
        {
            get { return _responses; }
            set { Set(ref _responses, value); }
        }

        public string URL
        {
            get { return _url; }
            set { Set(ref _url, value); }
        }
        public RelayCommand StartRequestsCommand { get; set; }
        public RelayCommand AddUrlCommand { get; set; }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public  MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            AddUrlCommand = new RelayCommand(() =>
            {
                Urls.Add(URL);
                URL = string.Empty;
                MessageBox.Show("URL added");

            });
            StartRequestsCommand=new RelayCommand(() =>
            {
                var ds = (_dataService as DataService);
                ds.SetCallback(observable =>
                {
                    _onNextSubscription = observable.ObserveOn(DispatcherScheduler.Current).
                        Subscribe(resp => Responses.Add(resp),
                            ex =>
                            {
                                MessageBox.Show(ex.Message);
                                MessageBox.Show("Completed with error");
                                Cleanup();
                            },
                            () =>
                            {
                                MessageBox.Show("Completed");
                                Cleanup();
                            });
                });
                ds.GetData(_urls,null);
            });
        }

        public override void Cleanup()
        {
            // Clean up if needed
            Urls.Clear();
            if(_onNextSubscription!=null)
            _onNextSubscription.Dispose();
            base.Cleanup();
        }
    }
}