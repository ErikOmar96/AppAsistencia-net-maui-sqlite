using AppAsistencia.Vistas;
using CommunityToolkit.Mvvm.Input;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAsistencia.VistaModelos
{
    public partial class AsistenciaVM
    {
        // Campo de tipoIFingerprint
        private readonly IFingerprint _fingerprint;


        public AsistenciaVM(IFingerprint fingerprint)
        {
            _fingerprint = fingerprint;
        }


        public async void ValidarBiometrico()
        {
            var hasBiometric = await _fingerprint.GetAvailabilityAsync();
            var bioType = await _fingerprint.GetAuthenticationTypeAsync();

            if (hasBiometric == FingerprintAvailability.Available)
            {
                var request = new AuthenticationRequestConfiguration("Biometric Auth!", $"use {bioType} to check assistant");
                var result = await _fingerprint.AuthenticateAsync(request);
                if (result.Authenticated)
                {
                    await Shell.Current.DisplayAlert("Authenticated", "Assistant is valid", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Not Authenticated", "Assistant is not valid", "OK");
                }
            }
            else 
            {
                await Shell.Current.DisplayAlert("Info!", "No bioemtrics found", "OK");
            }

        }                                                 
    }
}
