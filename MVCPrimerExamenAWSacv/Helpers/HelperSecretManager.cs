using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace ExamenAWSZapatillas.Helpers
{
    public class HelperSecretManager
    {
        // Modificamos el método para que sea público y devuelva un string
        public static async Task<string> GetSecretAsync()
        {
            string secretName = "datasecrets";
            string region = "us-east-2"; // Región de Ohio

            // Creamos el cliente de AWS Secrets Manager
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT",
            };

            try
            {
                GetSecretValueResponse response = await client.GetSecretValueAsync(request);

                // Retornamos el JSON que contiene tus secretos guardados
                return response.SecretString;
            }
            catch (Exception e)
            {
                // Si falla (por falta de permisos o credenciales), lanzará la excepción
                throw new Exception($"Error al recuperar el secreto de AWS: {e.Message}", e);
            }
        }
    }
}