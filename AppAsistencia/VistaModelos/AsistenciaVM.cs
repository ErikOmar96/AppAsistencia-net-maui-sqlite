﻿//using Plugin.Fingerprint;
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
                    //Asistencias ??= new ObservableCollection<Asistencia>();

                    //foreach (var asistencia in asistencias)
                    //{
                    //    Asistencias.Add(asistencia);
                    //}
                    Asistencias.Clear();
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
            //if (OperatingAsistencia is null)
            //    return;

            //var (isValid, errorMessage) = OperatingAsistencia.Validate();
            //if (!isValid)
            //{
            //    await Shell.Current.DisplayAlert("Error de validación", errorMessage, "OK");
            //    return;
            //}

            //var busyText = OperatingAsistencia.IdAsistencia == 0 ? "Guardar Asistencia" : "Actualizar Asistencia";
            //await ExecuteAsync(async () => 
            //{
            //    if (OperatingAsistencia.IdAsistencia == 0)
            //    {
            //        // Guardar asistencia
            //        await _context.AddItemAsync<Asistencia>(OperatingAsistencia);
            //        Asistencias.Add(OperatingAsistencia);
            //    }
            //    else
            //    {
            //        // Actualizar asistencia
            //        await _context.UpdateItemAsync<Asistencia>(OperatingAsistencia);

            //        var asistenciaCopy = OperatingAsistencia.Clone();

            //        var index = Asistencias.IndexOf(OperatingAsistencia);
            //        Asistencias.RemoveAt(index);

            //        Asistencias.Insert(index, asistenciaCopy);
            //    }
            //    SetOperatingAsistenciaCommand.Execute(new());
            //}, busyText);
            if (OperatingAsistencia is null)
                return;

            var (isValid, errorMessage) = OperatingAsistencia.Validate();
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Error de validación", errorMessage, "OK");
                return;
            }

            var busyText = "Actualizando Asistencia...";
            await ExecuteAsync(async () =>
            {
                if (OperatingAsistencia.IdAsistencia != 0)
                {
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
                    //Asistencias.Remove(asistencia);
                    if (asistencia != null)
                    {
                        Asistencias.Remove(asistencia);
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("ERROR ELIMINACIÓN", "No se ha eliminado la asistencia", "OK");
                }
            }, "Eliminando Asistencia...");
        }

        //[RelayCommand]
        //private async Task SearchAsistenciasByDateAsync()
        //{
        //    await ExecuteAsync(async () =>
        //    {
        //        var asistencias = await _context.GetFilteredAsync<Asistencia>(a => a.FechaAsistencia.Date == SelectedDate.Date);
        //        if (asistencias is not null && asistencias.Any())
        //        {
        //            Asistencias.Clear();
        //            foreach (var asistencia in asistencias)
        //            {
        //                Asistencias.Add(asistencia);
        //            }
        //        }
        //        else
        //        {
        //            Asistencias.Clear();
        //        }
        //    }, "Buscando asistencias...");
        //}

        private async Task ExecuteAsync(Func<Task> operation, string? busyText = null)
        {
            //IsBusy = true;
            //BusyText = busyText ?? "Procesando...";
            //try
            //{
            //    await operation?.Invoke();
            //}
            //catch (Exception ex)
            //{
            //    // Manejar la excepción de manera adecuada
            //    await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            //}
            //finally
            //{
            //    IsBusy = false;
            //    BusyText = busyText ?? "Procesando...";
            //}
            IsBusy = true;
            BusyText = busyText ?? "Procesando...";
            try
            {
                await operation?.Invoke();
            }
            catch (Exception ex)
            {
                // Manejar la excepción de manera adecuada
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                BusyText = null;
            }
        }
    }
}
