using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using APAS.CoreLib;
using APAS.Plugin.AutoTouchByMEQ.Classes;
using APAS.Plugin.AutoTouchByMEQ.Views;
using APAS.Plugin.Sdk.Base;
using APAS.ServiceContract.Wcf;
using Newtonsoft.Json;

namespace APAS.Plugin.AutoTouchByMEQ
{
    public class AutoTouchByMEQ : PluginBase
    {

        #region Variables
        private readonly string param1 = "";
        private string _selectedLMU = "";
        #endregion

        #region Constructors

        public AutoTouchByMEQ(ISystemService apasService, string caption) : base(Assembly.GetExecutingAssembly(), apasService, caption)
        {
            var config = GetAppConfig();

            // read config
            Configuration cfg = null;
            var configFileName = Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\config.json";
            apasService?.__SSC_LogInfo($"Configuration of {this}: {configFileName}");

            if (File.Exists(configFileName) == false) 
                cfg = new Configuration();
            else
            {
                try
                {
                    var json = File.ReadAllText(configFileName);
                    cfg = JsonConvert.DeserializeObject<Configuration>(json);
                }
                catch (Exception)
                {
                    cfg = new Configuration();
                }
            }



            var lmuNames = apasService?.__SSC_GetLMUList().Result;
            LMUList = CollectionViewSource.GetDefaultView(lmuNames ?? []);


            UserView = new AutoTouchByMEQView()
            {
                DataContext = this
            };
        }

        #endregion

        #region Properties
        
        public override string ShortCaption => "AutoTouch";

        public override string Description => "执行自动接触检测功能";

        public  ICollectionView LMUList { get; }

        public ICollectionView AxisList { get; private set; }

        public ICollectionView MeasurableEquipment { get; private set; }

        public string SelectedLMU
        {
            get => _selectedLMU;
            set
            {
                var isChanged = _selectedLMU != value;
                SetProperty(ref _selectedLMU, value);

                if (isChanged)
                {
                    var lmuNames = ApasService?.__SSC_GetAxisListInLMU(value).Result;
                    AxisList = CollectionViewSource.GetDefaultView(lmuNames ?? []);
                    OnPropertyChanged(nameof(AxisList));
                }
            }
        }

        public string SelectedAxis { get; set; }

        public string SelectedMEQ { get; set; }

        public int Speed { get; set; } = 10;

        public double Step { get; set; } = 1;

        public double MaxDistance { get; set; } = 10;

        public double Threshold { get; set; } = 0.01;

        #endregion

        #region Commands

        public RelayCommand RefreshMEQListCommand =>
            new(() =>
            {
                try
                {
                    var meqNames = ApasService?.__SSC_GetMEQList().Result;
                    MeasurableEquipment = CollectionViewSource.GetDefaultView(meqNames ?? []);
                    OnPropertyChanged(nameof(MeasurableEquipment));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });


        public RelayCommand StartCommand =>
            new(async () =>
            {
                try
                {
                    await Execute(null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            });

        public RelayCommand StopCommand =>
            new(() =>
            {
                try
                {
                    ApasService?.__SSC_MotionStop();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            });

        #endregion

        #region Methods

        public sealed override async Task<object> Execute(object args)
        {
            await ApasService.__SSC_AutoTouchByMEQ(SelectedLMU, SelectedAxis, (double)Speed, Step, MaxDistance, SelectedMEQ,
                Threshold);
            return null;
        }

        #endregion
    }
}
