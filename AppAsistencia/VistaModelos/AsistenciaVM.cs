//using Plugin.Fingerprint;
//using Plugin.Fingerprint.Abstractions;
using AppAsistencia.DataAccess;
using AppAsistencia.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AppAsistencia.VistaModelos
{
    public partial class AsistenciaVM : ObservableObject
    {
        // Campo de tipoIFingerprint
        //private readonly IFingerprint _fingerprint;
        private readonly AsistenciaDBContext _context;


        public AsistenciaVM(AsistenciaDBContext context)
        {
            //_fingerprint = fingerprint;
            _context = context;
        }

        [ObservableProperty]
        private ObservableCollection<Asistencia> _asistencias;

        [ObservableProperty]
        private Asistencia _operatingAsistencia = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;

        
        public async Task LoadAsistenciasAsync()
        {
            await ExecuteAsync(async () => 
            {
                var asistencias = await _context.GetAllAsync<Asistencia>();
                if (asistencias is not null && asistencias.Any())
                {
                    Asistencias ??= new ObservableCollection<Asistencia>();

                    foreach (var asistencia in asistencias)
                    {
                        Asistencias.Add(asistencia);
                    }
                }
            }, "Obteniendo asistencias...");          
        }

        [RelayCommand]
        private void SetOperatingAsistencia(Asistencia? asistencia) => OperatingAsistencia = asistencia ?? new();

        [RelayCommand]
        private async Task SaveAsistenciaAsync()
        {
            if (OperatingAsistencia is null)
                return;

            var busyText = OperatingAsistencia.IdAsistencia == 0 ? "Guardar Asistencia" : "Actualizar Asistencia";
            await ExecuteAsync(async () => 
            {
                if (OperatingAsistencia.IdAsistencia == 0)
                {
                    // Guardar asistencia
                    await _context.AddItemAsync<Asistencia>(OperatingAsistencia);
                    Asistencias.Add(OperatingAsistencia);
                }
                else
                {
                    // Actualizar asistencia
                    await _context.UpdateItemAsync<Asistencia>(OperatingAsistencia);

                    var asistenciaCopy = OperatingAsistencia.Clone();

                    var index = Asistencias.IndexOf(OperatingAsistencia);
                    Asistencias.RemoveAt(index);

                    Asistencias.Insert(index, asistenciaCopy);
                }
                SetOperatingAsistenciaCommand.Execute(new());
            }, busyText);        
        }

        [RelayCommand]
        private async Task DeleteAsistenciaAsync(int id)
        {
            await ExecuteAsync(async () => 
            {
                if (await _context.DeleteItemByKeyAsync<Asistencia>(id))
                {
                    var asistencia = Asistencias.FirstOrDefault(a => a.IdAsistencia == id);
                    Asistencias.Remove(asistencia);
                }
                else
                {
                    await Shell.Current.DisplayAlert("ERROR ELIMINACIÓN", "No se ha eliminado la asistencia", "OK");
                }
            }, "Eliminando Asistencia...");
        }

        private async Task ExecuteAsync(Func<Task> operation, string? busyText = null)
        {
            IsBusy = true;
            BusyText = busyText ?? "Procesando...";
            try
            {
                await operation?.Invoke();
            }
            finally
            {
                IsBusy = false;
                BusyText = busyText ?? "Procesando...";
            }
        }
    }
}
