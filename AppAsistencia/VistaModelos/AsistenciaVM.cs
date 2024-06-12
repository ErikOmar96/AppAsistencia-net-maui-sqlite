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
        private readonly AsistenciaDBContext _context;
        private readonly Usuario _usuarioAutenticado;

        public AsistenciaVM(AsistenciaDBContext context, Usuario usuarioAutenticado)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _usuarioAutenticado = usuarioAutenticado ?? throw new ArgumentNullException(nameof(usuarioAutenticado));
            Asistencias = new ObservableCollection<Asistencia>();
        }

        [ObservableProperty]
        private ObservableCollection<Asistencia> _asistencias;

        [ObservableProperty]
        private Asistencia _operatingAsistencia = new();

        partial void OnOperatingAsistenciaChanged(Asistencia value)
        {
            value?.AjustarHoraActual();
        }

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;

        [ObservableProperty]
        private DateTime _selectedDate = DateTime.Now;

        public async Task LoadAsistenciasAsync()
        {
            await ExecuteAsync(async () =>
            {
                var asistencias = await _context.GetAllAsync<Asistencia>();
                if (asistencias is not null && asistencias.Any())
                {
                    Asistencias.Clear();
                    foreach (var asistencia in asistencias)
                    {
                        Asistencias.Add(asistencia);
                    }
                }
            }, "Obteniendo asistencias...");
        }

        public string TipoUsuarioAutenticado => _usuarioAutenticado.TipoUsuario;

        [RelayCommand]
        private async void SetOperatingAsistencia(Asistencia? asistencia)
        {
            if (_usuarioAutenticado.TipoUsuario == "Administrador")
            {
                OperatingAsistencia = asistencia ?? new();
            }
            else if (Shell.Current != null)
            {
                await Shell.Current.DisplayAlert("Permiso Denegado", "No tienes permiso para editar asistencias", "OK");
            }
        }

        [RelayCommand]
        private async Task SaveAsistenciaAsync()
        {
            if (_usuarioAutenticado.TipoUsuario != "Administrador")
            {
                if (Shell.Current != null)
                {
                    await Shell.Current.DisplayAlert("Permiso Denegado", "No tienes permiso para editar asistencias", "OK");
                }
                return;
            }

            if (OperatingAsistencia is null)
                return;

            var (isValid, errorMessage) = OperatingAsistencia.Validate();
            if (!isValid)
            {
                if (Shell.Current != null)
                {
                    await Shell.Current.DisplayAlert("Error de validación", errorMessage, "OK");
                }
                return;
            }

            var busyText = "Actualizando Asistencia...";
            await ExecuteAsync(async () =>
            {
                if (OperatingAsistencia.IdAsistencia != 0)
                {
                    await _context.UpdateItemAsync(OperatingAsistencia);

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
            if (_usuarioAutenticado.TipoUsuario != "Administrador")
            {
                if (Shell.Current != null)
                {
                    await Shell.Current.DisplayAlert("Permiso Denegado", "No tienes permiso para eliminar asistencias", "OK");
                }
                return;
            }

            await ExecuteAsync(async () =>
            {
                if (await _context.DeleteItemByKeyAsync<Asistencia>(id))
                {
                    var asistencia = Asistencias.FirstOrDefault(a => a.IdAsistencia == id);
                    if (asistencia != null)
                    {
                        Asistencias.Remove(asistencia);
                    }
                }
                else
                {
                    if (Shell.Current != null)
                    {
                        await Shell.Current.DisplayAlert("ERROR ELIMINACIÓN", "No se ha eliminado la asistencia", "OK");
                    }
                }
            }, "Eliminando Asistencia...");
        }

        private async Task ExecuteAsync(Func<Task> operation, string? busyText = null)
        {
            IsBusy = true;
            BusyText = busyText ?? "Procesando...";
            try
            {
                if (operation != null)
                {
                    await operation.Invoke();
                }
            }
            catch (Exception ex)
            {
                if (Shell.Current != null)
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                }
            }
            finally
            {
                IsBusy = false;
                BusyText = null;
            }
        }
    }
}
