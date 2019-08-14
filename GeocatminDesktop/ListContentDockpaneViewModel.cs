using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using ArcGIS.Core.Data;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;

namespace GeocatminDesktop
{
    internal class ListContentDockpaneViewModel : DockPane
    {
        private const string _dockPaneID = "GeocatminDesktop_ListContentDockpane";
        private ICommand _openGdbCmd;
        private ObservableCollection<GdbItem> _gdbDefinitions = new ObservableCollection<GdbItem>();
        private readonly object _lockGdbDefinitions = new object();

        protected ListContentDockpaneViewModel()
        {
            BindingOperations.EnableCollectionSynchronization(_gdbDefinitions, _lockGdbDefinitions);
        }

        public ICommand OpenGdbCmd
        {
            get
            {
                return _openGdbCmd ?? (_openGdbCmd = new
                  RelayCommand(() => LookupItems(), () => true));
            }
        }

        internal static void Show()
        {
            var pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            pane?.Activate();
        }

        private string _heading = "BD Geocientifica";
        public string Heading
        {
            get { return _heading; }
            set
            {
                SetProperty(ref _heading, value, () => Heading);
            }
        }

        private string _gdbPath = string.Empty;
        public string GdbPath
        {
            get { return _gdbPath; }
            set
            {
                SetProperty(ref _gdbPath, value, () => GdbPath);
                LookupItems();
            }
        }

        public ObservableCollection<GdbItem> GdbDefinitions
        {
            get { return _gdbDefinitions; }
            set
            {
                SetProperty(ref _gdbDefinitions, value, () => GdbDefinitions);
            }
        }

        private void OpenGdbDialog()
        {
            OpenItemDialog searchGdbDialog = new OpenItemDialog
            {
                Title = "Find GDB",
                InitialLocation = @"C:\Data",
                MultiSelect = false,
                Filter = ItemFilters.geodatabases
            };

            var ok = searchGdbDialog.ShowDialog();
            if (ok != true) return;
            var selectedItems = searchGdbDialog.Items;
            foreach (var selectedItem in selectedItems)
                GdbPath = selectedItem.Path;
        }

        private void LookupItems()
        {
            QueuedTask.Run(() =>
            {
                Map map = MapView.Active.Map;
                IEnumerable<Layer> layers = map.GetLayersAsFlattenedList().Where(l => l.Name.Contains("world"));

                MapView mapView = MapView.Active;
                IReadOnlyList<Layer> selectedLayers = mapView.GetSelectedLayers();
                IReadOnlyList<StandaloneTable> selectedTables = mapView.GetSelectedStandaloneTables();

                //create a layer from a feature class off an sde
                string uriSde = @"C:\Users\autonomoosi02\source\repos\GeocatminDesktop\GeocatminDesktop\conn\bdgeocat_huawei_dataedit.sde\DATA_EDIT.DS_DRME\DATA_EDIT.GPT_DRME_GEOQUIMICA";
                Layer lyr = LayerFactory.Instance.CreateLayer(new Uri(uriSde), map);

                using (Geodatabase fileGeodatabase = new Geodatabase(new DatabaseConnectionFile(new Uri("C:\\Users\\autonomoosi02\\source\\repos\\GeocatminDesktop\\GeocatminDesktop\\conn\\bdgeocat_huawei_dataedit.sde"))))
                {
                    IReadOnlyList<FeatureClassDefinition> fcdefinitions = fileGeodatabase.GetDefinitions<FeatureClassDefinition>();
                    lock (_lockGdbDefinitions)
                    {
                        _gdbDefinitions.Clear();
                        foreach (var definition in fcdefinitions)
                        {
                            if (definition.GetName().Contains("MG"))
                            {
                                _gdbDefinitions.Add(new GdbItem() { Name = definition.GetName(), Type = definition.DatasetType.ToString() });
                            }
                        }
                    }
                    IReadOnlyList<TableDefinition> tbdefinitions = fileGeodatabase.GetDefinitions<TableDefinition>();
                    lock (_lockGdbDefinitions)
                    {
                        foreach (var definition in tbdefinitions)
                        {
                            if (definition.GetName().Contains("MG"))
                            {
                                _gdbDefinitions.Add(new GdbItem() { Name = definition.GetName(), Type = definition.DatasetType.ToString() });
                            }
                        }
                    }

                }
            }).ContinueWith(t =>
            {
                if (t.Exception == null) return;
                var aggException = t.Exception.Flatten();
                foreach (var exception in aggException.InnerExceptions)
                    System.Diagnostics.Debug.WriteLine(exception.Message);
            });
        }

        public async Task OpenEnterpriseGeodatabaseUsingSDEFilePath()
        {
            await QueuedTask.Run(() => {
                using (Geodatabase geodatabase = new Geodatabase(new DatabaseConnectionFile(new Uri("conn\\bdgeocat_publ_gis.sde"))))
                {
                    // Use the geodatabase.
                }
            });
        }
    }

    internal class GeocatminDesktop_ListContentDockpane_ShowButton : Button
    {
        protected override void OnClick()
        {
            ListContentDockpaneViewModel.Show();
        }
    }

    internal class GdbItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
