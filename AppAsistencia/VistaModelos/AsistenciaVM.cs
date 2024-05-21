using AppAsistencia.DataAccess;
using AppAsistencia.DTOs;
using AppAsistencia.Modelos;
using AppAsistencia.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
//using Plugin.Fingerprint;
//using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAsistencia.VistaModelos
{
    public partial class AsistenciaVM : ObservableObject
    {
        // Propiedades
        //private readonly IFingerprint _fingerprint;
        private readonly AsistenciaDBContext _dbContext;
        
        public AsistenciaVM(AsistenciaDBContext dBContext)
        {
            //_fingerprint = fingerprint;
            _dbContext = dBContext;
        }

        private int IdAsistencia;      
        [ObservableProperty]
        private string estadoAsistencia = string.Empty;
        [ObservableProperty]
        private UsuarioDTO usuarioDTO;


        // public async void ValidarBiometrico()
        // {
            //var hasBiometric = await _fingerprint.GetAvailabilityAsync();
            //var bioType = await _fingerprint.GetAuthenticationTypeAsync();

            //if (hasBiometric == FingerprintAvailability.Available)
            //{
            //    var request = new AuthenticationRequestConfiguration("Biometric Auth!", $"use {bioType} to check assistant");
            //    var result = await _fingerprint.AuthenticateAsync(request);
            //    if (result.Authenticated)
            //    {
            //        await Shell.Current.DisplayAlert("Authenticated", "Assistant is valid", "OK");
            //    }
            //    else
            //    {
            //        await Shell.Current.DisplayAlert("Not Authenticated", "Assistant is not valid", "OK");
            //    }
            //}
            //else 
            //{
            //    await Shell.Current.DisplayAlert("Info!", "No bioemtrics found", "OK");
            //}

        // }

        public async Task Guardar()
        {
            var guardarAsistencia = new AsistenciaDTO
            {
                IdAsistencia = IdAsistencia,
                Usuario = usuarioDTO,
            };

            var asistencia = new Asistencia 
            {
                IdAsistencia = IdAsistencia,
                IdUsuario = UsuarioDTO.IdUsuario,
                FechaAsistencia = DateTime.Now,
                EstadoAsistencia = "Presente"
            };
            _dbContext.Add(asistencia);
            await _dbContext.SaveChangesAsync();
            await Shell.Current.DisplayAlert("EXITO", "Se ha registrado su asistencia en la BD", "OK");
        }                                                 
    }
}
