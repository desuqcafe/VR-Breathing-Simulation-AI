using System.Security.Cryptography.X509Certificates;
using UnityEngine.Networking;

public class UnityWebRequestCertificateHandler : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        // For testing purposes, always accept the certificate
        return true;
    }
}